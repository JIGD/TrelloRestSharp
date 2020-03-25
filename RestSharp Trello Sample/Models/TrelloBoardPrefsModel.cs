using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestSharp_Trello_Sample
{
    public class TrelloBoardPrefsModel
    {
        [JsonProperty("permissionLevel")]
        public string PermissionLevel { get; set; }

        [JsonProperty("hideVotes")]
        public string HideVotes { get; set; }

        [JsonProperty("voting")]
        public string Voting { get; set; }

        public TrelloBoardPrefsModel(IRestResponse createResponse)
        {
            TrelloListBasicModel values = new JsonDeserializer().Deserialize<TrelloListBasicModel>(createResponse);
        }
    }
}
