using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
    
    [SerializeField]
    private DialogueData dialogueData;
    private LevelManager levelManager;
    private SoundManager soundManager;
    private SoundClip[] clips;

    private void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();
        soundManager = levelManager.soundManager;
        clips = new SoundClip[]
        {
            SoundClip.Hit1,
            SoundClip.Hit2
        };
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //TODO start a conversation
            soundManager.Play(clips[Random.Range(0, 2)]);
            levelManager.PauseLevel();
            levelManager.StartConversation(dialogueData);
        }
    }
}
