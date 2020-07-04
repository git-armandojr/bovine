using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriFarm
{
    public class Name
    {

        [JsonProperty("value")]
        public string value { get; set; }
    }
}
