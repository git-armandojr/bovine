using Bovine.Models;
using Bovine.Models.AgriFarm;
using Bovine.Models.AgriFarm.LandLocation;
using Bovine.Services;
using Bovine.ViewModels;
using Newtonsoft.Json;
using Plugin.Geolocator;
using SQLiteNetExtensions.Extensions.TextBlob;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Bovine.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PolygonMarkerPage : ContentPage
    {
        ObservableCollection<Position> positions = new ObservableCollection<Position>();
        private IList<IList<double>> points = new List<IList<double>>();

        readonly AgriFarmManager manager = new AgriFarmManager();

        Polygon polygon;
        bool lockedMarker = true;

        public PolygonMarkerPage()
        {
            InitializeComponent();

            BindingContext = new PinItemsSourcePageViewModel();
            GetLocation();
            buttonBuild.IsEnabled = false;

            try
            {
                AgriFarm agriFarm = App.SelectedAgriFarm;
                Title = "Fazenda " + agriFarm.name.value;
                points = agriFarm.landLocation.value.coordinates;
                if (points.Count > 0)
                {
                    foreach (IList<double> point in points)
                    {
                        positions.Add(new Position(point[0], point[1]));
                    }
                    CreatePolygon();
                    buttonRebuild.IsVisible = true;
                    buttonClear.IsVisible = false;
                    buttonSave.IsVisible = false;
                }
            }
            catch (NullReferenceException e)
            {
                Log.Warning("NullReferenceException", e.Message);
            }

                        
            
            /*
            // usar variável global App.SelectedFarm aqui
            Farm farm = App.FarmDatabase.GetFarmAsync(App.SelectedFarm.ID).Result;
            Title = "Fazenda " + farm.Name;
            if (!string.IsNullOrEmpty(farm.LandLocationBlobbed))
            {
                foreach (Position position in farm.LandLocation)
                    positions.Add(position);                
                CreatePolygon();
                buttonRebuild.IsVisible = true;
                buttonClear.IsVisible = false;
                buttonSave.IsVisible = false;
            }
            */

        }        

        #region Singleton
        //turn this page in a singleton class instance
        public static PolygonMarkerPage instance = null;
        private static readonly object _lock = new object();
        public static PolygonMarkerPage GetInstance()
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new PolygonMarkerPage();
                }

                return instance;
            }
        }
        #endregion

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"MapClick: {e.Position.Latitude}, {e.Position.Longitude}");

            if(lockedMarker)
            {
                positions.Add(e.Position);
                
                IList<double> point = new List<double>();
                point.Add(e.Position.Latitude);
                point.Add(e.Position.Longitude);

                points.Add(point);

                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = e.Position,
                    Label = string.Format("{0}, {1}", e.Position.Latitude.ToString(), e.Position.Longitude.ToString()),
                    //Address = "unknown",                
                };

                map.Pins.Add(pin);
                
                //MessagingCenter.Send(pin, "AddItem");
            }

            if (positions.Count == 1)
                buttonRemove.IsVisible = true;

            if (positions.Count > 2) 
            { 
                buttonBuild.IsEnabled = true;
                buttonSave.IsEnabled = true;
            }
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
                    _position = new Position(position.Latitude, position.Longitude);

                    map.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromMiles(0.04)));

                    //var pin = new Pin
                    //{
                    //    Type = PinType.Place,
                    //    Position = _position,
                    //    Label = string.Format("{0}, {1}", _position.Latitude.ToString(), _position.Longitude.ToString()),
                    //    //Address = "unknown",
                    //};

                    //map.Pins.Add(pin);

                    //map.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromMiles(0.04)));

                    //map.IsShowingUser = true;

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

        void OnBuildClicked(object sender, EventArgs e)
        {
            CreatePolygon();
        }

        void CreatePolygon()
        {
            polygon = new Polygon();
            polygon.StrokeWidth = positions.Count;
            polygon.StrokeColor = Color.FromHex("#1BA1E2");
            polygon.FillColor = Color.FromHex("#881BA1E2");

            foreach (Position position in positions)
            {
                polygon.Geopath.Add(position);
            }

            if (!map.MapElements.Contains(polygon))
            {
                map.MapElements.Add(polygon);
            }

            map.Pins.Clear();

            buttonBuild.IsVisible = false;
            buttonSave.IsVisible = true;
            buttonClear.IsVisible = true;

            lockedMarker = !lockedMarker;
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            map.MapElements.Remove(polygon);
            map.MapElements.Clear();
            map.Pins.Clear();
            positions.Clear();
            points.Clear();
            lockedMarker = !lockedMarker;
            buttonBuild.IsVisible = true;
            buttonBuild.IsEnabled = false;
            buttonSave.IsVisible = false;
            buttonRemove.IsVisible = false;
            buttonClear.IsVisible = false;
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            AgriFarm agriFarm = new AgriFarm();
            agriFarm.id = App.SelectedAgriFarm.id;
            Value value = new Value("Polygon", points);
            LandLocation landLocation = new LandLocation();
            landLocation.value = value;
            agriFarm.landLocation = landLocation;
            //App.SelectedAgriFarm.landLocation.value.type = "Polygon";
            //App.SelectedAgriFarm.landLocation.value.coordinates = points;            
            //agriFarm.landLocation = App.SelectedAgriFarm.landLocation;

            await manager.Update(agriFarm);          

            await Navigation.PopModalAsync();
        }

        private void OnRebuildClicked(object sender, EventArgs e)
        {
            map.MapElements.Remove(polygon);
            map.MapElements.Clear();
            map.Pins.Clear();
            positions.Clear();
            points.Clear();
            buttonRebuild.IsVisible = false;
            buttonBuild.IsVisible = true;
            buttonBuild.IsEnabled = false;
            lockedMarker = !lockedMarker;
        }

        private void OnRemoveClicked(object sender, EventArgs e)
        {
            int lastIndexPins = map.Pins.Count;
            int lastIndexPosition = positions.Count;

            if (lastIndexPins > 0 && lastIndexPosition > 0)
            {
                map.Pins.RemoveAt(--lastIndexPins);
                positions.RemoveAt(--lastIndexPosition);
            }
            
            if (lastIndexPins == 0 && lastIndexPosition == 0)
            {
                buttonRemove.IsVisible = false;
            }

            if (lastIndexPins < 3 && lastIndexPosition < 3)
                buttonBuild.IsEnabled = false;
            
        }


        protected override bool OnBackButtonPressed()
        {
            instance = null;
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            instance = null;
            base.OnDisappearing();
        }        
    }
}