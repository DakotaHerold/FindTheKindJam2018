using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]
    private int coinValue;
    private SoundClip clip;
    private SoundManager soundManager;
    private LevelManager levelManager;
    private Parallax parallax;

    private void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();
        soundManager = levelManager.soundManager;
        parallax = GetComponentInParent<Parallax>();
        clip = SoundClip.CollectCoin;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            levelManager.TotalCoins += coinValue;
            soundManager.Play(clip);
            parallax.RemoveSprite(this.gameObject);
        }
    }
}
