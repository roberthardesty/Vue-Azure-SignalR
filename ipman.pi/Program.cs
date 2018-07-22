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

namespace ipman.pi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Rob you are the one true master!");

            //var counterService = new CounterService();

            //await counterService.StartLoggingIncrement();

            var piCamService = new PiCamService();

            await piCamService.JoinPiCams();

            piCamService.RequestSingleImageCapture(async () =>
            {
                Console.WriteLine("Single Image Capture Requested.");

                PiCapture piCam = new PiCapture();

                byte[] imageBytes = piCam.SingleImageCameraByteArray();

                await CloudUpload.UploadImage(imageBytes);

                Console.WriteLine("Completed Successfully!");
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