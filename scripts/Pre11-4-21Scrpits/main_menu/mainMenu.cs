using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    public GameObject theMainMenu = null;
    public GameObject newGameMenu = null;
    public GameObject loadMenu = null;
    public GameObject settingsMenu = null;


    public void newGame()
    {
        theMainMenu.SetActive(false);
        newGameMenu.SetActive(true);
        loadMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    public void loadGame()
    {
        theMainMenu.SetActive(false);
        newGameMenu.SetActive(false);
        loadMenu.SetActive(true);
        settingsMenu.SetActive(false);
        DynamicMenu loadGames = new DynamicMenu();
        loadGames.FindSaveGames();
    }
    public void settingsForGame()
    {
        theMainMenu.SetActive(false);
        newGameMenu.SetActive(false);
        loadMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void backToMain()
    {
        theMainMenu.SetActive(true);
        newGameMenu.SetActive(false);
        loadMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    public void loadNewGame()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
