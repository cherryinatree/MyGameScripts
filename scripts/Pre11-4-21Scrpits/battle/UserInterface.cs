using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserInterface : GUIcontroller
{
    private Color yellow = Color.yellow;
    private Renderer rend;
    CharacterCube WhereTo;

    GameObject PC;
    
    public static cube current_cube;

    private Button firstButton;
    private static GameObject actionPanel;
    private GameObject theCamera;

    private bool actionPhase = false;
    public enum TURN { PLAYERTURN = 0, ENEMYTURN = 1, GAMEOVER= 2};
    public static TURN whosTurn = TURN.PLAYERTURN;

    public GameData theData;

    public UserInterface()
    {
        Time.timeScale = 1.0f;
        Time.timeScale *= 3;
        whosTurn = TURN.PLAYERTURN;
        theCamera = GameObject.Find("MainCamera");
        GameObject[] theCubes = GameObject.FindGameObjectsWithTag("StartCube");
        current_cube = theCubes[0].GetComponent<cube>();
        current_cube.selectedCube = true;
        rend = current_cube.GetComponent<Renderer>();
        checkCursorPosition(current_cube);
        // WhereTo = current_cube.GetComponent<CharacterCube>();
        WhereTo = new CharacterCube();
        
        firstButton = actionMenu.transform.GetChild(1).GetComponent<Button>();
        firstButton.Select();
        firstButton.OnSelect(null);

        LoadTheButtons();
    }

    public void LoadTheButtons()
    {
        GameObject.Find("moveButton").GetComponent<Button>().onClick.AddListener(delegate
        { moveOption(); });
        GameObject.Find("attackButton").GetComponent<Button>().onClick.AddListener(delegate
        { AttackOption(); });
        GameObject.Find("magicButton").GetComponent<Button>().onClick.AddListener(delegate
        { MagicOption(); });
        GameObject.Find("EndTurnButton").GetComponent<Button>().onClick.AddListener(delegate
        { EndRound(); });
        GameObject.Find("ContinueButton").GetComponent<Button>().onClick.AddListener(delegate
        { NextLevel(theData); });
    }


    // Update is called once per frame
    public void UiUpdate()
    {
        //Debug.Log(whosTurn);
        if (Input.GetKeyDown(KeyCode.P)) PauseGame();
        if (Input.GetKeyDown(KeyCode.O)) FastForward();
        if (whosTurn == TURN.PLAYERTURN)
        {
            //Debug.Log("move");
            CheckForInput();
            CheckIfRoundIsOver();
        }
        if (whosTurn != TURN.GAMEOVER)
        {
            CheckIfGameOver();
        }
    }


    public void GoToMainMenu()
    {

        badGuyTurnController.turnQueue.Clear();
        CharacterMain.theGoodGuys.Clear();
        CharacterMain.theBadGuys.Clear();
        cube.allTheCubes.Clear();
        cube.ActionCubes.Clear();
        Application.LoadLevel(0);

    }


    public void CheckForInput()
    {
        if (actionMenuAlpha.alpha == 0 && menuAlpha.alpha == 0)
        {
            MoveCursor();
            firstButton.Select();
            firstButton.OnSelect(null);
        }
        if (Input.GetKeyDown(KeyCode.T)) EndRound();
        if (Input.GetKeyDown(KeyCode.Space)) SelectAction();
        if (Input.GetKeyDown(KeyCode.Tab)) QuickCharacterSwitch();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeButtonPressed();
        }

        if (Input.anyKey)
        {
            checkCursorPosition(current_cube);
            current_cube.CubeColor();
        }
    }

    
    private void SelectAction()
    {

        if (current_cube.myPhase == cube.CUBEPHASE.ATTACK && current_cube != PC.GetComponent<CharacterMain>().my_cube.GetComponent<cube>())
        {
            PC.GetComponent<CharacterMain>().character.Attack(current_cube, PC);
        }
        if (current_cube.myPhase == cube.CUBEPHASE.MAGIC && current_cube != PC.GetComponent<CharacterMain>().my_cube.GetComponent<cube>())
        {
            PC.GetComponent<CharacterMain>().character.MagicAttack(current_cube, PC);
        }


        if (actionMenuAlpha.alpha == 0 && !actionPhase)
        {
            Debug.Log(CharacterMain.theGoodGuys.Count);
            foreach (GameObject a in CharacterMain.theGoodGuys)
            {
                if (a.GetComponent<CharacterMain>().my_cube == current_cube.gameObject &&
                    a.GetComponent<CharacterMain>().character.myState < Character.BATTLESTATE.OUTOFMOVES)
                {
                    firstButton.Select();
                    firstButton.OnSelect(null);
                    ActionMenu();
                }
            }
        }
        if (actionPhase && current_cube.myPhase != cube.CUBEPHASE.NORMAL)
        {
            CubeColorAction();
        }

        if (InventoryMenuAlpha.alpha == 1 && actionMenuAlpha.alpha == 0) InventoryMenuAlpha.alpha = 0;
    }


    void EscapeButtonPressed()
    {

        WhereTo.ResetVariables();
        if (menuAlpha.alpha == 0 && actionMenuAlpha.alpha == 0)
        {
            Cursor.visible = true;
            menuAlpha.alpha = 1;
        }
        else menuAlpha.alpha = 0;
        if (actionMenuAlpha.alpha == 1) actionMenuAlpha.alpha = 0;
        if (InventoryMenuAlpha.alpha == 1) InventoryMenuAlpha.alpha = 0;
        InventoryMenuAlpha.alpha = 0;
        Cursor.visible = false;
        actionPhase = false;

    }


    void PauseGame()
    {
        if(Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else { Time.timeScale = 1.0f; }
        Debug.Log(Time.timeScale);
    }
    
    void FastForward()
    {
        if (Time.timeScale < 3f)
        {
            Time.timeScale += 1f;
        }
        else
        {
            if (Time.timeScale < 80f)
            {
                Time.timeScale += 5f;
            }
        }
    }


    private void QuickCharacterSwitch()
    {
        Queue teamTab = new Queue();
        bool cursorOnCharacter = false;
        foreach (GameObject a in CharacterMain.theGoodGuys)
        {
            teamTab.Enqueue(a);
            if (current_cube == a.GetComponent<CharacterMain>().my_cube.GetComponent<cube>()) cursorOnCharacter = true;
        }

        if (cursorOnCharacter)
        {
            for (int i = 0; i < teamTab.Count * 2; i++)
            {

                GameObject currentGuy = (GameObject)teamTab.Dequeue();
                if (current_cube == currentGuy.GetComponent<CharacterMain>().my_cube.GetComponent<cube>())
                {
                    cube oldCube = current_cube;
                    GameObject nextGuy = (GameObject)teamTab.Dequeue();
                    current_cube = nextGuy.GetComponent<CharacterMain>().my_cube.GetComponent<cube>();
                    oldCube.selectedCube = false;
                    current_cube.selectedCube = true;
                    oldCube.CubeColor();
                    current_cube.CubeColor();
                    teamTab.Enqueue(nextGuy);
                    break;
                }
                else
                {
                    teamTab.Enqueue(currentGuy);
                }
            }
        }
        else
        {
            cube oldCube = current_cube;
            GameObject nextGuy = (GameObject)teamTab.Dequeue();
            current_cube = nextGuy.GetComponent<CharacterMain>().my_cube.GetComponent<cube>();
            oldCube.selectedCube = false;
            current_cube.selectedCube = true;
            oldCube.CubeColor();
            current_cube.CubeColor();
            teamTab.Enqueue(nextGuy);
        }
    }

    public void moveOption()
    {
        if (actionMenuAlpha.alpha == 1)
        {
            PC = WhereTo.MoveGoodGuy(current_cube);
            if (PC != null)
            {
                WhereTo.FindNeighborCubes(current_cube, cube.CUBEPHASE.MOVE, PC.GetComponent<CharacterMain>().character.myStats.MoveSpeed);
                actionPhase = true;
            }
            actionMenuAlpha.alpha = 0;
        }
    }

    public void AttackOption()
    {
        Debug.Log(current_cube.myPhase);
        if (actionMenuAlpha.alpha == 1)
        {
            PC = WhereTo.MoveGoodGuy(current_cube);
            if (PC != null)
            {
                WhereTo.FindNeighborCubes(current_cube, cube.CUBEPHASE.ATTACK, 1);
                actionPhase = true;
            }
            actionMenuAlpha.alpha = 0;
        }
    }

    public void MagicOption()
    {
        if (actionMenuAlpha.alpha == 1)
        {
            /*  PC = WhereTo.MoveGoodGuy(current_cube);
              if (PC != null)
              {
                  WhereTo.FindSquareNeighborCubes(current_cube, cube.CUBEPHASE.ATTACK, PC.GetComponent<CharacterMain>().attackRange);
                  actionPhase = true;
              }*/
            // actionPhase = true;
            foreach (GameObject a in CharacterMain.theGoodGuys)
            {
                if (current_cube.gameObject == a.GetComponent<CharacterMain>().my_cube)
                {
                    InventoryActivate(a);
                }
            }
           // actionMenuAlpha.alpha = 0;
        }
    }

    public void CastSpell()
    {
        Cursor.visible = false;
        if (actionMenuAlpha.alpha == 1)
        {
             PC = WhereTo.MoveGoodGuy(current_cube);
              if (PC != null)
              {
                  WhereTo.FindSquareNeighborCubes(current_cube, cube.CUBEPHASE.MAGIC, PC.GetComponent<CharacterMain>().attackRange);
                  actionPhase = true;
              }
             InventoryMenuAlpha.alpha = 0;
             actionMenuAlpha.alpha = 0;
        }
    }

    private void CubeColorAction()
    {
        if (current_cube.gameObject != PC.GetComponent<CharacterMain>().my_cube)
        {
            if (current_cube.myPhase == cube.CUBEPHASE.MOVE)
            {
                GameObject oldCube = PC.GetComponent<CharacterMain>().my_cube;
                PC.GetComponent<CharacterMain>().character.findPath(current_cube);
                 PC.GetComponent<CharacterMain>().my_cube = current_cube.gameObject;
                actionPhase = false;
                //PC.GetComponent<CharacterMain>().character.myState = Character.BATTLESTATE.TURNOVER;
                PC.GetComponent<CharacterMain>().character.myState += 1;
                CheckIfRoundIsOver();
                WhereTo.ResetVariables();
            }
            if (current_cube.myPhase == cube.CUBEPHASE.ATTACK)
            {
                actionPhase = false;
                PC.GetComponent<CharacterMain>().character.myState = Character.BATTLESTATE.TURNOVER;
                CheckIfRoundIsOver();
                WhereTo.ResetVariables();
            }
            if (current_cube.myPhase == cube.CUBEPHASE.MAGIC)
            {
                actionPhase = false;
                PC.GetComponent<CharacterMain>().character.myState = Character.BATTLESTATE.TURNOVER;
                CheckIfRoundIsOver();
                WhereTo.ResetVariables();
            }
        }
    }
    
    private void MoveCursor()
    {
        cube oldCube = current_cube;
        if (Input.GetKeyDown(KeyCode.S))
        {
            current_cube = WhereTo.MoveCube(-theCamera.transform.forward, current_cube);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("move");
            current_cube = WhereTo.MoveCube(theCamera.transform.forward, current_cube);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            current_cube = WhereTo.MoveCube(-theCamera.transform.right, current_cube);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            current_cube = WhereTo.MoveCube(theCamera.transform.right, current_cube);
        }
        oldCube.CubeColor();
    }

    private void CheckIfRoundIsOver()
    {
        int checkIfRoundIsOver = 0;
        foreach (GameObject a in CharacterMain.theGoodGuys)
        {
            if (a.GetComponent<CharacterMain>().character.myState < Character.BATTLESTATE.OUTOFMOVES)
            {
                checkIfRoundIsOver++;
            }
        }
        if (checkIfRoundIsOver == 0)
        {
            EndRound();
        }
    }


    public void EndRound()
    {
        if (menuAlpha.alpha == 1)
        {
            menuAlpha.alpha = 0;
        }

        foreach (GameObject a in CharacterMain.theBadGuys)
        {
            if (a != null)
            {
                a.GetComponent<CharacterMain>().character.myState = Character.BATTLESTATE.WAITING;
            }
        }
        badGuyTurnController.StartEnemyTurn();

        foreach (GameObject a in CharacterMain.theGoodGuys)
        {
            a.GetComponent<CharacterMain>().character.myState = Character.BATTLESTATE.ACTION1;
        }
    }

    void CheckIfGameOver()
    {
        int badGuyCount = 0;
        int GoodGuyCount = 0;

        foreach (GameObject a in CharacterMain.theBadGuys)
        {
            if (a != null)
            {
                badGuyCount++;
            }
        }

        foreach (GameObject a in CharacterMain.theGoodGuys)
        {
            if (a != null)
            {
                GoodGuyCount++;
            }
        }
        //Debug.Log(badGuyCount);
        if (GoodGuyCount == 0) GameOver("You Lose", false);
        if (badGuyCount == 0) GameOver("You Win", true);
    }

    public void GameOver(string text, bool nextLevel)
    {
        //GameData theData = new GameData();
      //  goodGuy.statCount = 0;
        Save.SaveGame(Save.LastLoadedFile, theData);

        Debug.Log("over");

        GameOverMenu.transform.GetChild(0).GetComponent<Text>().text = text;
        GameOverMenu.transform.GetChild(2).gameObject.SetActive(nextLevel);

        Cursor.visible = true;
        GameOverMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        whosTurn = TURN.GAMEOVER;
        GameOverAlpha.alpha = 1;
    }
}
