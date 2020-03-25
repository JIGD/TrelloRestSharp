using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestSharp_Trello_Sample
{
    public class TrelloBoardBasicModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("prefs")]
        public List<TrelloBoardPrefsModel> Prefs { get; set; }

        [JsonProperty("_value")]
        public object? _Value { get; set; }

        public TrelloBoardBasicModel(IRestResponse createResponse) {
            TrelloBoardBasicModel values = new JsonDeserializer().Deserialize<TrelloBoardBasicModel>(createResponse);
        }
    }
}
