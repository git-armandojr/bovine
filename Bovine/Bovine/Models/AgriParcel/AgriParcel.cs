using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriParcel
{
    public class AgriParcel
    {

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("dateCreated")]
        public DateCreated dateCreated { get; set; }

        [JsonProperty("dateModified")]
        public DateModified dateModified { get; set; }

        [JsonProperty("location")]
        public Bovine.Models.AgriParcel.Location.Location location { get; set; }

        [JsonProperty("area")]
        public Area area { get; set; }

        [JsonProperty("description")]
        public Description description { get; set; }

        [JsonProperty("category")]
        public Category category { get; set; }

        [JsonProperty("hasAnimal")]
        public HasAnimal hasAnimal { get; set; }
    }
}
