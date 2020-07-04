using Android.Content.Res;
using Plugin.Geolocator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Bovine.ViewModels
{
    public class PinItemsSourcePageViewModel
    {
        private Position _position;
        int _pinCreatedCount = 0;
        readonly ObservableCollection<Location> _locations;

        public IEnumerable Locations => _locations;

        public ICommand AddLocationCommand { get; }
        public ICommand RemoveLocationCommand { get; }
        public ICommand ClearLocationsCommand { get; }
        public ICommand UpdateLocationsCommand { get; }
        public ICommand ReplaceLocationCommand { get; }

        public PinItemsSourcePageViewModel()
        {
            _locations = new ObservableCollection<Location>()
            {
                //new Location("New York, USA", "The City That Never Sleeps", new Position(40.67, -73.94)),
                //new Location("Los Angeles, USA", "City of Angels", new Position(34.11, -118.41)),
                //new Location("San Francisco, USA", "Bay City", new Position(37.77, -122.45))
            };

            AddLocationCommand = new Command(AddLocation);
            RemoveLocationCommand = new Command(RemoveLocation);
            ClearLocationsCommand = new Command(() => _locations.Clear());
            UpdateLocationsCommand = new Command(UpdateLocations);
            ReplaceLocationCommand = new Command(ReplaceLocation);
        }


        void AddLocation()
        {
            _locations.Add(NewLocation());
        }

        void RemoveLocation()
        {
            if (_locations.Any())
            {
                _locations.Remove(_locations.First());
            }
        }

        void UpdateLocations()
        {
            if (!_locations.Any())
            {
                return;
            }

            double lastLatitude = _locations.Last().Position.Latitude;
            foreach (Location location in Locations)
            {
                location.Position = new Position(lastLatitude, location.Position.Longitude);
            }
        }

        void ReplaceLocation()
        {
            if (!_locations.Any())
            {
                return;
            }

            _locations[_locations.Count - 1] = NewLocation();
        }

        Location NewLocation()
        {
            GetLocation();
            _pinCreatedCount++;
            return new Location(
                $"Pin {_pinCreatedCount}",
                $"Desc {_pinCreatedCount}", _position
                /*RandomPosition.Next(new Position(39.8283459, -98.5794797), 8, 19)*/);
        }

        private async void GetLocation()
        {            
            Plugin.Geolocator.Abstractions.Position position;
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

    }
}
