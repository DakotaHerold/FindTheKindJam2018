using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    private Player player;
    [SerializeField]
    private float speedChange, slowestSpeed, screenEdge;
    [SerializeField]
    private Parallax[] parallaxLayers;
    [SerializeField]
    public SoundManager soundManager;
    [SerializeField]
    public HudStates hud;
    public Parallax gameSpace;

    private void Awake()
    {
        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            parallaxLayers[i].setScreenEdge(screenEdge);
        }

        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            if (parallaxLayers[i].gameObject.tag == "GameSpace")
            {
                gameSpace = parallaxLayers[i];
            }
        }
    }

    public void StartLevel()
    {
        parallaxLayers[0].ScrollSpeed = slowestSpeed;
        parallaxLayers[1].ScrollSpeed = speedChange + slowestSpeed; // game space parallax 
        parallaxLayers[2].ScrollSpeed = (2 * speedChange) + slowestSpeed;
        parallaxLayers[3].ScrollSpeed = (2 * speedChange) + slowestSpeed;
    }

    public void ShowHud()
    {
        player.State = CharacterState.Run;
        hud.gameObject.SetActive(true);
        hud.ShowHud();
    }

    public void PauseLevel()
    {
        player.State = CharacterState.Idle;

        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            parallaxLayers[i].ScrollSpeed = 0;
        }
    }

    public bool SpawnNPC(GameObject NPC)
    {
        GameObject tile = gameSpace.mostRecentTile;

        return tile.GetComponent<TileScroll>().SpawnNPC(NPC);
    }

    public void StartConversation(DialogueData data)
    {
        DialogueManager.Instance.StartConversation(data);
    }

    public int TotalCoins
    {
        get
        {
            return GameManager.Instance.NumCoins;
        }
        set
        {
            GameManager.Instance.NumCoins = value;
        }
    }
}
