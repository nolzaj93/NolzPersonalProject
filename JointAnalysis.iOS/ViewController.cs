/*
 *  Austin Nolz COP 2001 Personal Project, learning/testing with ARKit,Vision
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
    public class ViewController : UIViewController
    {
        //Source: https://github.com/vecalion/Xamarin.VisionFrameworkFaceLandmarks/blob/master/Vision.FaceLandmarksDemo/ViewController.cs
        readonly AVCaptureSession _captureSession = new AVCaptureSession();
        readonly CAShapeLayer _shapeLayer = new CAShapeLayer();

        AVCaptureVideoPreviewLayer _videoPreview;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        //Source:: https://msdn.microsoft.com/en-us/magazine/mt830360.aspx
        //Starts an AR session and shows how to load 3d assets
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _videoPreview = new AVCaptureVideoPreviewLayer(_captureSession);

            ConfigureDeviceAndStart();

        }

        //Source: https://github.com/vecalion/Xamarin.VisionFrameworkFaceLandmarks/blob/master/Vision.FaceLandmarksDemo/ViewController.cs
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            View.Layer.AddSublayer(_videoPreview);

            // needs to filp coordinate system for Vision
            _shapeLayer.AffineTransform = CGAffineTransform.MakeScale(1, -1);

            View.Layer.AddSublayer(_shapeLayer);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            _videoPreview.Frame = View.Frame;
            _shapeLayer.Frame = View.Frame;
        }

        public AVCaptureDevice GetDevice()
        {
            var videoDeviceDiscoverySession = AVCaptureDeviceDiscoverySession.Create(new AVCaptureDeviceType[] { AVCaptureDeviceType.BuiltInWideAngleCamera },
                                                                                                                 AVMediaType.Video,
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
                _captureSession.AddInput(input);

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
                videoOutput.SetSampleBufferDelegateQueue(new OutputRecorder(View, _shapeLayer), videoCaptureQueue);

                if (_captureSession.CanAddOutput(videoOutput))
                {
                    _captureSession.AddOutput(videoOutput);
                }

                // Start session
                _captureSession.StartRunning();
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
    public class OutputRecorder : AVCaptureVideoDataOutputSampleBufferDelegate
    {
        readonly UIView _view;
        CAShapeLayer _shapeLayer;

        public OutputRecorder(UIView view, CAShapeLayer shapeLayer)
        {
            _shapeLayer = shapeLayer;
            _view = view;
        }

        public override void DidOutputSampleBuffer(AVCaptureOutput captureOutput, CMSampleBuffer sampleBuffer, AVCaptureConnection connection)
        {
            using (var pixelBuffer = sampleBuffer.GetImageBuffer())
            using (var ciImage = new CIImage(pixelBuffer))
            using (var imageWithOrientation = ciImage.CreateByApplyingOrientation(ImageIO.CGImagePropertyOrientation.LeftMirrored))
            {
                DetectFaceLandmarks(imageWithOrientation);
            }

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




