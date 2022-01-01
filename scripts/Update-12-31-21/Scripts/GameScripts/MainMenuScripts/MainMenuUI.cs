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

        Stats fighter = CharacterCreator.CreateCharacter(CharacterCreator.CHARACTERCLASS.Fighter, save.characterIDs);
        save.characterIDs++;
        Stats wizard = CharacterCreator.CreateCharacter(CharacterCreator.CHARACTERCLASS.Wizard, save.characterIDs);
        save.characterIDs++;
        Stats rogue = CharacterCreator.CreateCharacter(CharacterCreator.CHARACTERCLASS.Rogue, save.characterIDs);
        save.characterIDs++;

        GameProgress game = new GameProgress();

        save.characterRoster.Add(fighter);
        save.characterRoster.Add(wizard);
        save.characterRoster.Add(rogue);

        save.characterBattleTeam.Add(fighter);
        save.characterBattleTeam.Add(wizard);
        save.characterBattleTeam.Add(rogue);

        save.game = game;

        save.battleMap = 0;
        save.enemies = new List<string>();
        save.numberOfEnemies = new List<int>();

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
