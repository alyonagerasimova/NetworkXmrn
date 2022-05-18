using Android.OS;
using Android.Widget;

namespace Network.Droid.activities
{
    public class ChooseImageActivity: global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_images_upload);
        }
    }
}