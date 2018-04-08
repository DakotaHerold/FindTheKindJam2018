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

    [SerializeField] private bool randomOffset;

    [SerializeField] private float scrollSpeed;

    private List<GameObject> pool;
    [SerializeField]
    public List<PooledObject> pooledPrefabs;

    // Use this for initialization
    void Start()
    {
        CreatePool(pooledPrefabs);
        
        //Create initial roads
        if (!randomOffset && pool.Count > 0)
        {
            for (int i = 1; i <= 2; i++)
            {
                GameObject toRemove = pool[Random.Range(0, pool.Count)];

                toRemove.transform.position = spawnLocation.position + new Vector3(-11.5f * i, 0, 0);
                toRemove.gameObject.tag = "temp";
                toRemove.SetActive(true);

                pool.Remove(toRemove);
            }
        }

        GetObject();
    }

    public void CreatePool(List<PooledObject> pooled)
    {
        pool = new List<GameObject>();
        foreach(PooledObject obj in pooled)
        {
            for (int i = 0; i < obj.count; ++i)
            {
                GameObject gameObj = Instantiate(obj.chunk, this.gameObject.transform) as GameObject;
                gameObj.GetComponent<TileScroll>().ScrollSpeed = scrollSpeed;
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

            toRemove.transform.position = spawnLocation.position + (randomOffset ? new Vector3(Random.Range(-1.0f, 1.0f), 0, 0) : Vector3.zero);

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
