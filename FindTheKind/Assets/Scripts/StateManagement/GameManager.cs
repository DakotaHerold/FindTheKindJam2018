using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    enum GAME_STATE
    {
        Title, 
        Running, 
        Talking, 
        GameOver
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {

    }

    public void EndGame()
    {

    }

    public void RestartGame()
    {

    }

    #region Gameplay Functions

    public void TriggerDialogue(DialogueData characterData)
    {

    }

    public void ResumeScrolling()
    {
        
    }
    #endregion
}
