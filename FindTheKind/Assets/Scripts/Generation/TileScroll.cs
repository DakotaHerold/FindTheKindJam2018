using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class TileScroll : MonoBehaviour {

    private float scrollSpeed;
    private float maxScrollSpeed = 0.3f;

    public float ScrollSpeed
    {
        get { return scrollSpeed; }
        set
        {
            if (value <= 0)
            {
                scrollSpeed = 0;
            }
            else if (value >= maxScrollSpeed)
            {
                scrollSpeed = maxScrollSpeed;
            }
            else
            {
                scrollSpeed = value;
            }
        }
    }
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(-scrollSpeed, 0));
    }
}
