using Assets.GameServer;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int selectedMenuItem = 0;

    public List<TextMeshPro> _menuItems;

    public GameData _gameData;

    // Start is called before the first frame update
    void Start()
    {
        _menuItems[0].color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (selectedMenuItem < 2)
            {
                selectedMenuItem++;
            }
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (selectedMenuItem > 0)
            {
                selectedMenuItem--;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            if (selectedMenuItem == 0)
            {
                var newGameData = new GameServerApiClient().StartNewGame();
                _gameData = new GameData();
                _gameData.GameId = newGameData.GameId;
                _gameData.PlayerId = newGameData.PlayerId;
                SceneManager.LoadScene("MainGame");
            }
            else if (selectedMenuItem == 1)
            {

            }
            else if (selectedMenuItem == 2) 
            {
                Application.Quit();
            }
        }

        for (int i = 0; i < _menuItems.Count; i++)
        {
            if (i == selectedMenuItem)
            {
                _menuItems[i].color = Color.blue;
            }
            else
            {
                _menuItems[i].color = Color.white;
            }
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("gameData.gameId", _gameData.GameId.ToString());
        PlayerPrefs.SetString("gameData.playerId", _gameData.PlayerId.ToString());
    }
}
