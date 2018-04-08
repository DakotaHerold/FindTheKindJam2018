using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour {

    public string tagToReturn;
    ObjectPool objectPool;

    private void Start()
    {
        objectPool = GetComponentInParent<ObjectPool>();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == tagToReturn)
        {
            objectPool.ReturnToPool(coll.gameObject);
        }

        if(coll.tag == "temp" && tagToReturn == "Chunk") {
            coll.tag = "Chunk";
            objectPool.ReturnToPool(coll.gameObject);
        }
    }
}