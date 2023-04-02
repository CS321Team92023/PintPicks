using Android.Widget;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace PintPicks;

public partial class MainPage : ContentPage {

    private string accessKey, secretKey, bucketName;

    public MainPage() {
        InitializeComponent();


        //Set the values for AWS Information
        accessKey = "AKIAVFY5HTAZGTSGIUES";
        secretKey = "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";
        bucketName = "pintmenus";
    }
    /* Handles "Choose Picture" button being clicked
     */
    private async void OnChoosePictureClicked(object sender, EventArgs e) {

        //Button Animation
        await imageButton.ScaleTo(1.2, 100, Easing.CubicOut);
        await imageButton.ScaleTo(1, 100, Easing.CubicIn);


        try {
            //Media Picker code
            var file = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "image/*" } },
                }),
                PickerTitle = "Please select an image",
            });

            if (file != null) {
                var stream = await file.OpenReadAsync();
                var fileName = Path.GetFileName(file.FileName);

                //Uploads photo to S3 Bucket
                var transferUtility = new TransferUtility(new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.USEast1));
                await transferUtility.UploadAsync(stream, bucketName, fileName);
            }

        }
        catch (Exception) {

            await DisplayAlert("Error", "There was an error selecting an image, please try using the camera instead", "Exit");

        }

    }

    /* Handles "Take Picture" button being clicked
     */
    private async void OnTakePictureClicked(object sender, EventArgs e) {

        //Button Animation
        await imageButton2.ScaleTo(1.2, 100, Easing.CubicOut);
        await imageButton2.ScaleTo(1, 100, Easing.CubicIn);


        try {
            //Media Picker code
            var photo = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
            {
                Title = "Take Picture",
            });

            if (photo != null) {
                var stream = await photo.OpenReadAsync();
                var fileName = Path.GetFileName(photo.FileName);

                //Uploads photo to S3 Bucket
                var transferUtility = new TransferUtility(new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.USEast1));
                await transferUtility.UploadAsync(stream, bucketName, fileName);
            }
        }
        catch (Exception) {
            
            await DisplayAlert("Error", "There was an error using the camera, please try selecting an image instead", "Exit");
        }



    }


}
