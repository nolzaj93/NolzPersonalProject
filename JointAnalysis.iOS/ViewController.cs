/*
 *  Austin Nolz COP 2001 Personal Project, learning/testing with ARKit,Vision
 * in Xamarin iOS.
 */

using System;
using System.Linq;
using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using CoreImage;
using CoreMedia;
using UIKit;
using Vision;

namespace JointAnalysis.iOS
{
    /* The main ViewController
     */
    public class ViewController : UIViewController
    {
        //Source: https://github.com/vecalion/Xamarin.VisionFrameworkFaceLandmarks/blob/master/Vision.FaceLandmarksDemo/ViewController.cs
        readonly AVCaptureSession avCapSession = new AVCaptureSession();
        readonly CAShapeLayer shapeOverlay = new CAShapeLayer();
        private AVCaptureVideoPreviewLayer vidPreviewLayer;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        //Source:: https://msdn.microsoft.com/en-us/magazine/mt830360.aspx
        //Starts an AR session and shows how to load 3d assets
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            vidPreviewLayer = new AVCaptureVideoPreviewLayer(avCapSession);

            ConfigureDeviceAndStart();


        }

        //Source: https://github.com/vecalion/Xamarin.VisionFrameworkFaceLandmarks/blob/master/Vision.FaceLandmarksDemo/ViewController.cs
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            View.Layer.AddSublayer(vidPreviewLayer);

            // needs to filp coordinate system for Vision
            shapeOverlay.AffineTransform = CGAffineTransform.MakeScale(1, -1);

