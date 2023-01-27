using Assets.GameServer;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    int selectedMenuItem = 0;

    public List<TextMeshPro> menuItems;

    // Start is called before the first frame update
    void Start()
    {
        menuItems[0].color = Color.blue;
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
            //SceneManager.LoadScene("");
            new GameServerApiClient().StartNewGame();
        }

        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i == selectedMenuItem)
            {
                menuItems[i].color = Color.blue;
            }
            else
            {
                menuItems[i].color = Color.white;
            }
        }
    }
}
