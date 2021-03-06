using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject objectToPool;
    [SerializeField][Min(0)] int spawnCount;
    public int countSpawned
    {
        get
        {
            return spawnCount;
        }
        private set
        {

        }
    }
    public List<GameObject> pooledObjects { get; private set; }


    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform);
        }
    }

    //Get the object to activate
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
