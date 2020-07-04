using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriParcel
{
    public class Category
    {

        [JsonProperty("value")]
        public string value { get; set; }
    }
}
