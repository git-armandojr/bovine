using Newtonsoft.Json;
using Plugin.Geolocator.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models
{
    public class Pasture
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [TextBlob("LandLocationBlobbed")]
        public List<Position> Location
        {
            get => JsonConvert.DeserializeObject<List<Position>>(LocationBlobbed);
            set { if (value != null) LocationBlobbed = JsonConvert.SerializeObject(value); }
        }

        public string LocationBlobbed { get; set; }

        public double Area { get; set; }

        public string Description { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Cattle> Cattles { get; set; }

        [ForeignKey(typeof(Farm))]
        public int FarmID { get; set; }
    }
}
