using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]
    private int coinValue;
    private LevelManager manager;

    private void Start()
    {
        manager = GetComponentInParent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            manager.TotalCoins += coinValue;
            Destroy(this.gameObject);
        }
    }
}
