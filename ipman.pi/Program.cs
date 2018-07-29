using System;
using System.IO;
using Unosquare.RaspberryIO;
using OpenCvSharp;
using ipman.pi.Services;
using System.Threading.Tasks;
using System.Linq;
using OpenCvSharp.Dnn;
using System.Net;
using System.Collections.Generic;
using ipman.pi.Utilities;
using System.Threading;
using ipman.shared.Entity;

namespace ipman.pi
{
    class Program
    {
        public static Guid SiteRobsRaspID = Guid.Parse("6b93aaaa-8ef0-4d91-9574-fae60fc336a8");
        public static Guid UserBudNJoeID = Guid.Parse("4D8881BD-DB0A-4725-9CF0-2C4390013A30");

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Rob you are the one true master!");

            //var counterService = new CounterService();

            //await counterService.StartLoggingIncrement();

            var piCamService = new PiCamService();

            var postService = new PostService();
            var test = await postService.TestConnection();
            await piCamService.JoinPiCams();

            piCamService.RequestSingleImageCapture(async () =>
            {
                Guid piPostID = Guid.NewGuid();
                DateTime nowTime = DateTime.UtcNow;
                Post piPost = new Post
                {
                    PostTitle = $"Requested Image {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}",
                    PostDescription = "Eventually this should be generated",
                    IsActive = true,
                    IsLocked = false,
                    StartTimeUTC = nowTime,
                    LockTimeUTC = nowTime,
                    EndTimeUTC = nowTime,
                    CreatedUTC = nowTime,
                    LastUpdatedUTC = nowTime,
                    SiteAccountID = SiteRobsRaspID,
                    UserAccountCreatorID = UserBudNJoeID
                };

                Console.WriteLine("Single Image Capture Requested.");

                var testConnection = await postService.TestConnection();

                if (!testConnection.Success)
                {
                    Console.WriteLine("Connection Error!");
                    return;
                }
                PiCapture piCam = new PiCapture();

                byte[] imageBytes = piCam.SingleImageCameraByteArray();


                piPost.PostImageUri = await CloudUpload.UploadPostImage(piPostID, imageBytes);

                var savePostResponse = await postService.AddPost(piPost);

                if (savePostResponse.Success)
                    Console.WriteLine("Completed Successfully! A New Post Should Be Availible.");
                else
                    Console.WriteLine($"An Error happened while saving the post: {savePostResponse.Error.Message}");
            });

            CancellationTokenSource cts = new CancellationTokenSource();

            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                cts.Cancel();
            };

            await Task.Factory.StartNew(() =>
            {
                while (!cts.IsCancellationRequested);
            });

        }
    }
}