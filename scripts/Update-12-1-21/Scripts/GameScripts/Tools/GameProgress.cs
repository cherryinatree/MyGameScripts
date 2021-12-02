using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameProgress
{
    public int gold;
    public int buildingMaterials;
    public int days;
    public string sceneName;
    public int questsCompleted;
    public int mainStoryMilesonesCompleted;

    public GameProgress()
    {
        gold = 0;
        buildingMaterials = 0;
        days = 1;
        sceneName = "HomeScene";
        questsCompleted = 0;
        mainStoryMilesonesCompleted = 0;

        
    }
}
