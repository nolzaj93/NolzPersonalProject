/*
 *  Austin Nolz COP 2001 Personal Project, learning/testing with ARKit,Vision
 */

using Xamarin.Forms;

namespace JointAnalysis
{
    public class MainPageCS : ContentPage
    {
        public MainPageCS()
        {
            //Source: https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/View
            // Code behind for the custom camera renderer.
            Title = "Main Page";
            Padding = new Thickness(0, 20, 0, 0);
            Content = new StackLayout
            {
                Children = {
                    new CamPreview {
                        Camera = CameraOptions.Rear,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
                }
            };
        }
    }
}

