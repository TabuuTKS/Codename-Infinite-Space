using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    //Save System (Basic Implementation Tutorial By Brakcyes)
    //Reimplimented into my game by me
    PlayerData playerData;
    BinaryFormatter formatter = new BinaryFormatter();

    public void Save()
    {
        string path = Application.persistentDataPath + "/SaveGame.infsav";
        GameEvents.HaveCoins += GameEvents.coins;
        if (GameEvents.score > GameEvents.HighScore) { GameEvents.HighScore = GameEvents.score; }
        FileStream SaveStream = new FileStream(path, FileMode.Create);
        playerData = new PlayerData();
        playerData.HighScore = GameEvents.HighScore;
        playerData.HaveCoins = GameEvents.HaveCoins;
        playerData.ShipTypeIndex = GameEvents.ShipTypeIndex;
        playerData.BoughtShips = GameEvents.BoughtShips;
        formatter.Serialize(SaveStream, playerData);
        SaveStream.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/SaveGame.infsav";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            playerData = formatter.Deserialize(stream) as PlayerData;
            GameEvents.HighScore = playerData.HighScore;
            GameEvents.HaveCoins = playerData.HaveCoins;
            GameEvents.ShipTypeIndex = playerData.ShipTypeIndex;
            GameEvents.BoughtShips = playerData.BoughtShips;
            stream.Close();
        }
        else { Debug.Log("No Save File Found"); }
    }

}
