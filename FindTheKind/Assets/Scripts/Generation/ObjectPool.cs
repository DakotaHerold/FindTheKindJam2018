using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TileType { Empty, Person, Coin } 

[System.Serializable]
public struct PooledObject
{
    public GameObject chunk;
    public int count; 
}

public class ObjectPool : MonoBehaviour
{
    public Transform spawnLocation;

    [SerializeField] private int chunkWidth;

    [SerializeField] private float scrollSpeed;

    private List<GameObject> pool;
    [SerializeField]
    public List<PooledObject> pooledPrefabs;

    static ObjectPool _generator;

    public static ObjectPool generator {
        get
        {
            if (_generator != null)
            {
                _generator = FindObjectOfType<ObjectPool>();
            }
            return _generator;
        }
    }

    // Use this for initialization
    void Start()
    {
        CreatePool(pooledPrefabs);

        GetObject();
    }

    public void CreatePool(List<PooledObject> pooled)
    {
        pool = new List<GameObject>();
        foreach(PooledObject obj in pooled)
        {
            obj.chunk.GetComponent<TileScroll>().ScrollSpeed = scrollSpeed;
            for (int i = 0; i < obj.count; ++i)
            {
                GameObject gameObj = Instantiate(obj.chunk) as GameObject;
                gameObj.SetActive(false);
                pool.Add(gameObj);
            }
        }
    }

    public GameObject GetObject()
    {
        if(pool.Count > 0)
        {
            GameObject toRemove = pool[Random.Range(0, pool.Count)];
            toRemove.transform.position = spawnLocation.position;
            toRemove.SetActive(true);
            
            pool.Remove(toRemove);

            return toRemove;
        }
        return null;
    }

    public void ReturnToPool(GameObject gameObj)
    {
        gameObj.SetActive(false);
        pool.Add(gameObj); 
    }
}
