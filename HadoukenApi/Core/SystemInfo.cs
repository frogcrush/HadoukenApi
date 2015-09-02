using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hadouken.Core
{
    
    public class SystemInfo
    {
        [JsonProperty(PropertyName = "commitish")]
        public string Commitish { get; set; }

        [JsonProperty(PropertyName = "branch")]
        public string Branch { get; set; }

        [JsonProperty(PropertyName = "versions")]
        public Dictionary<string, string> Versions { get; set; }



        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
