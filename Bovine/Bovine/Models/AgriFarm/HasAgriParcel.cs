using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriFarm
{
    public class HasAgriParcel
    {

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("value")]
        public IList<string> value { get; set; }
    }
}
