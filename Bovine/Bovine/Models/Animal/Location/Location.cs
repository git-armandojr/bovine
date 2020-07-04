using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.Animal.Location
{
    public class Location
    {

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("value")]
        public Value value { get; set; }
    }
}
