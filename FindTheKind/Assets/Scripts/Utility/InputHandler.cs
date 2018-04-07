using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired; 

public class InputHandler : Singleton<InputHandler> {

    private Vector2 moveAxes; 
    public Vector2 MoveAxes { get { return moveAxes; } }

    private bool interact; 
    public bool Interact { get { return interact; } }

    // Rewired variables
    private int playerId = 0;
    private Rewired.Player player;

    // Use this for initialization
    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
    }
    // Update is called once per frame
    void Update () {
        // Get button presses from controller

        moveAxes = new Vector2(player.GetAxis("MoveHorizontal"), player.GetAxis("MoveVertical"));
        interact = player.GetButtonDown("Interact");
        
    }
}

