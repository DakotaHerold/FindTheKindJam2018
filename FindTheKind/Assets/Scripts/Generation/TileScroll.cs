using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TileScroll : MonoBehaviour {

    [SerializeField]
    public Transform npcTransform; 

    // Update is called once per frame
	public void Move (float speed) {
        transform.Translate(new Vector2(-speed, 0));
    }
}
