using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TypeAhead.Models
{
    public class Modification
    {
        [JsonProperty(PropertyName = "PropertyName")]
        public string PropertyName{ get; set; }
        public string OriginalValue { get; set; }
        public string CurrentValue { get; set; }
    }
}