using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayerSystem(int health, int maxHealth, bool sword, bool RubberRing, bool Lamp, int coins, int swordAttack) {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerdata.pro";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(health, maxHealth, sword, RubberRing, Lamp, coins, swordAttack);

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
}
