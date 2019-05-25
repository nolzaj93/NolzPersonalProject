using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(JointAnalysis.iOS.ARAppImpl))]
namespace JointAnalysis.iOS
{
    public class ARAppImpl : IARJointAnalysis
    {
        public void LaunchAR()
        {
            // This is in native code; invoke the native UI
            ViewController viewController = new ViewController();
            UIApplication.SharedApplication.KeyWindow.RootViewController.
              PresentViewController(viewController, true, null);
        }
    }
}