using UnityEngine;

//Mobile UI Implementation Using New Input System
public class MobileUI : MonoBehaviour
{
    [SerializeField] SpaceShip spaceShip;
    public void MoveLeftButtonDown() => spaceShip.setXdir(-1);
    public void MoveRightButtonDown() => spaceShip.setXdir(1);
    public void MoveButtonUp() => spaceShip.setXdir(0);

    public void AttackButtonDown() => spaceShip.setAttack(true);
    public void AttackButtonUp() => spaceShip.setAttack(false);
}
