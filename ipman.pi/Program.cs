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

        public static int STEP_COUNT = 6;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Rob you are the one true master!");

            //var counterService = new CounterService();

            //await counterService.StartLoggingIncrement();

            await RunService();

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

        static async Task RunService()
        {
            var piCamService = new PiCamService();

            var postService = new PostService();
            var test = await postService.TestConnection();
            await piCamService.JoinPiCams();

            piCamService.RequestSingleImageCapture(async () =>
            {
                await SingleCaptureAndPost(postService, piCamService);

            });

        }

        static async Task SingleCaptureAndPost(PostService postService, PiCamService piCamService)
        {
            Guid piPostID = Guid.NewGuid();
            DateTime nowTime = DateTime.UtcNow;
            Post piPost = new Post
            {
                ID = piPostID,
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

            Console.WriteLine($"Single Image Capture Requested. {piPostID}");

            var testConnection = await postService.TestConnection();

            if (!testConnection.Success)
            {
                await piCamService.UpdateProgress("Error connecting :(", -1, STEP_COUNT);
                Console.WriteLine("Connection Error!");
                return;
            }

            await piCamService.UpdateProgress("Received", 1, STEP_COUNT);

            PiCapture piCam = new PiCapture();

            if (!piCam.TryOpenVideoCapture())
            {
                await piCamService.UpdateProgress("Opening Camera", 2, STEP_COUNT);
                piCam.ForceOpenVideoCapture();
            }
            await piCamService.UpdateProgress("Capturing", 3, STEP_COUNT);

            byte[] imageBytes = piCam.SingleImageCameraByteArray();

            await piCamService.UpdateProgress("Uploading Image", 4, STEP_COUNT);

            piPost.PostImageUri = await CloudUpload.UploadPostImage(piPostID, imageBytes);

            await piCamService.UpdateProgress("Creating Post", 5, STEP_COUNT);

            var savePostResponse = await postService.AddPost(piPost);

            if (savePostResponse.Success)
            {
                await piCamService.UpdateProgress("Success!", 6, STEP_COUNT);
                Console.WriteLine("Completed Successfully! A New Post Should Be Availible.");
            }
            else
            {
                await piCamService.UpdateProgress("Error saving post :(", -1, 6);
                Console.WriteLine($"An Error happened while saving the post: {savePostResponse.Error.Message}");

            }
            piCam.DisposeCaptureInstance();
        }
    }
}