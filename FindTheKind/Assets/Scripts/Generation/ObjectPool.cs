﻿using System.Collections;
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

    private Parallax parallax;

    [SerializeField] private bool randomOffset, generateChunk;

    [SerializeField] private float scrollSpeed;

    private List<GameObject> pool;
    [SerializeField]
    public List<PooledObject> pooledPrefabs;


    // Use this for initialization
    void Awake()
    {
        parallax = GetComponentInParent<Parallax>();
        parallax.Initialize();
        CreatePool(pooledPrefabs);
       
        //Create initial roads
        if (pool.Count > 0)
        {
            for (int i = 1; i <= 2; i++)
            {
                GameObject toRemove;
                if (!generateChunk)
                {
                    toRemove = pool[Random.Range(0, pool.Count)];
                }
                else
                {
                    toRemove = pool[0];
                    toRemove.tag = "temp";
                }
                
                toRemove.transform.position = spawnLocation.position + new Vector3(-11.5f * i, 0, 0);
                toRemove.SetActive(true);

                parallax.Tiles.Add(toRemove.GetComponent<TileScroll>());

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
            
            //set all children to acive -------------------------------------- << change tag of charachters interacted with and don't set them to active?
            foreach (Transform child in toRemove.transform)
            {
                child.gameObject.SetActive(true);
            }
            toRemove.SetActive(true);

            pool.Remove(toRemove);

            parallax.SetMostRecentTile(toRemove); 
            parallax.Tiles.Add(toRemove.GetComponent<TileScroll>());

            return toRemove;
        }
        return null;
    }

    public void ReturnToPool(GameObject gameObj)
    {
        gameObj.SetActive(false);
        gameObj.GetComponent<TileScroll>().RemoveTile();
        pool.Add(gameObj);
        parallax.Tiles.Remove(gameObj.GetComponent<TileScroll>());
    }
}
