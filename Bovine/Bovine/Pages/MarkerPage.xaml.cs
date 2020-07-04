using Bovine.ViewModels;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Location = Bovine.ViewModels.Location;

namespace Bovine.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkerPage : ContentPage
    {
        ObservableCollection<Position> positions = new ObservableCollection<Position>();        
      
        public MarkerPage()
        {
            InitializeComponent();
            App.Positions.Clear();
            //BindingContext = this;
            //_locations = new ObservableCollection<Location>();

            BindingContext = new PinItemsSourcePageViewModel();
            //this.Title = App.SelectedFarm.Name;
            GetLocation();
            
            //AddLocationCommand = new Command(AddLocation);
            //NavigateCommand = new Command<Type>(async (Type pageType) =>
            //{                
            //    Page page = (Page)Activator.CreateInstance(pageType);
            //    //page.BindingContext = positions;
            //    await Navigation.PushAsync(page);
            //});

            
        }
        

        //#region Singleton
        ////turn this page in a singleton class instance
        //public static MarkerPage instance = null;
        //private static readonly object _lock = new object();
        //public static MarkerPage GetInstance()
        //{
        //    lock (_lock)
        //    {
        //        if (instance == null)
        //        {
        //            instance = new MarkerPage();
        //        }

        //        return instance;
        //    }
        //}
        //#endregion

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine($"MapClick: {e.Position.Latitude}, {e.Position.Longitude}");
            
            Pin pin = new Pin
            {
                Type = PinType.Place,
                Position = e.Position,
                Label = string.Format("{0}, {1}", e.Position.Latitude.ToString(), e.Position.Longitude.ToString()),
                //Address = "unknown",                
            };

            map.Pins.Add(pin);

            App.Positions.Add(e.Position);
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

        protected override bool OnBackButtonPressed()
        {
            //instance = null;
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            //instance = null;
            base.OnDisappearing();
        }

        async void OnBuildClicked(object sender, EventArgs e)
        {
            var page = new NavigationPage(new PasturePage());
            page.BarBackgroundColor = Color.FromHex("0078D7");
            await Navigation.PushModalAsync(page);

            //IList<Position> listPositions = new ObservableCollection<Position>(positions);
            //ViewModels.Polygon polygon = new ViewModels.Polygon
            //{
            //    StrokeWidth = 8,
            //    StrokeColor = Color.FromHex("#1BA1E2"),
            //    FillColor = Color.FromHex("#881BA1E2"),
            //    Geopath = listPositions
            //};

            //// add the polygon to the map's MapElements collection
            //map.MapElements.Add(polygon);
        }

        private void OnItemEditClicked(object sender, EventArgs e)
        {

        }
    }
}