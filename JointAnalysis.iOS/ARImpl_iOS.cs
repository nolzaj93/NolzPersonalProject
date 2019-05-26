/*
 *  Austin Nolz COP 2001 Personal Project, learning/testing with ARKit,Vision
 */

using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(JointAnalysis.iOS.ARAppImpl))]
namespace JointAnalysis.iOS
{
    public class ARAppImpl : IARJointAnalysis
    {
        public void LaunchAR()
        {
            // This is in native code; invoke the native UI
            ARViewController viewController = new ARViewController();
            UIApplication.SharedApplication.KeyWindow.RootViewController.
              PresentViewController(viewController, true, null);
        }
    }
}