            View.Layer.AddSublayer(shapeOverlay);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            vidPreviewLayer.Frame = View.Frame;
            shapeOverlay.Frame = View.Frame;
        }

        //Function that discovers the device's back camera and returns an AVCaptureDevice object to 
        // ConfigureDeviceAndStart() where it is called below.
        public AVCaptureDevice GetDevice()
        {
            var videoDeviceDiscoverySession = AVCaptureDeviceDiscoverySession.Create(new AVCaptureDeviceType[] { AVCaptureDeviceType.BuiltInWideAngleCamera }, AVMediaType.Video,
                                                                                                                 AVCaptureDevicePosition.Back);
            return videoDeviceDiscoverySession.Devices.FirstOrDefault();
        }

        public void ConfigureDeviceAndStart()
        {
            var device = GetDevice();

            if (device == null)
            {
                return;
            }

            try
            {
                if (device.LockForConfiguration(out var error))
                {
                    if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
                    {
                        device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                    }

                    device.UnlockForConfiguration();
                }

                // Configure Input
                var input = AVCaptureDeviceInput.FromDevice(device, out var error2);
                avCapSession.AddInput(input);

                // Configure Output
                var settings = new AVVideoSettingsUncompressed()
                {
                    PixelFormatType = CoreVideo.CVPixelFormatType.CV32BGRA
                };

                var videoOutput = new AVCaptureVideoDataOutput
                {
                    WeakVideoSettings = settings.Dictionary,
                    AlwaysDiscardsLateVideoFrames = true
                };

                var videoCaptureQueue = new DispatchQueue("Video Queue");
                videoOutput.SetSampleBufferDelegateQueue(new OutputRecorder((UICameraPreview)View, shapeOverlay), videoCaptureQueue);

                if (avCapSession.CanAddOutput(videoOutput))
                {
                    avCapSession.AddOutput(videoOutput);
                }

                // Start session
                avCapSession.StartRunning();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }

    //Vision Face Landmarker Detection
    //https://trailheadtechnology.com/vision-framework-for-face-landmarks-detection-using-xamarin-ios/
    //https://github.com/vecalion/Xamarin.VisionFrameworkFaceLandmarks
    public class OutputRecorder : AVCaptureVideoDataOutputSampleBufferDelegate
    {
        readonly UICameraPreview _view;
        CAShapeLayer _shapeLayer;

        public OutputRecorder(UICameraPreview view, CAShapeLayer shapeLayer)
        {
            _shapeLayer = shapeLayer;
            _view = view;
        }

        /*
         * This function is called each time new frames are captured. A CIImage object is created from the 
         * pixels in the buffer on each call to this function. This is left mirrored to the correct orientation
         * and then passed as an argument to DetectFaceLandmarks().        
         */
        public override void DidOutputSampleBuffer(AVCaptureOutput captureOutput, CMSampleBuffer sampleBuffer, AVCaptureConnection connection)
        {
            using (var imageBuffer = sampleBuffer.GetImageBuffer())
            using (var ciImage = new CIImage(imageBuffer))
            using (var imageWithOrientation = ciImage.CreateByApplyingOrientation(ImageIO.CGImagePropertyOrientation.LeftMirrored))
            {
                DetectFaceLandmarks(imageWithOrientation);
            }

            //
            sampleBuffer.Dispose();
        }

        VNSequenceRequestHandler _sequenceRequestHandler = new VNSequenceRequestHandler();
        VNDetectFaceLandmarksRequest _detectFaceLandmarksRequest;

        void DetectFaceLandmarks(CIImage imageWithOrientation)
        {
            if (_detectFaceLandmarksRequest == null)
            {
                _detectFaceLandmarksRequest = new VNDetectFaceLandmarksRequest((request, error) =>
                {
                    RemoveSublayers(_shapeLayer);

                    if (error != null)
                    {
                        throw new Exception(error.LocalizedDescription);
                    }

                    var results = request.GetResults<VNFaceObservation>();

                    foreach (var result in results)
                    {
                        if (result.Landmarks == null)
                        {
                            continue;
                        }

                        var boundingBox = result.BoundingBox;
                        var scaledBoundingBox = Scale(boundingBox, _view.Bounds.Size);

                        InvokeOnMainThread(() =>
                        {
                            DrawLandmark(result.Landmarks.FaceContour, scaledBoundingBox, false, UIColor.White);

                            DrawLandmark(result.Landmarks.LeftEye, scaledBoundingBox, true, UIColor.Green);
                            DrawLandmark(result.Landmarks.RightEye, scaledBoundingBox, true, UIColor.Green);

                            DrawLandmark(result.Landmarks.Nose, scaledBoundingBox, true, UIColor.Blue);
                            DrawLandmark(result.Landmarks.NoseCrest, scaledBoundingBox, false, UIColor.Blue);

                            DrawLandmark(result.Landmarks.InnerLips, scaledBoundingBox, true, UIColor.Yellow);
                            DrawLandmark(result.Landmarks.OuterLips, scaledBoundingBox, true, UIColor.Yellow);

                            DrawLandmark(result.Landmarks.LeftEyebrow, scaledBoundingBox, false, UIColor.Blue);
                            DrawLandmark(result.Landmarks.RightEyebrow, scaledBoundingBox, false, UIColor.Blue);
                        });
                    }
                });
            }

            _sequenceRequestHandler.Perform(new[] { _detectFaceLandmarksRequest }, imageWithOrientation, out var requestHandlerError);
            if (requestHandlerError != null)
            {
                throw new Exception(requestHandlerError.LocalizedDescription);
            }
        }

        static void RemoveSublayers(CAShapeLayer layer)
        {
            if (layer.Sublayers?.Any() == true)
            {
                var sublayers = layer.Sublayers?.ToList();
                foreach (var sublayer in sublayers)
                {
                    sublayer.RemoveFromSuperLayer();
                }
            }
        }

        void DrawLandmark(VNFaceLandmarkRegion2D feature, CGRect scaledBoundingBox, bool closed, UIColor color)
        {
            if (feature == null)
            {
                return;
            }

            var mappedPoints = feature.NormalizedPoints.Select(o => new CGPoint(x: o.X * scaledBoundingBox.Width + scaledBoundingBox.X, y: o.Y * scaledBoundingBox.Height + scaledBoundingBox.Y));

            using (var newLayer = new CAShapeLayer())
            {
                newLayer.Frame = _view.Frame;
                newLayer.StrokeColor = color.CGColor;
                newLayer.LineWidth = 2;
                newLayer.FillColor = UIColor.Clear.CGColor;

                using (UIBezierPath path = new UIBezierPath())
                {
                    path.MoveTo(mappedPoints.First());
                    foreach (var point in mappedPoints.Skip(1))
                    {
                        path.AddLineTo(point);
                    }

                    if (closed)
                    {
                        path.AddLineTo(mappedPoints.First());
                    }

                    newLayer.Path = path.CGPath;
                }

                _shapeLayer.AddSublayer(newLayer);
            }
        }

        static CGRect Scale(CGRect source, CGSize dest)
        {
            return new CGRect
            {
                X = source.X * dest.Width,
                Y = source.Y * dest.Height,
                Width = source.Width * dest.Width,
                Height = source.Height * dest.Height
            };
        }
    }

}




