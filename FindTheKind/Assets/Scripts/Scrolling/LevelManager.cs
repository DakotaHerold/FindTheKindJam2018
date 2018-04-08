using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private int totalCoins;
    [SerializeField]
    private Player player;
    [SerializeField]
    private float speedChange, screenEdge;
    [SerializeField]
    private Parallax[] parallaxLayers;

    private void Start()
    {
        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            parallaxLayers[i].setScreenEdge(screenEdge);
        }
    }

    public void StartLevel()
    {
        player.State = CharacterState.Run;

        for(int i = 0; i < parallaxLayers.Length; i++)
        {
            parallaxLayers[i].setScrollSpeed((i * speedChange) + 5);
        }
    }

    public void PauseLevel()
    {
        player.State = CharacterState.Idle;

        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            parallaxLayers[i].setScrollSpeed(0);
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartLevel();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PauseLevel();
        }
    }

    public void StartConversation(DialogueData data)
    {
        DialogueManager.Instance.StartConversation(data);
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
