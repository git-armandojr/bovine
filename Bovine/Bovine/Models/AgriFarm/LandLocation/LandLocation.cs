using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriFarm.LandLocation
{
    public class LandLocation
    {

        [JsonProperty("value")]
        public Value value { get; set; }
    }
}
