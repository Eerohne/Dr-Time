using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
 
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.DrTime";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavingPlayerData data = new SavingPlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SavingPlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.DrTime";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavingPlayerData data = formatter.Deserialize(stream) as SavingPlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found");
            return null;
        }
    }

}
