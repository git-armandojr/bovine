using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriFarm.Adress
{
    public class Value
    {

        [JsonProperty("addressLocality")]
        public string addressLocality { get; set; }

        [JsonProperty("addressCountry")]
        public string addressCountry { get; set; }

        [JsonProperty("streetAddress")]
        public string streetAddress { get; set; }
    }
}
