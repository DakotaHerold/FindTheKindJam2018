using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    
    public const float LEVEL_TIME = 10;//120; 
    public const int SPAWN_INTERVAL = 5;

    bool isAllowedToSpawn = true; 

    enum GAME_STATE
    {
        Title, 
        Running, 
        Talking, 
        GameOver
    }

    private GAME_STATE gameState;
    private float spawnTimer = 0f;
    [SerializeField]
    public DialogueManager dialogueManager;
    private LevelManager levelManager;
    [SerializeField]
    private GameObject[] NPCs;
    private List<GameObject> talkedTo, notTalkedTo;

    [SerializeField]
    public GameObject funeralScreen;
    [SerializeField]
    public GameObject gameUI;
    [SerializeField]
    public GameObject menuUI;

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
            timer -= Time.deltaTime;
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= SPAWN_INTERVAL && isAllowedToSpawn)
            {
                isAllowedToSpawn = false;
                spawnTimer = 0f;
                SpawnNpc();
            }

            if (timer <= 0) 
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
        levelManager.PauseLevel();
        dialogueManager.DisableStartGameHud();
        funeralScreen.SetActive(true);
        dialogueManager.FinalDialogue(); 


        // Have dialogue manager disable top hud
        // TODO generate conversation based on who talked and what they'd say
        gameState = GAME_STATE.GameOver; 
    }

    public void RestartGame()
    {
        talkedTo = new List<GameObject>();
        notTalkedTo = new List<GameObject>(NPCs);
    }

    public void SpawnNpc()
    {
        GameObject npcToSpawn = notTalkedTo[Random.Range(0, notTalkedTo.Count)];

        bool spawned = levelManager.SpawnNPC(npcToSpawn);
        if (spawned)
        {
            Debug.Log("Spawned NPC: " + npcToSpawn.name);
        }
    }

    public void TalkedToNPC(GameObject NPC)
    {
        for(int i = 0; i < notTalkedTo.Count; i++)
        {
            if (notTalkedTo[i].GetComponent<Person>().Data == NPC.GetComponent<Person>().Data)
            {
                talkedTo.Add(notTalkedTo[i]);
                notTalkedTo.Remove(notTalkedTo[i]);
            }
        }

        string output = "Spoken to NPCs: \n";

        foreach (GameObject npc in talkedTo)
        {
            output += npc.name + "\n";
        }
        output += "\nUnspoken to NPCs: \n";
        foreach(GameObject npc in notTalkedTo)
        {
            output += npc.name + "\n";
        }

        Debug.Log(output);
    }

    public void GoToNextLevel()
    {
        timer = LEVEL_TIME;
        spawnTimer = 0f;
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
        levelManager.StartLevel();
    }
    #endregion
}
