using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class DynamicMenu : MonoBehaviour
{
    public void FindSaveGames()
    {
        float x = -2;
        DirectoryInfo directory = new DirectoryInfo("C:\\Users\\cherr\\OneDrive\\Desktop\\my_game_2\\saves");
        string path = "C:\\Users\\cherr\\OneDrive\\Desktop\\my_game_2\\saves";
        FileInfo[] files = directory.GetFiles();
        GameObject scrollLoad = GameObject.Find("ScrollViewLoad/Viewport/Content");
        GameObject loadBar = (GameObject)Resources.Load("prefabs/UiPrefabs/PanelSaveFile", typeof(GameObject));
        foreach (FileInfo file in files)
        {
            GameObject loadFile = Instantiate(loadBar);
            loadFile.transform.SetParent(scrollLoad.transform, false);
            loadFile.transform.localScale = new Vector3(0.15f, 0.06f, 0.1f);
            loadFile.transform.localPosition = new Vector3(100, x, 0f);
            x -= 35;
            DateTime creation = file.LastWriteTime;
            string[] theFileName = file.Name.Split('.');
            loadFile.transform.GetChild(3).GetComponent<Text>().text = creation + "\n  " + theFileName[0];
        }

    }

    public void MakeNewGame()
    {
        Save save = new Save();
        
        save.CreateNewGame();
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
