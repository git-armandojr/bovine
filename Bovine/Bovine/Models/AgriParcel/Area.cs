using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriParcel
{
    public class Area
    {

        [JsonProperty("value")]
        public double value { get; set; }
    }
}
