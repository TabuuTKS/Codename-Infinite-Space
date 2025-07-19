using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float RotateSpeed = 0.5f;
    [SerializeField] float DropSpeed = 2f;
    void Update()
    {
        transform.Rotate(RotateSpeed, 0, 0);
        transform.position += new Vector3(0, 0, -DropSpeed) * Time.deltaTime;
    }
}
