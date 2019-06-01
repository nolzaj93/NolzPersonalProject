# NolzPersonalProject
## COP2001 Project at FGCU

## Problem Statement
  The problem is to create a program to add to my portfolio, which displays my skills to potential employers and my professor. The ultimate short-term goal within the next six weeks is to create a cross-platform mobile app that will originally run on iOS that is able to overlay an AR skeleton on a human body within a camera view. My boundaries are to develop software within the following categories: Android, Gaming, C#, Raspberry Pi, or iOS. The long-term goal is to use these technologies to analyze biomechanics while standing, walking, running, throwing and a library of other movements with a cross-platform application available on iOS, Android, and Windows. Short-term success would be to learn C# and use Xamarin to create a mobile application that runs on iOS, which uses OpenCV and ARKit to recognize the user’s face and construct a virtual skeleton over a picture or video. The main constraints are lack tutorial materials for Xamarin and the limited time available in the next six weeks to complete the project. Another constraint is that I do not own an Android device, so I may need to find a way to use a webcam with the Android simulator to test and develop for this platform. I am assuming that C# will have the capability of achieving the short-term goal to create a cross platform mobile application using Xamarin and implementing Augmented Reality/Computer Vision. Another assumption I am making is that I will be capable of learning how to solve this problem and achieving the short-term goal within this accelerated semester. The stakeholders are myself, my professor, and potential employers. The timeline for the completion of the short-term goal of completing a personal project with C# is six weeks. Each week will be a Sprint during which I have set incremental tasks described in the Sprint Backlogs below.

## Sprint Backlog 1
- Choose language and personal project goals (C# and joint detection on iOS using Xamarin in Visual Studio- 1 hour 5/17)
- Setup IDE and compiler on Windows and Mac (30 minutes 5/17)
- Download Xamarin framework (15 minutes 5/17)
- Setup Xam.Plugin.Media (15 minutes 5/18)
- Connect to Github (15 minutes 5/18)
- Start a project in Visual Studio and follow tutorials to request permissions to access camera (1 hour 5/18)
- Finish problem statement and plan out the remaining SBL tasks (30 minutes 5/18)
- Total time logged: 3 hours 45 minutes

## Sprint Backlog 2
- Read/watch tutorials for C#/Xamarin (1.5 hours 5/23)
- Study iOS Vision to learn how to implement face detection in C# (30 minutes 5/23, 2 hours 5/24)
- https://trailheadtechnology.com/vision-framework-for-face-landmarks-detection-using-xamarin-ios/
- Utilize tutorials from Microsoft to implement a basic camera (3 hours 5/25)
- https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/view
- https://github.com/xamarin/xamarin-forms-samples/tree/master/CustomRenderers/View
- Connect iPhone 7 over WiFi for debugging, fix USB recognition error on Macbook Pro (1 hour 5/25)
- https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/wireless-deployment?tabs=macos
- Debug, document, and format (2 hours 5/25)
- Total time logged: 9 hours

## Sprint Backlog 3
- Pay Apple Developer fee
- Utilize Vision within Xamarin to detect a face (1 hour 5/31)
- Overlay an outline around the facial features (1 hour 5/31)
- https://trailheadtechnology.com/vision-framework-for-face-landmarks-detection-using-xamarin-ios/
- https://github.com/vecalion/Xamarin.VisionFrameworkFaceLandmarks
- Research open-source projects that have implemented joint detection (2 hours 6/1)
- https://www.youtube.com/watch?v=mhJs6zIoBUM
- https://www.learnopencv.com/deep-learning-based-human-pose-estimation-using-opencv-cpp-python/
- Find and Study tutorials and documentation for OpenCV/Emgu CV
- https://chamoda.com/how-to-use-opencv-with-xamarin-ios/
- https://github.com/trinnguyen/xamarin.ios-opencv
- Debug, document, and format

## Sprint Backlog 4
- Utilize OpenCV/Emgu CV within Xamarin
- Build a virtual skeleton with ARKit and OpenCV over an image
- Outline the torso, arms and legs within the image view
- Estimate joint positions
- Overlay markers over the joint positions
- Debug, document, and format

## Sprint Backlog 5
- Learn how to use OpenCV and ARKit to have the virtual skeleton move with the body in real time 
- Record a video of movement with overlay
- Test different combinations of clothing and environments for accuracy
- Debug, document, and format

## Sprint Backlog 6
- Debug, improve documentation, and research ways to improve accuracy of joint detection
- Start creating algorithms to calculate joint angles
- Start writing methods to analyze human movements
- Begin coding algorithms that recognize movements faults and ideal movement
