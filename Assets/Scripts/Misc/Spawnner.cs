using UnityEngine;
public enum SpawnType
{
    LEFT,
    RIGHT,
    UP
}
public class Spawnner : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] BoxCollider collider;

    [Header("Properties")]
    [SerializeField] float DefaultY = 0f;
    [SerializeField] float Length = 2f;
    [SerializeField] SpawnType spawnType = SpawnType.LEFT;

    //private Objects
    GameObject Meteor;

    //private Properties
    Vector3 initPos = Vector3.zero, finalPos = Vector3.zero, midPoint = Vector3.zero, spawnPos;
    float spawnInitPos = 0f;

    /// <summary>
    /// Spawn Positions are Calculated Using Debug Gizmos Visualization.
    /// A Spawn Collider is Used to draw inital, final and mid points and also line of spawn.
    /// Spawn will be on a random position on that line. on the basis of which spawnner is this according to enum
    /// </summary>
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

    //Debug Gizmos
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
