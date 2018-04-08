using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    
    private List<GameObject> backgrounds, spriteObjects;
    private List<TileScroll> tiles;
    private GameObject lastBackground;
    [SerializeField]
    private float backgroundWidth;
    private float screenEdge, scrollSpeed;

    // Use this for initialization
    public void Initialize () {
        tiles = new List<TileScroll>(GetComponentsInChildren<TileScroll>());
	}
	
	// Update is called once per frame
	void Update () {
        foreach(TileScroll tile in tiles)
        {
            tile.Move(scrollSpeed * Time.deltaTime);
        }
	}

    public void RemoveSprite(GameObject removed)
    {
        if (spriteObjects.Contains(removed))
        {
            spriteObjects.Remove(removed);

            if (backgrounds.Contains(removed))
            {
                backgrounds.Remove(removed);
            }
        }

        Destroy(removed);
    }
    

    public float ScrollSpeed
    {
        get { return scrollSpeed; }
        set
        {
            scrollSpeed = value;
        }
    }

    public List<TileScroll> Tiles
    {
        get
        {
            return tiles;
        }
    }

    public void setScreenEdge(float edge)
    {
        screenEdge = edge;
    }

    private int addCycle(int max, int value)
    {
        value++;

        if(value > max)
        {
            value = 0;
        }

        return value;
    }
}
