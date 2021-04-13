using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    public static void SavePlayerSystem(int health, int maxHealth, bool sword, bool RubberRing, bool Lamp, int coins, int swordAttack, bool pirateHouseKey, string scene) {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerdata.pro";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(health, maxHealth, sword, RubberRing, Lamp, coins, swordAttack, pirateHouseKey, scene);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerSystem() 
    {
        string path = Application.persistentDataPath + "/playerdata.pro";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else {
            Debug.Log("Save File not found");
            return null;
        }
    }

    public static void ResetPlayerSystem() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerdata.pro";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(4, 4, false, false, false, 0, 1, false, "BeachBeginning");

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveChestsInScene(int scene, List<bool> chestStatusInScenes)
    {
        MinichestStatus AllChests = LoadChestsInScene();
        bool saved = false;
        if (scene > AllChests.ChestStatusInGame.Count) {
            Debug.Log("No hay datos para cargar");
            return;    
        }
        for (int i = 0; i < AllChests.ChestStatusInGame.Count; i++) {
            if (AllChests.ChestStatusInGame[i].id == scene) {

                AllChests.ChestStatusInGame[i].OpenedStatus = chestStatusInScenes;
                saved = true;
            }
        }

        if(!saved)AllChests.ChestStatusInGame.Add(new ListMinichest(scene, chestStatusInScenes));

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/chestdata.pro";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, AllChests);
        stream.Close();
    }

    public static MinichestStatus LoadChestsInScene()
    {
        string path = Application.persistentDataPath + "/chestdata.pro";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MinichestStatus AllChests = formatter.Deserialize(stream) as MinichestStatus;
            stream.Close();

            return AllChests;
        }
        else
        {
            Debug.Log("Save File not found");
            return null;
        }
    }

    public static void ResetChestsInGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/chestdata.pro";
        FileStream stream = new FileStream(path, FileMode.Create);

        MinichestStatus AllChests = new MinichestStatus();

        formatter.Serialize(stream, AllChests);
        stream.Close();
    }

}
