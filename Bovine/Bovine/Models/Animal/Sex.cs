using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.Animal
{
    public class Sex
    {

        [JsonProperty("value")]
        public string value { get; set; }
    }
}
