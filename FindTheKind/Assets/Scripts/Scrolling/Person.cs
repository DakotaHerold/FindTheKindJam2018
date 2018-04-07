using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //TODO start a conversation
            other.GetComponent<Player>().State = CharacterState.Idle;
        }
    }
}
