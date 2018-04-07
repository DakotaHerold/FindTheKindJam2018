using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
    
    [SerializeField]
    private DialogueData dialogueData;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //TODO start a conversation
            levelManager.PauseLevel();
            levelManager.StartConversation(dialogueData);
        }
    }
}
