/*
 *  Austin Nolz COP 2001 Personal Project, learning/testing with ARKit,Vision
 */

using Xamarin.Forms;

namespace JointAnalysis
{
    //Source: https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/View
    public class CamPreview : View
    {

        public static readonly BindableProperty CameraProperty = BindableProperty.Create(
            "Camera",
            typeof(CameraOptions),
            typeof(CamPreview),
            CameraOptions.Rear);

        public CameraOptions Camera
        {
            get { return (CameraOptions)GetValue(CameraProperty); }
            set { SetValue(CameraProperty, value); }
        }
    }
}


