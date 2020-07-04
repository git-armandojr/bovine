using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriFarm.Location
{
    public class Value
    {

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("coordinates")]
        public IList<double> coordinates { get; set; }
        
    }
}
