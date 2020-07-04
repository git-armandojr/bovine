using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace Bovine.Models.AgriFarm.LandLocation
{
    public class Value
    {
        public Value(string type, IList<IList<double>> coordinates)
        {
            this.type = type;
            this.coordinates = coordinates;
        }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("coordinates")]
        public IList<IList<double>> coordinates { get; set; }
    }
}
