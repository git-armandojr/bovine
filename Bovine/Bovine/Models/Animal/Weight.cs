using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.Animal
{
    public class Weight
    {

        [JsonProperty("value")]
        public float value { get; set; }
    }
}
