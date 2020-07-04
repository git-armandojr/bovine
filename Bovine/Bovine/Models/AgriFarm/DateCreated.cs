using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriFarm
{
    public class DateCreated
    {

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }
    }
}
