using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public Canvas theCanvas;
    protected static GameObject menu = null;
    protected static GameObject goldUI = null;
    CanvasGroup menuAlpha = null;
    public int myGold = 0;
    // Start is called before the first frame update
    void Start()
    {
        theCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        menu = theCanvas.transform.GetChild(2).gameObject;
        goldUI = theCanvas.transform.GetChild(3).gameObject;
        menuAlpha = menu.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        goldUI.transform.GetChild(0).GetComponent<Text>().text = myGold.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuAlpha.alpha == 0)
            {
                menuAlpha.alpha = 1;
                Cursor.visible = true;
            }
            else
            {

                Cursor.visible = false;
                menuAlpha.alpha = 0;
            }
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
}
