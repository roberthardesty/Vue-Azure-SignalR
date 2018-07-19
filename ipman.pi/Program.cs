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

namespace ipman.pi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Rob you are the one true master!");

            //var counterService = new CounterService();
            //await counterService.StartLoggingIncrement().ConfigureAwait(false);

            PiCapture piCam = new PiCapture();

            byte[] imageBytes = piCam.SingleImageCameraByteArray();

            await CloudUpload.UploadImage(imageBytes);
            Console.WriteLine("Completed Successfully!");
        }
    }
}