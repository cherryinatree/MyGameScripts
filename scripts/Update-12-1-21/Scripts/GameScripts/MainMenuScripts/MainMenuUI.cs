using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;

public class MainMenuUI : MonoBehaviour
{
    public static string CurrentFile;

    public void Test()
    {

        Debug.Log("trigger1");
    }

   public void NewGame()
    {

        Debug.Log("trigger");
        SaveFile save = new SaveFile();

        Stats fighter = CharacterCreator.CreateCharacter(CharacterCreator.CHARACTERCLASS.Fighter);
        Stats wizard = CharacterCreator.CreateCharacter(CharacterCreator.CHARACTERCLASS.Wizard);
        Stats rogue = CharacterCreator.CreateCharacter(CharacterCreator.CHARACTERCLASS.Rogue);

        GameProgress game = new GameProgress();

        

        save.characterRoster.Add(fighter);
        save.characterRoster.Add(wizard);
        save.characterRoster.Add(rogue);

        save.characterBattleTeam.Add(fighter);
        save.characterBattleTeam.Add(wizard);
        save.characterBattleTeam.Add(rogue);

        save.game = game;

        save.battleMap = 0;
        save.enemies = 0;
        save.numberOfEnemies = 0;

        SaveNames theNames = SaveSystem.SaveNamesFinder.LoadTheNames("Names");
        string thisSaveName = "Save" + theNames.theSaveNames.Count.ToString();

        Debug.Log(thisSaveName);

        save.saveName = thisSaveName;
        theNames.theSaveNames.Add(thisSaveName);
        CurrentFile = thisSaveName;

        SaveSystem.SaveGame(thisSaveName, save);
        SaveSystem.SaveNamesFinder.SaveTheNames("Names", theNames);
        

        SceneController.ChangeScene("HomeScene");

    }
}
