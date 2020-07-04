using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Bovine.Models.Geolocation
{
    public class Polygon
    {
        public const string type = "polygon";
        public List<Location> location = new List<Location>();

        public Polygon(List<Location> location)
        {
            this.location = location;
        }
    }
}
