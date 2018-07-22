using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ipman.pi.Utilities
{
    public class PiCapture
    {
        private readonly string _fileName;

        private VideoCapture _captureInstance;

        public PiCapture()
        {  }

        public PiCapture(string fileName)
        {
            _fileName = fileName;
        }

        public byte[] SingleImageCameraByteArray()
        {
            Mat captureImage = new Mat();
            try
            {
                _captureInstance = new VideoCapture(0);

                while (!_captureInstance.IsOpened())
                {
                    Console.WriteLine("Video Capture being reopened.");
                    _captureInstance.Open(0);
                    Thread.Sleep(500);
                }

                double desiredHeight = 600;
                double desiredWidth = 800;

                _captureInstance.Set(CaptureProperty.FrameHeight, desiredHeight);
                _captureInstance.Set(CaptureProperty.FrameWidth, desiredWidth);

                Console.WriteLine("Set Capture Width x Height : {0} x {1}", desiredWidth, desiredHeight);

                double height = _captureInstance.Get(CaptureProperty.FrameHeight);
                double width = _captureInstance.Get(CaptureProperty.FrameWidth);

                Console.WriteLine("Current Capture Width x Height : {0} x {1}", width, height);
                _captureInstance.Read(captureImage);

                if (captureImage.Empty())
                    throw new Exception("Image is empty - Rob Error.");

                Console.WriteLine("Image successfully captured.");
            }
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                _captureInstance.Release();
                _captureInstance.Dispose();
            }

            Mat copy = captureImage.Clone();

            return copy.ToBytes(".jpeg");
        }

        // VideoWriter writer = new VideoWriter("/share/ipman.pi/test.jpeg", "JPEG", 30, new Size(640, 480));
    }
}
