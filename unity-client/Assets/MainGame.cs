using Assets.GameServer;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainGame : MonoBehaviour
{
    public GameData _gameData;

    private float nextActionTime = 0.0f;
    public float period = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            // execute block of code here
            if (_gameData != null)
            {
                var gameStatus = new GameServerApiClient().GetGameStatus(_gameData.GameId, _gameData.PlayerId);
                Debug.Log("GameStatus: " + gameStatus.GameState.ToString() + ", NumberOfRoundsPlayed:" + gameStatus.NumberOfRoundsPlayed);
            }
        }
    }

    void OnEnable()
    {
        _gameData = new GameData();
        var gameIdValue = PlayerPrefs.GetString("gameData.gameId");
        var playerIdValue = PlayerPrefs.GetString("gameData.playerId");

        if (string.IsNullOrEmpty(gameIdValue))
        {
            Debug.Log("Loaded dummy GameData...");
            _gameData.GameId = System.Guid.NewGuid();
            _gameData.PlayerId = System.Guid.NewGuid();
        }
        else 
        { 
            _gameData.GameId = new System.Guid(gameIdValue);
            _gameData.PlayerId = new System.Guid(playerIdValue);
        }
        Debug.Log("GameData: \n" + JsonConvert.SerializeObject(_gameData));
    }
}
