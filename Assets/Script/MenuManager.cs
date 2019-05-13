using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    public GameObject firstMenu;
    public GameObject configMenu;
    public Toggle deckNumConfig;
    public Slider deckMovNumConfig;
    private int gametype = 0;

    #region First Menu
    public void Play()
    {
        gametype = 1;
        firstMenu.SetActive(false);
        configMenu.SetActive(true);
    }
    #endregion

    #region Config Menu
    public void OK()
    {
        if (!deckNumConfig.isOn)
        {
            GameConfig.deckNum = 1;
            GameConfig.suitDeckpack = 1;
            GameConfig.columnsNum = 6;
        }
        else
        {
            GameConfig.deckNum = 2;
            GameConfig.suitDeckpack = 2;
            GameConfig.columnsNum = 7;
        }
        GameConfig.cardMove = (int)deckMovNumConfig.value;
        SceneManager.LoadScene(gametype, LoadSceneMode.Single);
    }

    public void Back()
    {
        configMenu.SetActive(false);
        firstMenu.SetActive(true);
    }
    #endregion
}
