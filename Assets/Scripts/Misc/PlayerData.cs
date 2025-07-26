using UnityEngine;

//This Class Collects Player Data For Save
[System.Serializable]
public class PlayerData
{
    public int HaveCoins;
    public int HighScore;
    public int ShipTypeIndex;
    public bool[] BoughtShips;
}
