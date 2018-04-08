using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public const float LEVEL_TIME = 6;//120; 

    enum GAME_STATE
    {
        Title, 
        Running, 
        Talking, 
        GameOver
    }

    private GAME_STATE gameState;
    private DialogueManager dialogueManager;
    private LevelManager levelManager; 

    private int numCoins = 0; 
    public int NumCoins { get { return numCoins; } set { numCoins = value; } }

    private float timer; 
    public float Timer { get { return timer; } }

    public string TimerString { get { return GetTimerString(timer); } }

    private int level = 20; 
    public int Level { get { return level; } }

	// Use this for initialization
	void Awake () {
        gameState = GAME_STATE.Running; // Title
        timer = LEVEL_TIME; 
        dialogueManager = FindObjectOfType<DialogueManager>();
        levelManager = FindObjectOfType<LevelManager>(); 
	}

    // Update is called once per frame
    void Update()
    {
        if (gameState == GAME_STATE.Running)
        {
            if(timer > 0)
                timer -= Time.deltaTime;
            else 
            {
                GoToNextLevel(); 
            }
        }
            
    }

    public void StartGame()
    {
        levelManager.StartLevel(); 
    }

    public void EndGame()
    {
        gameState = GAME_STATE.GameOver; 
    }

    public void RestartGame()
    {

    }

    public void GoToNextLevel()
    {
        timer = LEVEL_TIME; 
        level += 10; 
        if(level > 70)
        {
            // TODO Game Over
            EndGame(); 
        }
    }

    #region Gameplay Functions

    private string GetTimerString(float timer)
    {
        string timerStr = "";
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.RoundToInt(timer % 60);

        if (minutes < 10)
        {
            timerStr = "0" + minutes.ToString();
        }
        else
        {
            timerStr = minutes.ToString(); 
        }
        timerStr += ":";
        if (seconds < 10)
        {
            timerStr += "0" + Mathf.RoundToInt(seconds).ToString();
        }
        else
        {
            timerStr += Mathf.RoundToInt(seconds).ToString();
        }

        string result = "Time: " + timerStr; 
        return result;
    }

    public void TriggerDialogue(DialogueData characterData)
    {
        gameState = GAME_STATE.Talking; 
        dialogueManager.StartConversation(characterData); 
    }

    public void TriggerDialogueEnd()
    {
        gameState = GAME_STATE.Running; 
    }
    #endregion
}
