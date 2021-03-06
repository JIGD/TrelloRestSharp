﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharp_Trello_Sample.Clients
{
    internal class TrelloClient
    {
        private string trelloKey = "";
        private string trelloToken = "";
        RestClient trelloClient = new RestClient("https://api.trello.com/1");

        internal IRestResponse createBoard(string boardName, Dictionary<string, string> extraParams = null)
        {
            IRestRequest createBoardRequest = new RestRequest("/boards");
            createBoardRequest.Method = Method.POST;
            createBoardRequest.AddParameter("name", boardName);
            createBoardRequest.AddParameter("key", trelloKey);
            createBoardRequest.AddParameter("token", trelloToken);
            //Need to move below to extra params
            createBoardRequest.AddParameter("defaultLists", "false");
            //TODO: Above
            addExtraParams(extraParams, createBoardRequest);

            IRestResponse createBoardResponse = trelloClient.Execute(createBoardRequest);
            Console.WriteLine(createBoardResponse.Content);
            return createBoardResponse;
        }

        internal IRestResponse deleteBoard(string boardId)
        {
            IRestRequest deleteBoardRequest = new RestRequest("/boards/" + boardId);

            deleteBoardRequest.Method = Method.DELETE;
            deleteBoardRequest.AddParameter("key", trelloKey);
            deleteBoardRequest.AddParameter("token", trelloToken);

            IRestResponse deleteBoardResponse = trelloClient.Execute(deleteBoardRequest);
            return deleteBoardResponse;
        }

        internal IRestResponse createBoard()
        {
            IRestRequest createBoardRequest = new RestRequest("/boards");
            createBoardRequest.Method = Method.POST;
            createBoardRequest.AddParameter("name", "My Amazing Board");
            createBoardRequest.AddParameter("key", trelloKey);
            createBoardRequest.AddParameter("token", trelloToken);
            createBoardRequest.AddParameter("defaultLists", "false");

            IRestResponse createBoardResponse = trelloClient.Execute(createBoardRequest);
            Console.WriteLine(createBoardResponse.Content);
            return createBoardResponse;
        }

        internal IRestResponse createList(string boardId, string listName, Dictionary<string, string> extraParams = null)
        {
            IRestRequest createListRequest = new RestRequest("/boards/" + boardId + "/lists");

            createListRequest.Method = Method.POST;
            createListRequest.AddParameter("name", listName);
            createListRequest.AddParameter("key", trelloKey);
            createListRequest.AddParameter("token", trelloToken);
            addExtraParams(extraParams, createListRequest);

            IRestResponse createListResponse = trelloClient.Execute(createListRequest);
            return createListResponse;
        }

        internal IRestResponse addCardToList(string boardId, string cardName, string listId, Dictionary<string, string> extraParams = null)
        {
            IRestRequest addCardRequest = new RestRequest("/cards");

            addCardRequest.Method = Method.POST;
            addCardRequest.AddParameter("idBoard", boardId);
            addCardRequest.AddParameter("name", cardName);
            addCardRequest.AddParameter("idList", listId);
            //Need to add this to extra params in test
            addCardRequest.AddParameter("keepFromSource", "all");
            //TODO: Above
            addCardRequest.AddParameter("key", trelloKey);
            addCardRequest.AddParameter("token", trelloToken);
            addExtraParams(extraParams, addCardRequest);

            IRestResponse cardAdditionResponse = trelloClient.Execute(addCardRequest);
            return cardAdditionResponse;
        }

        internal IRestResponse updateCard(string boardId, string listId, string cardId, Dictionary<string, string> extraParams = null)
        {
            IRestRequest updateCardRequest = new RestRequest("/cards/" + cardId);

            updateCardRequest.Method = Method.PUT;            
            updateCardRequest.AddParameter("token", trelloToken);
            updateCardRequest.AddParameter("key", trelloKey);
            updateCardRequest.AddParameter("idBoard", boardId);
            updateCardRequest.AddParameter("idList", listId);
            addExtraParams(extraParams, updateCardRequest);

            IRestResponse updateCardResponse = trelloClient.Execute(updateCardRequest);
            return updateCardResponse;
        }

        internal IRestResponse getBoard(string boardId)
        {
            IRestRequest getBoardRequest = new RestRequest("/boards/" + boardId);

            getBoardRequest.Method = Method.GET;
            getBoardRequest.AddParameter("key", trelloKey);
            getBoardRequest.AddParameter("token", trelloToken);

            IRestResponse createResponse = trelloClient.Execute(getBoardRequest);
            return createResponse;
        }


        //Could be moved to utility class
        private void addExtraParams(Dictionary<string, string> extraParams, IRestRequest request)
        {
            if (extraParams != null)
            {
                foreach (KeyValuePair<string, string> param in extraParams)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            }
        }




    }
}
