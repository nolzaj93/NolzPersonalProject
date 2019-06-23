## COP2001 Personal Project Summer A 2019
The goal of this project was to learn by creating a Xamarin iOS app with future capability to be supported on Android and Windows mobile devices. For the FGCU Programming Methodology curriculum, we were tasked with creating a personal project within a selection of categories, and I chose to use C#. This application was made individually with about a year of experience programming. 


## Demonstration


## Documentation
This project is still in the design phase as I am still learning. Most of the current code is default from creating a Xamarin Forms project and sample code found on Github. 

## Diagrams
Coming soon.

## Getting Started
Download Visual Studio and click the green button to Open in Desktop or Download as a Zip and extract the project. Open the project within Visual Studio to analyze, run, test.

## Built With
Visual Studio for Mac 2019

## Contributing
At the current moment I am attempting to learn how to handle the orientation change event, and how to structure the XAML within landscape.

## Author
- Austin Nolz

## License
MIT License

Copyright (c) 2019 Austin Nolz

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

## Acknowledgments
- James Montemagno at Microsoft - [Github](https://github.com/jamesmontemagno)
- Valeriy Kovalenko - [Website](https://trailheadtechnology.com/vision-framework-for-face-landmarks-detection-using-xamarin-ios/)

## History


## Key Programming Concepts Utilized
Object-Oriented Programming, MVC Architecture, Cross-platform mobile development, 

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
- Document, and format (30 minutes 5/25)
- Total time logged: 7.5 hours

## Sprint Backlog 3
- Utilize Vision within Xamarin to detect a face (1 hour 5/31)
- Overlay an outline around the facial features (1 hour 5/31)
- https://trailheadtechnology.com/vision-framework-for-face-landmarks-detection-using-xamarin-ios/
- https://github.com/vecalion/Xamarin.VisionFrameworkFaceLandmarks
- Research open-source projects that have implemented face/joint detection (2 hours 6/1)
- https://www.learnopencv.com/head-pose-estimation-using-opencv-and-dlib/
- https://www.learnopencv.com/deep-learning-based-human-pose-estimation-using-opencv-cpp-python/
- Find and Study tutorials and documentation for using OpenCV within Xamarin iOS (1 hour 6/1)
- https://chamoda.com/how-to-use-opencv-with-xamarin-ios/
- https://github.com/chamoda/XamarinOpenCV
- https://trinnguyen.com/opencv-for-xamarin-ios/
- https://github.com/trinnguyen/xamarin.ios-opencv
- OpenCV API Reference: https://docs.opencv.org/3.0-beta/modules/refman.html
- Document, and format (15 minutes 6/1)
- Total time logged: 5.25 hours

## Sprint Backlog 4
- Research different methods to use C/C++ code for OpenCV within Xamarin Forms project (2 hours 6/7)
- https://docs.microsoft.com/en-us/xamarin/cross-platform/cpp/
- https://drthitirat.wordpress.com/2013/06/03/use-c-codes-in-a-c-project-wrapping-native-c-with-a-managed-clr-wrapper/
- Utilize OpenCV within Xamarin by following tutorial on creating static framework in Xcode and binding project (2 hours 6/7, 1 hour 6/8)
- https://chamoda.com/how-to-use-opencv-with-xamarin-ios/
- Attempted to follow a different tutorial to setup an Objective-C wrapper (static library) to call OpenCV functions, and a C# wrapper (Xamarin iOS Binding Project) (3 hours 6/8)
- https://trinnguyen.com/opencv-for-xamarin-ios/
- https://github.com/nolzaj93/OpenCVBindingLibrary
- Total time logged: 8 hours

## Sprint Backlog 5
- Research Microsoft tutorials for using C++ code within C# to call OpenCV functions(1 hour 6/14)
- https://www.youtube.com/watch?v=48CC0_Jc3_I
- Research how to customize UI using UIKit, UIViewController, and UINavigationController within AppDelegate (1 hour 6/14)
- https://docs.microsoft.com/en-us/xamarin/ios/app-fundamentals/ios-code-only?tabs=macos
- More reading and research on how to wrap C++ OpenCV framework into C# (2 hours 6/15)
- https://docs.microsoft.com/en-us/xamarin/cross-platform/cpp/
- Total time logged: 4 hours

## Sprint Backlog 6
- Pay Apple Developer fee to allow testing on iPhone (15 minutes 6/20)
- Research how to create UML diagram for a MVC project, including symbols for associations, dependencies, inheritance, aggregation, and composition. (1 hour 6/20)
- Test and debug the current sample project (2 hours 6/22).
- Create a gif of the program running (15 minutes 6/22)
- Total time logged: 3 hours 30 minutes

## Future tasks
- Determine if Xamarin is the best option for cross-platform mobile development. I will look into Python development with Kivy, Java development with the Gluon framework, and other cross-platform options. Another option is to spend the extra time to create a native Swift project for iOS and a native project for Android, which may be the best option because documentation and sample code are more abundant for the native languages.
- Build a virtual stick figure with OpenCV over a video
- Outline the torso, arms and legs within the image view
- Estimate joint positions
- Overlay markers over the joint positions
- Learn how to use OpenCV and ARKit to have the virtual skeleton move with the body in real time 
- Record a video of movement with overlay
- Start creating algorithms to calculate joint angles
- Start writing methods to analyze human movements
- Begin converting algorithms to code that recognize movements faults and ideal movement
