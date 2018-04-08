using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class TileScroll : MonoBehaviour {

    [SerializeField]
    public Transform npcTransform;
    private GameObject NPC;

    // Update is called once per frame
	public void Move (float speed) {
        transform.Translate(new Vector2(-speed, 0));
    }

    public bool SpawnNPC(GameObject NPC)
    {
        if(npcTransform != null)
        {
            NPC = Instantiate(NPC, npcTransform.position, npcTransform.rotation, this.transform);
            return true;
        }
        return false;
    }

    public void RemoveTile()
    {
        if(NPC != null)
        {
            if (NPC.GetComponent<Person>().TalkedTo)
            {
                GameManager.Instance.TalkedToNPC(NPC);
            }
        }
    }
}
