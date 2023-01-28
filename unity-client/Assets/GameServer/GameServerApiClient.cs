using GameServer;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using UnityEngine;

namespace Assets.GameServer
{
    internal class GameServerApiClient
    {
        HttpClient _httpClient;

        public GameServerApiClient()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri("http://hamang-xps9500:57977/")
            };
        }

        public StartGameResponse StartNewGame()
        {
            IGameServerClient client = new GameServerClient(_httpClient);
            var result = client.StartGame(new StartGameRequest());
            var resultAsJSon = JsonConvert.SerializeObject(result);
            Debug.Log("Response from server: \n" + resultAsJSon);
            return result;
        }

        public GameStateResponse GetGameStatus(Guid? gameId, Guid? playerId)
        {
            IGameServerClient client = new GameServerClient(_httpClient);
            var result = client.GetGameState(gameId, playerId);
            var resultAsJSon = JsonConvert.SerializeObject(result);
            Debug.Log("Response from server: \n" + resultAsJSon);
            return result;
        }
    }
}
