using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class Save
{
    private static string _lastLoadedFile;
    public static void SaveGame(string whichFile, GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "C:\\Users\\cherr\\OneDrive\\Desktop\\my_game_2\\saves\\"+ whichFile+".fun";
        FileStream stream;
        try
        {
            stream = new FileStream(path, FileMode.Create);
        }
        catch (FileLoadException e)
        {

            stream = new FileStream(path, FileMode.CreateNew);
        }
        GameData theData = data;
        Debug.Log(data);


        formatter.Serialize(stream, theData);
        stream.Close();
    }

    public static void QuickNewSave()
    {
        float x = 0;
        DirectoryInfo directory = new DirectoryInfo("C:\\Users\\cherr\\OneDrive\\Desktop\\my_game_2\\saves");
        string path = "C:\\Users\\cherr\\OneDrive\\Desktop\\my_game_2\\saves";
        FileInfo[] files = directory.GetFiles();
        foreach (FileInfo file in files)
        {
            x++;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        path = "C:\\Users\\cherr\\OneDrive\\Desktop\\my_game_2\\saves\\save" + x.ToString() + ".fun";
        FileStream stream;
        _lastLoadedFile = "save" + x.ToString();
        try
        {
            stream = new FileStream(path, FileMode.Create);
        }
        catch (FileLoadException e)
        {

            stream = new FileStream(path, FileMode.CreateNew);
        }

        GameData data = new GameData();
        data.CreateNewGameData();
        Debug.Log(data.char1);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public GameData LoadGameData(string whichFile)
    {
        string path = "C:\\Users\\cherr\\OneDrive\\Desktop\\my_game_2\\saves\\" + whichFile + ".fun";
        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                GameData data = formatter.Deserialize(stream) as GameData;

                stream.Close();
                Debug.Log(whichFile);
                return data;
            }
            catch (FileNotFoundException e)
            {
                // Debug.Log(e);
                GameData newData = new GameData();
                newData.CreateNewGameData();
                return newData;
            }

        }
        else
        {
            Debug.Log("Save file not found in " + path);
            GameData newData = new GameData();
            newData.CreateNewGameData();
            //Debug.Log("newData " + newData);
            return newData;
        }
    }

    /*public void FindSaveGames()
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
            loadFile.transform.localPosition = new Vector3(180, x, 0f);
            x -=35;
            DateTime creation = file.LastWriteTime;
            string[] theFileName = file.Name.Split('.');
            loadFile.transform.GetChild(3).GetComponent<Text>().text = creation + "\n  "+ theFileName[0];
        }
        
    }*/

    public void CreateNewGame()
    {
       // GameData game = new GameData();
        //game.CreateNewGameData();
        QuickNewSave();
    }

    public static string LastLoadedFile
    {
        get { return _lastLoadedFile; }
        set { _lastLoadedFile = value; }
    }
}

