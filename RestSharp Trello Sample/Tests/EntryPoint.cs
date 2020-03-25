using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using RestSharp_Trello_Sample.Clients;
using System.Collections.Generic;
using System.Net;

namespace RestSharp_Trello_Sample
{
    [TestFixture]
    class EntryPoint
    {
        public TrelloClient trelloClient = new TrelloClient();

        public string boardId = "";
        public string boardName = "";
        public string listId = "";
        public string listName = "";
        public string cardId = "";
        public string cardName = "";

        [Test, Order(1)]
        public void CreateBoard()
        {
            string boardName = "My Amazing Board";
            IRestResponse boardCreationResponse = trelloClient.createBoard(boardName);

            TrelloBoardBasicModel boardValues = new TrelloBoardBasicModel(boardCreationResponse);
            //Might want to remove this and use a get for further tests.
            boardId = boardValues.Id;
            boardId = boardValues.Name;
            Assert.AreEqual(HttpStatusCode.OK, boardCreationResponse.StatusCode);
            Assert.AreEqual(boardName, boardValues.Name);
            Assert.AreEqual("private", boardValues.Prefs[0].PermissionLevel);
            Assert.False(boardValues.Closed);
        }

        [Test, Order(2)]
        public void CreateList()
        {
            string listName = "My Amazing List";
            IRestResponse listCreationResponse = trelloClient.createList(boardId, listName);

            TrelloListBasicModel listValues = new JsonDeserializer().Deserialize<TrelloListBasicModel>(listCreationResponse);

            listId = listValues.Id;
            listName = listValues.Name;

            Assert.AreEqual(HttpStatusCode.OK, listCreationResponse.StatusCode);
            Assert.AreEqual(listName, listValues.Name);
            Assert.False(listValues.Closed);
        }

        [Test, Order(3)]
        public void AddingACard()
        {
            string cardName = "My Amazing Card";
            IRestResponse cardAdditionResponse = trelloClient.addCardToList(boardId, cardName, listId);

            TrelloCardBasicModel cardValues = new TrelloCardBasicModel(cardAdditionResponse);

            cardId = cardValues.Id;
            cardName = cardValues.Name;

            Assert.AreEqual(HttpStatusCode.OK, cardAdditionResponse.StatusCode);
            Assert.AreEqual(cardName, cardValues.Name);
            Assert.AreEqual(false, cardValues.Closed);
            Assert.AreEqual(listId, cardValues.IdList);
            Assert.AreEqual(boardId, cardValues.IdBoard);
            Assert.Zero(cardValues.Badges[0].Votes);
            Assert.Zero(cardValues.Badges[0].Attachments);
        }

        [Test, Order(4)]
        public void UpdateCard()
        {
            string cardName = "My Amazing Card Updated!";
            Dictionary<string, string> extraParams = new Dictionary<string, string> {
                {"name", cardName }
            };

            IRestResponse cardAdditionResponse = trelloClient.updateCard(boardId, listId, cardId, extraParams);

            TrelloCardBasicModel cardValues = new TrelloCardBasicModel(cardAdditionResponse);

            Assert.AreEqual(HttpStatusCode.OK, cardAdditionResponse.StatusCode);
            Assert.AreEqual("My Amazing Card Updated!", cardValues.Name);
            Assert.AreEqual(false, cardValues.Closed);
            Assert.AreEqual(listId, cardValues.IdList);
            Assert.AreEqual(boardId, cardValues.IdBoard);
            Assert.Zero(cardValues.Badges[0].Votes);
            Assert.Zero(cardValues.Badges[0].Attachments);
        }

        [Test, Order(5)]
        public void DeleteBoard()
        {
            IRestResponse boardDeletionResponse = trelloClient.deleteBoard(boardId);
            TrelloBoardBasicModel boardValues = new TrelloBoardBasicModel(boardDeletionResponse);

            Assert.IsNull(boardValues._Value);
        }

        [Test, Order(6)]
        public void GetBoard()
        {
            string messageNotFound = "The requested resource was not found.";

            IRestResponse getBoardResponse = trelloClient.getBoard(boardId);

            TrelloBoardBasicModel boardValues = new TrelloBoardBasicModel(getBoardResponse);

            Assert.AreEqual(HttpStatusCode.NotFound, getBoardResponse.StatusCode);
            Assert.AreEqual(messageNotFound, getBoardResponse.Content);
        }
    }
}
