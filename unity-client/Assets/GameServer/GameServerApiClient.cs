using GameServer;
using Newtonsoft.Json;
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

        public GameStateResponse StartNewGame()
        {
            IGameServerClient client = new GameServerClient(_httpClient);
            string gameId = "1";
            var result = client.GetGameState(gameId);
            var resultAsJSon = JsonConvert.SerializeObject(result);
            Debug.Log("Response from server: \n" + resultAsJSon);
            return result;
        }
    }
}
