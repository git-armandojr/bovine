using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriParcel
{
    public class Description
    {

        [JsonProperty("value")]
        public string value { get; set; }
    }
}
