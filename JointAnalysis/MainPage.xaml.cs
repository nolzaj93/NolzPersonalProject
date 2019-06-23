/*
 *  Austin Nolz COP 2001 Personal Project, learning/testing with ARKit,Vision
 */

using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JointAnalysis
{

    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        ObservableCollection<MediaFile> files = new ObservableCollection<MediaFile>();
        //https://github.com/jamesmontemagno/MediaPlugin/blob/master/src/Media.Plugin.Sample/Media.Plugin.Sample/MediaPage.xaml.cs
        public MainPage()
        {
            InitializeComponent();

            files.CollectionChanged += Files_CollectionChanged;

            takePhoto.Clicked += async (sender, args) =>
            {
                files.Clear();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", "A camera was not detected.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                files.Add(file);
            };

            takeVideo.Clicked += async (sender, args) =>
            {
                files.Clear();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
                {
                    await DisplayAlert("No Camera", "A camera was not detected.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
                {
                    Name = "video.mp4",
                    Directory = "DefaultVideos",
                });

                if (file == null)
                    return;

                await DisplayAlert("Video Recorded", "Location: " + file.Path, "OK");

                file.Dispose();
            };

            //pickPhoto.Clicked += async (sender, args) =>
            //{
            //    if (!CrossMedia.Current.IsPickPhotoSupported)
            //    {
            //        await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
            //        return;
            //    }
            //    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            //    {
            //        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            //    });


            //    if (file == null)
            //        return;

            //    image.Source = ImageSource.FromStream(() =>
            //    {
            //        var stream = file.GetStream();
            //        file.Dispose();
            //        return stream;
            //    });
            //}
            //pickVideo.Clicked += async (sender, args) =>
            //{
            //    if (!CrossMedia.Current.IsPickVideoSupported)
            //    {
            //        await DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
            //        return;
            //    }
            //    var file = await CrossMedia.Current.PickVideoAsync();

            //    if (file == null)
            //        return;

            //    await DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
            //    file.Dispose();
            //};
        }
        private void Files_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (files.Count == 0)
            {
                ImageList.Children.Clear();
                return;
            }
            if (e.NewItems.Count == 0)
                return;

            var file = e.NewItems[0] as MediaFile;
            var image = new Image { WidthRequest = 300, HeightRequest = 300, Aspect = Aspect.AspectFit };
            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            ImageList.Children.Add(image);
        }

    }
}
