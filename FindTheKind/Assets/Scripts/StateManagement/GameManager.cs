using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public const float LEVEL_TIME = 60;//120; 
    public const int SPAWN_INTERVAL = 5;

    bool isAllowedToSpawn = false; 

    enum GAME_STATE
    {
        Title, 
        Running, 
        Talking, 
        GameOver
    }

    private GAME_STATE gameState;
    [SerializeField]
    public DialogueManager dialogueManager;
    private LevelManager levelManager;
    [SerializeField]
    private GameObject[] NPCs;
    private List<GameObject> talkedTo, notTalkedTo;

    private int numCoins = 0; 
    public int NumCoins { get { return numCoins; } set { numCoins = value; } }

    private float timer; 
    public float Timer { get { return timer; } }

    public string TimerString { get { return GetTimerString(timer); } }

    private int level = 20; 
    public int Level { get { return level; } }

	// Use this for initialization
	void Start () {
        gameState = GAME_STATE.Title; // Title
        timer = LEVEL_TIME; 
        levelManager = FindObjectOfType<LevelManager>();
        notTalkedTo = new List<GameObject>(NPCs);
        talkedTo = new List<GameObject>();
	}

    // Update is called once per frame
    void Update()
    {
        if (gameState == GAME_STATE.Running)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                if(Mathf.RoundToInt(timer) % SPAWN_INTERVAL == 0 && !isAllowedToSpawn)
                {
                    isAllowedToSpawn = true; 
                    SpawnNpc();
                }
            }
                
            else 
            {
                GoToNextLevel(); 
            }
        }
    }

    public void StartGame()
    {
        gameState = GAME_STATE.Running;
        levelManager.ShowHud();
        levelManager.StartLevel();
    }

    public void EndGame()
    {
        gameState = GAME_STATE.GameOver; 
    }

    public void RestartGame()
    {
        talkedTo = new List<GameObject>();
        notTalkedTo = new List<GameObject>(NPCs);
    }

    public void SpawnNpc()
    {
        bool spawned = levelManager.SpawnNPC(notTalkedTo[Random.Range(0, notTalkedTo.Count)]);
        Debug.Log("Spawned? " + spawned);
    }

    public void TalkedToNPC(GameObject NPC)
    {
        notTalkedTo.Remove(NPC);
        talkedTo.Add(NPC);
    }

    public void GoToNextLevel()
    {
        timer = LEVEL_TIME;
        level += 10;
        if (level > 70)
        {
            // TODO Game Over
            EndGame();
        }
        else
        {
            isAllowedToSpawn = true; 
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
