using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.Animal
{
    public class LegalID
    {

        [JsonProperty("value")]
        public string value { get; set; }
    }
}
