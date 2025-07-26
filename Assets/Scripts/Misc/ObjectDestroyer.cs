using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    //Destroy Objects on the Basis of Tags
    [SerializeField] string Tag = string.Empty;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag))
        {
            other.gameObject.SetActive(false);
        }
    }
}
