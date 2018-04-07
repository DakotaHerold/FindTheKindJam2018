using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType { Empty, Person, Coin } 

public class GenerateLanes : MonoBehaviour
{ 
    private TileType[,] chunk;

    private Vector2 spawnLocation = new Vector2(0,0); //where the TOP tile spawns from
    private float offset = 1.0f;

    private int chunkWidth = 8;

    [SerializeField] private GameObject EmptyTile;
    GameObject lastTile;
   
    // Use this for initialization
    void Start () {
        chunk = FillEmpty();
        //spawn in new Row
        Spawn();
    }

	private void Spawn () {
        //spawn in new Row
        for(int r = 0; r < 3; r++)
        {
            for(int c = 0; c < chunkWidth; c++)
            {
                EmptyTile.transform.position = spawnLocation + new Vector2(c * offset, r * -offset);
                Instantiate(EmptyTile);
                if(r == 2 && c == chunkWidth - 1)
                {
                    lastTile = EmptyTile;
                }
                switch (chunk[r,c])
                { 
                    case TileType.Person:
                    case TileType.Coin:
                    default:
                        break;
                }
            }
        }
	}

    private TileType[,] FillEmpty() {

        TileType[,] Filled = new TileType[3,chunkWidth];

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < chunkWidth; c++)
            {
                Filled[r, c] = TileType.Empty;
            }
        }
        return Filled;
    }

}
