
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Android;

using Android.Util;
using Android.Gms.Common;

namespace Bovine.Droid
{
    [Activity(Label = "Bovine", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";

        const int RequestLocationId = 0;

        readonly string[] LocationPermissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        protected override void OnStart()
        {
            base.OnStart();

            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                {
                    RequestPermissions(LocationPermissions, RequestLocationId);
                }
                else
                {
                    // Permissions already granted - display a message.
                }
            }            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }

            IsPlayServicesAvailable();
            CreateNotificationChannel();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);            
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //if (requestCode == RequestLocationId)
            //{
            //    if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
            //        return;
            //    // Permissions granted - display a message.
            //    else
            //        return;
            //    // Permissions denied - display a message.
            //}
            //else
            //{
            //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //    Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //}
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Log.Debug(TAG, GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Log.Debug(TAG, "This device is not supported");
                    Finish();
                }
                return false;
            }

            Log.Debug(TAG, "Google Play Services is available.");
            return true;
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channelName = CHANNEL_ID;
            var channelDescription = string.Empty;
            var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}