using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace GamingTools
{
    public static class ResourseLoader
    {
        public static GameObject GetGameObject(string loadMe)
        {
            GameObject theObject = Resources.Load<GameObject>(loadMe);
            return theObject;
        }
        public static GameObject[] GetMultipleGameObjects(string loadMe)
        {
            GameObject[] theObject = Resources.LoadAll<GameObject>(loadMe);
            return theObject;
        }
        public static Material GetMaterial(string loadMe)
        {
            Material theObject = Resources.Load<Material>(loadMe);
            return theObject;
        }
        public static Sprite GetSprite(string loadMe)
        {
            Sprite theObject = Resources.Load<Sprite>(loadMe);
            return theObject;
        }
    }
    public static class SaveLoad
    {
        public static void save(SaveFile ship, string saveName)
        {
            SaveFile theShip = ship;
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + saveName + ".save");
            bf.Serialize(file, theShip);
            file.Close();
        }

        public static SaveFile load(string saveName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saveName + ".save", FileMode.Open);
            SaveFile save = (SaveFile)bf.Deserialize(file);
            file.Close();
            return save;
        }
    }
}
