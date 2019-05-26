/*
 *  Austin Nolz COP 2001 Personal Project, learning/testing with ARKit,Vision
 */

using System;
using System.Linq;
using AVFoundation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace JointAnalysis.iOS
{
    //Source: https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/View
    public class UICameraPreview : UIView
    {
        private AVCaptureVideoPreviewLayer camPreviewLayer;
        readonly CameraOptions cameraOptions;

        public event EventHandler<EventArgs> Tapped;

        public AVCaptureSession CaptureSession { get; private set; }

        public bool IsPreviewing { get; set; }

        public UICameraPreview(CameraOptions options)
        {
            cameraOptions = options;
            IsPreviewing = false;
            Initialize();
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            camPreviewLayer.Frame = rect;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            OnTapped();
        }

        protected virtual void OnTapped()
        {
            var eventHandler = Tapped;
            if (eventHandler != null)
            {
                eventHandler(this, new EventArgs());
            }
        }

        void Initialize()
        {
            CaptureSession = new AVCaptureSession();
            camPreviewLayer = new AVCaptureVideoPreviewLayer(CaptureSession)
            {
                Frame = Bounds,
                VideoGravity = AVLayerVideoGravity.ResizeAspectFill
            };

            var videoDevices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);
            var cameraPosition = (cameraOptions == CameraOptions.Front) ? AVCaptureDevicePosition.Front : AVCaptureDevicePosition.Back;
            var device = videoDevices.FirstOrDefault(d => d.Position == cameraPosition);

            if (device == null)
            {
                return;
            }

            NSError error;
            var input = new AVCaptureDeviceInput(device, out error);
            CaptureSession.AddInput(input);
            Layer.AddSublayer(camPreviewLayer);
            CaptureSession.StartRunning();
            IsPreviewing = true;
        }
    }
}


