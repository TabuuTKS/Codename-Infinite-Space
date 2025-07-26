using UnityEngine;

public class BullitMove : MonoBehaviour
{
    [SerializeField] float Speed = 5;
    GameObject Coin, DestroyParticle;
    Vector3 pos = Vector3.zero;

    void Update()
    {
        pos.Set(0, 0, Speed * Time.deltaTime);
        transform.position += pos;
    }

    //Collision with Bullit
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteor")
        {
            GameUI.Instance.MeteorSound.Play();
            if (ShouldCoinDrop())
            {
                Coin = ObjectPool.Instance.SpawnFromPool("Coin", transform.position, Quaternion.Euler(90, 90, 0));
                Coin.SetActive(true);
            }
            GameEvents.addScore();
            DestroyParticle = ObjectPool.Instance.SpawnFromPool("DestroyParticles", other.transform.position, Quaternion.Euler(-90,0,0));
            DestroyParticle.GetComponent<ParticleSystem>().Play();
            DestroyParticle.SetActive(true);
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    //Helper Method
    bool ShouldCoinDrop() { return (Random.Range(0f, 1f) < 0.2); }
}
