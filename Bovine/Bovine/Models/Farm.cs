using Bovine.Models.Geolocation;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions.TextBlob;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace Bovine.Models
{
    public class Farm
    {
        private List<Position> landLocation;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //public const string Type = "AgriFarm";
        //public DateTime DateCreated { get; set; }
        //public DateTime DateModified { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Street { get; set; }

        public string Locality { get; set; }

        public string Country { get; set; }

        [TextBlob("LandLocationBlobbed")]
        public List<Position> LandLocation
        {
            get => JsonConvert.DeserializeObject<List<Position>>(LandLocationBlobbed);
            set { if(value != null) LandLocationBlobbed = JsonConvert.SerializeObject(value); }
        }

        public string LandLocationBlobbed { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Pasture> Pastures { get; set; }

        //public object Deserialize(string text, Type type)
        //{
        //    return JsonConvert.DeserializeObject(text, type);
        //}

        //public string Serialize(object element)
        //{
        //    return JsonConvert.SerializeObject(element);
        //}
        //public Point Location { get; set; }
        //public Polygon LandLocation { get; set; }
        //public Address Address { get; set; }
        //public ContactPoint ContactPoint { get; set; }
    }
}
