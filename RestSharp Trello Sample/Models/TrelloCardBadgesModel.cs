using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharp_Trello_Sample
{
    class TrelloCardBadgesModel
    {
        [JsonProperty("votes")]
        public int Votes { get; set; }

        [JsonProperty("attachments")]
        public int Attachments { get; set; }
        public TrelloCardBadgesModel(IRestResponse createResponse)
        {
            TrelloListBasicModel values = new JsonDeserializer().Deserialize<TrelloListBasicModel>(createResponse);
        }
    }
}
