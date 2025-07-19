using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPool Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    Dictionary<string, Queue<GameObject>> poolDictonary;
    void Start()
    {
        poolDictonary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject ObjectToAdd = Instantiate(pool.prefab);
                ObjectToAdd.SetActive(false);
                objectPool.Enqueue(ObjectToAdd);
            }

            poolDictonary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 SpawnPosition, Quaternion SpawnRotation)
    {

        GameObject ObjectToSpawn = poolDictonary[tag].Dequeue();
        ObjectToSpawn.transform.position = SpawnPosition;
        ObjectToSpawn.transform.rotation = SpawnRotation;

        poolDictonary[tag].Enqueue(ObjectToSpawn);

        return ObjectToSpawn;
    }
}
