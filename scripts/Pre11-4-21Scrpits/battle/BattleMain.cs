using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMain : MonoBehaviour
{
    map_maker map;
    UserInterface theUI;
    Save save;
    GameData data;
    GUIcontroller theGUI;

    void Awake()
    {
        save = new Save();
        data = save.LoadGameData(Save.LastLoadedFile);
        map = new map_maker();
        map.MakeTheGoodGuys(data);
        theUI = new UserInterface();
        theUI.theData = data;
        //theGUI = new GUIcontroller();
    }


    void Update()
    {
        theUI.UiUpdate();
        //theGUI.GuiUpdate(UserInterface.current_cube);
    }
}
