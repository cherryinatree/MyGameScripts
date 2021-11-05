using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIcontroller
{
    protected static GameObject menu = null;
    protected static GameObject statsDisplay = null;
    protected static GameObject actionMenu = null;
    protected static GameObject GameOverMenu = null;
    protected static GameObject InventoryMenu = null;
    protected static CanvasGroup menuAlpha = null;
    protected static CanvasGroup actionMenuAlpha = null;
    protected static CanvasGroup statsDisplayAlpha = null;
    protected static CanvasGroup GameOverAlpha = null;
    protected static CanvasGroup InventoryMenuAlpha = null;
    public static Text text;
    public Material moreRed;
    public Material moreYellow;

    public Sprite nullSprite = null;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public GUIcontroller()
    {
        menu = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        actionMenu = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        statsDisplay = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        GameOverMenu = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
        InventoryMenu = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        text = statsDisplay.transform.GetChild(1).GetComponent<Text>();
        menuAlpha = menu.GetComponent<CanvasGroup>();
        actionMenuAlpha = actionMenu.GetComponent<CanvasGroup>();
        statsDisplayAlpha = statsDisplay.GetComponent<CanvasGroup>();
        InventoryMenuAlpha = InventoryMenu.GetComponent<CanvasGroup>();
        GameOverAlpha = GameOverMenu.GetComponent<CanvasGroup>();
        GameOverMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void GuiUpdate(cube currentCube)
    {
        PauseMenu();
        GoToMainMenu();
        //NextLevel();
        ActionMenu();
        checkCursorPosition(currentCube);
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuAlpha.alpha == 1) menuAlpha.alpha = 0;
            else menuAlpha.alpha = 1;
        }
    }

    public void GoToMainMenu()
    {
        if (menuAlpha.alpha == 1)
        {
            badGuyTurnController.turnQueue.Clear();
            CharacterMain.theGoodGuys.Clear();
            CharacterMain.theBadGuys.Clear();
            cube.allTheCubes.Clear();
            cube.ActionCubes.Clear();
            Application.LoadLevel(0);
        }
    }

    public void RestartLevel()
    {
        badGuyTurnController.turnQueue.Clear();
        CharacterMain.theGoodGuys.Clear();
        CharacterMain.theBadGuys.Clear();
        cube.allTheCubes.Clear();
        cube.ActionCubes.Clear();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void NextLevel(GameData data)
    {
        if (GameOverAlpha.alpha == 1)
        {
            data.UpdateStats(CharacterMain.theGoodGuysForSavingStats[0].GetComponent<CharacterMain>().myStats, 
                CharacterMain.theGoodGuysForSavingStats[1].GetComponent<CharacterMain>().myStats,
                CharacterMain.theGoodGuysForSavingStats[2].GetComponent<CharacterMain>().myStats);
            Save.SaveGame("AutoSave", data);
            badGuyTurnController.turnQueue.Clear();
            CharacterMain.theGoodGuys.Clear();
            CharacterMain.theGoodGuysStats.Clear();
            CharacterMain.theGoodGuysForSavingStats.Clear();
            CharacterMain.theBadGuys.Clear();
            cube.allTheCubes.Clear();
            cube.ActionCubes.Clear();
            SceneManager.LoadScene("NewTown");
        }
    }


    public void ActionMenu()
    {
        if (actionMenuAlpha.alpha == 1) actionMenuAlpha.alpha = 0;
        else actionMenuAlpha.alpha = 1;

    }

    public void checkCursorPosition(cube currsorCube)
    {
        statsDisplayAlpha.alpha = 0;
        foreach (GameObject a in CharacterMain.theGoodGuys)
        {
            if (currsorCube.gameObject == a.GetComponent<CharacterMain>().my_cube)
            {
                //Debug.Log(a.GetComponent<CharacterMain>().myStats);
                text.text = "Lvl: " + a.GetComponent<CharacterMain>().myStats.Level + "\t\tHP: " + a.GetComponent<CharacterMain>().HealthPoints
                    + "/" + a.GetComponent<CharacterMain>().myStats.MaxHealthPoints
                    + "\nXP: " + a.GetComponent<CharacterMain>().myStats.Experience;
                statsDisplay.transform.GetChild(2).GetComponent<Image>().enabled = true;
                statsDisplay.transform.GetChild(3).GetComponent<Image>().enabled = true;
                if (a.GetComponent<CharacterMain>().character.myState > Combat.BATTLESTATE.ACTION1)
                {
                    statsDisplay.transform.GetChild(3).GetComponent<Image>().color = Color.black;
                }
                else
                {
                    statsDisplay.transform.GetChild(3).GetComponent<Image>().color = Color.blue;
                }
                if (a.GetComponent<CharacterMain>().character.myState > Combat.BATTLESTATE.ACTION2)
                {
                    statsDisplay.transform.GetChild(2).GetComponent<Image>().color = Color.black;
                }
                else
                {
                    statsDisplay.transform.GetChild(2).GetComponent<Image>().color = Color.blue;
                }
                statsDisplay.GetComponent<Image>().material = moreRed;
                statsDisplay.GetComponent<Image>().color = Color.red;
                statsDisplay.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                statsDisplay.transform.GetChild(0).GetComponent<Image>().material = moreYellow;
                statsDisplay.GetComponent<CanvasGroup>().alpha = 1;
                
            }
        }
        foreach (GameObject a in CharacterMain.theBadGuys)
        {
            if (a != null)
            {
                if (currsorCube.gameObject == a.GetComponent<CharacterMain>().my_cube)
                {
                    text.text = "HP: " + a.GetComponent<CharacterMain>().HealthPoints + "/" + a.GetComponent<CharacterMain>().myStats.MaxHealthPoints;
                    statsDisplay.transform.GetChild(2).GetComponent<Image>().enabled = false;
                    statsDisplay.transform.GetChild(3).GetComponent<Image>().enabled = false;
                    statsDisplay.GetComponent<Image>().material = moreRed;
                    statsDisplay.GetComponent<Image>().color = Color.red;
                    statsDisplay.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                    statsDisplay.transform.GetChild(0).GetComponent<Image>().material = moreYellow;
                    statsDisplay.GetComponent<CanvasGroup>().alpha = 1;
                }
            }
        }
    }
    



    public void InventoryActivate(GameObject guy)
    {
        Cursor.visible = true;
        InventoryMenuAlpha.alpha = 1;
        for (int i = 0; i < InventoryMenu.transform.childCount; i++)
        {
            if (guy.GetComponent<CharacterMain>().character.allMySpells != null)
            {
                if (guy.GetComponent<CharacterMain>().character.allMySpells.Count > i)
                {
                    InventoryMenu.transform.GetChild(i).GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
                    InventoryMenu.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = guy.GetComponent<CharacterMain>().character.allMySpells[i].GetComponent<Spell>().myImage;
                    if (guy.GetComponent<CharacterMain>().character.allMySpells[i].GetComponent<Spell>().myClass == Spell.SPELLCLASS.RANGE)
                    {
                        guy.GetComponent<CharacterMain>().character.loadedSpell = guy.GetComponent<CharacterMain>().character.allMySpells[i];
                    }
                    if (guy.GetComponent<CharacterMain>().character.allMySpells[i].GetComponent<Spell>().myClass == Spell.SPELLCLASS.HEALING)
                    {
                        InventoryMenu.transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
                        InventoryMenu.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(guy.GetComponent<CharacterMain>().HealSelf);
                    }
                }
                else
                {
                    InventoryMenu.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = nullSprite;
                    InventoryMenu.transform.GetChild(i).GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                }
            }
            else
            {
                InventoryMenu.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = nullSprite;
                InventoryMenu.transform.GetChild(i).GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            }
        }

    }
    
    public void DoNothing()
    {

        Cursor.visible = false;
        InventoryMenuAlpha.alpha = 0;
        actionMenuAlpha.alpha = 0;
    }
}
