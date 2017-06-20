using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTeaCompany.Model
{
    public class StoreInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

    }
}
