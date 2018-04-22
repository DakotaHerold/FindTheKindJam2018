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
            NPC = Instantiate(NPC, npcTransform.position, npcTransform.rotation);
            NPC.transform.parent = npcTransform;
            return true;
        }

        Debug.Log("No spawnpoint");
        return false;
    }

    public void clearNPCs()
    {
        if (npcTransform != null)
        {
            for (int i = 0; i < npcTransform.childCount; ++i)
            {
                GameObject childGO = npcTransform.transform.GetChild(i).gameObject;
                if (childGO.GetComponent<Person>() != null)
                    Destroy(childGO);
            }
        }
    }
}
