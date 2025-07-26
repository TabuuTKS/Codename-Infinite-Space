using UnityEngine;

public class ParallexBackground : MonoBehaviour
{
    //Basic Parallex Using a Default Repeat Position
    Vector3 StartPosition;
    Vector3 currentPos = Vector3.zero;
    [SerializeField] float ResetPosition;
    [SerializeField] float Speed = 5;
    void Start() { StartPosition = transform.position; }
    void Update()
    {
        currentPos.Set(0, 0, Speed * Time.deltaTime);
        transform.position -= currentPos;
        if (transform.position.z < ResetPosition)
        {
            transform.position = StartPosition;
        }
    }
}
