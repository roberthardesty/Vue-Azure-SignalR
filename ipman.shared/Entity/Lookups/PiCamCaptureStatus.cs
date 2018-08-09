using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.shared.Entity.Lookups
{
    public enum PiCamCaptureStatus
    {
        Received,
        OpeningCapture,
        ReadyToCapture,
        Capturing,
        Uploading,
        Posting,
        Finished
    }
}
