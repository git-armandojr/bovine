using Bovine.Models.Animal;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Bovine.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GpsPage : ContentPage
    {
        //private Position _position;
        //private Plugin.Geolocator.Abstractions.Position _coordinates;
        ObservableCollection<Pin> pins = new ObservableCollection<Pin>();        

        public GpsPage()
        {            
            InitializeComponent();

            Mapa.ItemsSource = pins;
            BindingContext = App.SelectedAnimal;

            //MessagingCenter.Subscribe<Pin>(this, "AddItem", pin => { pins.Add(pin); });            


            //_coordinates.Latitude = GetCurrentPosition().Result.Latitude;
            //_coordinates.Longitude = GetCurrentPosition().Result.Longitude;

            //Xamarin.Forms.Maps.Position _position = new Xamarin.Forms.Maps.Position(GetCurrentPosition().Result.Latitude, GetCurrentPosition().Result.Longitude);            

            //if (IsLocationAvailable() && IsGeoLocationEnabled())

            GetLocation();            
        }

        /*
        public static async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentPosition()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    //got a cahched position, so let's use it.
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
            }

            if (position == null)
                return null;

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

            Debug.WriteLine(output);

            return position;
        }
        */

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetLocation();
        }

        private async void GetLocation()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            Position _position;
            try
            {
                var permissions = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                
                if (permissions != PermissionStatus.Granted)
                {
                    permissions = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();                    
                }

                if (permissions != PermissionStatus.Granted)
                {
                    return;
                }

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 10;

                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    if(App.SelectedAnimal == null)
                        _position = new Position(position.Latitude, position.Longitude);
                    else
                    {
                        _position = new Position(App.SelectedAnimal.location.value.coordinates[0], App.SelectedAnimal.location.value.coordinates[1]);
                        //double latitude = double.Parse(App.SelectedAnimal.location.value.coordinates[0], CultureInfo.InvariantCulture);
                        //double longitude = double.Parse(App.SelectedAnimal.location.value.coordinates[1], CultureInfo.InvariantCulture);
                        //_position = new Position(latitude, longitude);
                    }
                    

                    Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromMiles(0.04)));

                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = _position,
                        Label = string.Format("{0}, {1}", _position.Latitude.ToString(), _position.Longitude.ToString()),                        
                        //Address = "unknown",
                    };
                    
                    Mapa.Pins.Add(pin);

                    Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromMiles(0.04)));

                    Mapa.IsShowingUser = true;                    

                    return;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {
                throw ex;
                //Display error as we have timed out or can't get location.
            }

            _position = new Position(position.Latitude, position.Longitude);

            if (position == null)
                return;
        }

        private bool IsGeoLocationEnabled()
        {
            return CrossGeolocator.Current.IsGeolocationEnabled;
        }

        private bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        private void OnUnsubscribeButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<Pin>(this, "AddItem");
        }

        private void OnSubscribeButtonClicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<Pin>(this, "AddItem", pin => { pins.Add(pin); });
        }
    }
}