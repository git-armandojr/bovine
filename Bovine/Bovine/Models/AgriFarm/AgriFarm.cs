using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Models.AgriFarm
{   
    public class AgriFarm
    {

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("dateCreated")]
        public DateCreated dateCreated { get; set; }

        [JsonProperty("dateModified")]
        public DateModified dateModified { get; set; }

        [JsonProperty("name")]
        public Name name { get; set; }

        [JsonProperty("description")]
        public Description description { get; set; }

        [JsonProperty("location")]
        public Bovine.Models.AgriFarm.Location.Location location { get; set; }

        [JsonProperty("landLocation")]
        public Bovine.Models.AgriFarm.LandLocation.LandLocation landLocation { get; set; }

        [JsonProperty("address")]
        public Bovine.Models.AgriFarm.Adress.Address address { get; set; }

        [JsonProperty("hasAgriParcel")]
        public HasAgriParcel hasAgriParcel { get; set; }
    }

}
