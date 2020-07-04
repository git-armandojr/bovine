using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriParcel.Location
{
    public class Value
    {

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("coordinates")]
        public IList<IList<IList<double>>> coordinates { get; set; }

    }
}
