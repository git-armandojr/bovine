using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriParcel.Location
{
    public class Location
    {

        [JsonProperty("value")]
        public Value value { get; set; }
    }
}
