using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship")]
public class ShipObject : ScriptableObject
{
    public new string name;
    public string description;
    public GameObject SpaceShipMesh;
    public float MoveSpeed;
    public float BullitWaitTime;
    public int Price;
    [HideInInspector] public bool BoughtTheShip;
}
