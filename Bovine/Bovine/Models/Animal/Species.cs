using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.Animal
{
    public class Species
    {

        [JsonProperty("value")]
        public string value { get; set; }
    }
}
