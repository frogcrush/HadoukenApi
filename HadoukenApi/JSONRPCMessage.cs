using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hadouken
{
    public class JSONRPCMessage
    {
        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }
        [JsonProperty(PropertyName = "params", NullValueHandling = NullValueHandling.Include)]
        public string[] Params {get;set;}

        public JSONRPCMessage(string method, string[] _params = null)
        {
            Method = method;
            Params = _params;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    //{"jsonrpc":"2.0","result":{"commitish":"ee1b56f","branch":"master","versions":{"libtorrent":"1.0.5.0","hadouken":"5.2.0"}}}

    public class JSONRPCReader
    {
        public static string GetResult(string json)
        {
            JObject j = JObject.Parse(json);
            var res = j["result"];
            return res.ToString();
        }        

        public static T GetResult<T>(string json)
        {
            return (T)JsonConvert.DeserializeObject(GetResult(json), typeof(T));
        }
    }
}
