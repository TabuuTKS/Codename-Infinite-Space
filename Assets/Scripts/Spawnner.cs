using UnityEngine;
public enum SpawnType
{
    LEFT,
    RIGHT,
    UP
}
public class Spawnner : MonoBehaviour
{
    GameObject Meteor;
    [SerializeField] BoxCollider collider;
    [SerializeField] float DefaultY = 0f;
    [SerializeField] float Length = 2f;
    [SerializeField] SpawnType spawnType = SpawnType.LEFT;

    Vector3 initPos = Vector3.zero, finalPos = Vector3.zero, midPoint = Vector3.zero, spawnPos;
    float spawnInitPos = 0f;

    public void Spawn()
    {
        Meteor = ObjectPool.Instance.SpawnFromPool("Meteor", spawnPos, Quaternion.identity);
        switch (spawnType)
        {
            case SpawnType.LEFT:
                initPos.Set(collider.center.x, DefaultY, collider.center.y);
                finalPos.Set(collider.center.x, DefaultY, (collider.center.y + Length));
                midPoint.Set(collider.center.x, DefaultY, collider.center.y + (Length / 2));
                spawnInitPos = Random.Range(initPos.z, finalPos.z);

                if (spawnInitPos < midPoint.z)
                {
                    GameEvents.MeteorDir.Set(Random.Range(0.3f, 1), Random.Range(0.3f, 1));
                }
                else
                {
                    GameEvents.MeteorDir.Set(Random.Range(0.3f, 1), Random.Range(-0.3f, -1));
                }

                spawnPos.Set(initPos.x, initPos.y, spawnInitPos);
                Meteor.SetActive(true);
                Meteor.transform.position = spawnPos;
                Meteor.transform.rotation = Quaternion.identity;
                break;
            case SpawnType.RIGHT:

                initPos.Set(collider.center.x, DefaultY, collider.center.y);
                finalPos.Set(collider.center.x, DefaultY, (collider.center.y + Length));
                midPoint.Set(collider.center.x, DefaultY, collider.center.y + (Length / 2));
                spawnInitPos = Random.Range(initPos.z, finalPos.z);

                if (spawnInitPos < midPoint.z)
                {
                    GameEvents.MeteorDir.Set(Random.Range(-0.3f, -1), Random.Range(0.3f, 1));
                }
                else
                {
                    GameEvents.MeteorDir.Set(Random.Range(-0.3f, -1), Random.Range(-0.3f, -1));
                }

                spawnPos.Set(initPos.x, initPos.y, spawnInitPos);
                Meteor.SetActive(true);
                Meteor.transform.position = spawnPos;
                Meteor.transform.rotation = Quaternion.identity;
                break;
            case SpawnType.UP:
                initPos.Set(collider.center.x, DefaultY, collider.center.y);
                finalPos.Set(collider.center.x + Length, DefaultY, collider.center.y);
                midPoint.Set(collider.center.x + (Length / 2), DefaultY, collider.center.y);
                spawnInitPos = Random.Range(initPos.x, finalPos.x);

                if (spawnInitPos < midPoint.x)
                {
                    GameEvents.MeteorDir.Set(Random.Range(0.3f, 1), Random.Range(-0.3f, -1));
                }
                else
                {
                    GameEvents.MeteorDir.Set(Random.Range(-0.3f, -1), Random.Range(-0.3f, -1));
                }

                spawnPos.Set(spawnInitPos, initPos.y, initPos.z);
                Meteor.SetActive(true);
                Meteor.transform.position = spawnPos;
                Meteor.transform.rotation = Quaternion.identity;
                break;
        }
    }
    private void OnDrawGizmos()
    {
        initPos.Set(collider.center.x, DefaultY, collider.center.y);
        finalPos.Set(collider.center.x, DefaultY, (collider.center.y + Length));
        if (spawnType == SpawnType.UP)
        {
            initPos.Set(collider.center.x, DefaultY, collider.center.y);
            finalPos.Set(collider.center.x + Length, DefaultY, collider.center.y);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(initPos, 0.1f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(finalPos, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(initPos, finalPos);
    }
}
