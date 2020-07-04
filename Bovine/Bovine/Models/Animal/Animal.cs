using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.Animal
{
    public class Animal
    {

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("species")]
        public Species species { get; set; }

        [JsonProperty("legalID")]
        public LegalID legalID { get; set; }

        [JsonProperty("birthdate")]
        public Birthdate birthdate { get; set; }

        [JsonProperty("sex")]
        public Sex sex { get; set; }

        [JsonProperty("location")]

        public Bovine.Models.Animal.Location.Location location { get; set; }

        [JsonProperty("weight")]
        public Weight weight { get; set; }
    }
}
