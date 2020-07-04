using Bovine.Enums;
using Xamarin.Essentials;

namespace Bovine.Models.Geolocation
{
    public class Point
    {
        public const string type = "point";
        public Location location { get; set; }
        public Point(Location location)
        {
            this.location = location;
        }
    }
}
