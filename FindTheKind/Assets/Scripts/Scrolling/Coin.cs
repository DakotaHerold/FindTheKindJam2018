﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour {

    [SerializeField]
    private int coinValue;
    private AudioSource source;
    private LevelManager manager;
    private Parallax parallax;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        manager = GetComponentInParent<LevelManager>();
        parallax = GetComponentInParent<Parallax>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            manager.TotalCoins += coinValue;
            source.Play();
            parallax.RemoveSprite(this.gameObject);
        }
    }
}
