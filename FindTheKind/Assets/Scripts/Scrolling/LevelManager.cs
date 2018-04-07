using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private int totalCoins;
    [SerializeField]
    private Player player;
	
    public void StartLevel()
    {
        player.State = CharacterState.NormalRun;
    }

    public void EndLevel()
    {
        player.State = CharacterState.Idle;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartLevel();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndLevel();
        }

        Debug.Log("Player State: " + player.State + " Total Coins: " + totalCoins);
    }

    public int TotalCoins
    {
        get
        {
            return totalCoins;
        }
        set
        {
            totalCoins = value;
        }
    }
}
