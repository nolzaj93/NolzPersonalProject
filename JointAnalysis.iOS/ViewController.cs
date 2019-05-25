using ARKit;
using SceneKit;
using UIKit;
using Vision;

namespace JointAnalysis.iOS
{
    public class ViewController : UIViewController
    {

        public ViewController() : base("ViewController", null)
        {
        }

        //Source:: https://msdn.microsoft.com/en-us/magazine/mt830360.aspx
        //Starts an AR session and shows how to load 3d assets
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            startAR();
        }
        // Initialize AR scene view
        public void startAR()
        {
            // Create the scene view for displaying the 3-D scene
            ARSCNView sceneView = new ARSCNView();
            sceneView.Frame = View.Frame;
            View = sceneView;
            CreateARScene(sceneView);
            PositionScene(sceneView);
        }
        // Configure AR scene with 3-D object
        public void CreateARScene(ARSCNView sceneView)
        {
            // lLoading the 3-D asset from file
            var scene = SCNScene.FromFile("art.scnassets/ship");
            // Attaching the 3-D object to the scene
            sceneView.Scene = scene;
            // This is for debugging purposes
            sceneView.DebugOptions = ARSCNDebugOptions.ShowWorldOrigin |
              ARSCNDebugOptions.ShowFeaturePoints;
        }
        // Position AR scene
        public void PositionScene(ARSCNView sceneView)
        {
            // ARWorldTrackingConfiguration uses the back-facing camera,
            // tracks a device's orientation and position, and detects
            // real-world surfaces, and known images or objects
            using (var arConfiguration = new ARWorldTrackingConfiguration
            {
                PlaneDetection = ARPlaneDetection.Horizontal,
                LightEstimationEnabled = true
            })
            {
                // Run the AR session
                sceneView.Session.Run(arConfiguration, ARSessionRunOptions.ResetTracking);
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

