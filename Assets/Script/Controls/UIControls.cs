using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControls : MonoBehaviour {
    public Button home;
    public Button restart;
    public Button config;
    public GameObject configPanel;
    public Toggle use2Decks;
    public Slider moveNum;

    public void HomeBtn() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void RestartBtn() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ConfigBtn() {
        GameConfig.pause = true;
        home.interactable = false;
        restart.interactable = false;
        config.interactable = false;

        configPanel.SetActive(true);

        if (GameConfig.deckNum == 1)
            use2Decks.isOn = false;
        else
            use2Decks.isOn = true;

        moveNum.value = GameConfig.cardMove;

    }

    public void ConfigPanelBackBtn() {
        configPanel.SetActive(false);
        home.interactable = true;
        restart.interactable = true;
        config.interactable = true;
        GameConfig.pause = false;
    }

    public void ConfigPanelOKBtn() {
        if (!use2Decks.isOn)
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
        GameConfig.cardMove = (int)moveNum.value;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
