using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    
    private List<GameObject> backgrounds, spriteObjects;
    private GameObject lastBackground;
    private float screenEdge, scrollSpeed;

    // Use this for initialization
    void Start () {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        backgrounds = new List<GameObject> ();
        spriteObjects = new List<GameObject>();

        for(int i = 0; i < renderers.Length; i++)
        {
            if(renderers[i].tag == "Background")
            {
                backgrounds.Add(renderers[i].gameObject);
            }

            spriteObjects.Add(renderers[i].gameObject);
        }

        if(backgrounds.Count > 0)
        {
            lastBackground = backgrounds[backgrounds.Count - 1];
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < spriteObjects.Count; i++)
        {
            if (backgrounds.Contains(spriteObjects[i]))
            {
                if (spriteObjects[i].transform.position.x < screenEdge)
                {
                    float spawnPosition = lastBackground.transform.localPosition.x + lastBackground.transform.localScale.x;
                    spriteObjects[i].transform.localPosition = new Vector3(spawnPosition, 0, 0);
                    lastBackground = spriteObjects[i];
                }
            }
            else if(spriteObjects[i].transform.position.x < screenEdge)
            {
                GameObject removed = spriteObjects[i];
                RemoveSprite(removed);
                Destroy(removed);
            }

            if(i < spriteObjects.Count)
            {
                spriteObjects[i].transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
            }
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

    public void setScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
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
