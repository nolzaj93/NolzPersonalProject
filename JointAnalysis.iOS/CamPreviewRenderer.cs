/*
 *  Austin Nolz COP 2001 Personal Project, learning/testing with ARKit,Vision
 */

using System;
using JointAnalysis;
using JointAnalysis.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//Source: https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/View
[assembly: ExportRenderer(typeof(CamPreview), typeof(CamPreviewRenderer))]
namespace JointAnalysis.iOS
{
    public class CamPreviewRenderer : ViewRenderer<CamPreview, UICameraPreview>
    {
        UICameraPreview uiCameraPreview;

        protected override void OnElementChanged(ElementChangedEventArgs<CamPreview> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                uiCameraPreview = new UICameraPreview(e.NewElement.Camera);
                SetNativeControl(uiCameraPreview);
            }
            if (e.OldElement != null)
            {
                // Unsubscribe
                uiCameraPreview.Tapped -= OnCameraPreviewTapped;
            }
            if (e.NewElement != null)
            {
                // Subscribe
                uiCameraPreview.Tapped += OnCameraPreviewTapped;
            }
        }

        void OnCameraPreviewTapped(object sender, EventArgs e)
        {
            if (uiCameraPreview.IsPreviewing)
            {
                uiCameraPreview.CaptureSession.StopRunning();
                uiCameraPreview.IsPreviewing = false;
            }
            else
            {
                uiCameraPreview.CaptureSession.StartRunning();
                uiCameraPreview.IsPreviewing = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.CaptureSession.Dispose();
                Control.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}