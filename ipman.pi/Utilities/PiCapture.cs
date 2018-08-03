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

        public bool TryOpenVideoCapture()
        {
            _captureInstance = new VideoCapture(0);
            return _captureInstance.IsOpened();
        }

        public void ForceOpenVideoCapture()
        {
            _captureInstance =  _captureInstance ?? new VideoCapture(0);
            while (!_captureInstance.IsOpened())
            {
                Console.WriteLine("Video Capture being reopened.");
                _captureInstance.Open(0);
                Thread.Sleep(500);
            }
        }
        public void DisposeCaptureInstance()
        {
            _captureInstance.Release();
            _captureInstance.Dispose();
            _captureInstance = null;
        }
        public byte[] SingleImageCameraByteArray()
        {
            Mat captureImage = new Mat();
           if (_captureInstance == null || !_captureInstance.IsOpened())
                throw new Exception("Camera was not properly opened before calling SingleImageCameraByteArray - Rob Error");
            double desiredHeight = 600;
            double desiredWidth = 800;

            _captureInstance.Set(CaptureProperty.FrameHeight, desiredHeight);
            _captureInstance.Set(CaptureProperty.FrameWidth, desiredWidth);

            _captureInstance.Read(captureImage);

            if (captureImage.Empty())
                throw new Exception("Image is empty - Rob Error.");

            Console.WriteLine("Image successfully captured.");

            Mat copy = captureImage.Clone();

            return copy.ToBytes(".jpeg");
        }

        // VideoWriter writer = new VideoWriter("/share/ipman.pi/test.jpeg", "JPEG", 30, new Size(640, 480));
    }
}
