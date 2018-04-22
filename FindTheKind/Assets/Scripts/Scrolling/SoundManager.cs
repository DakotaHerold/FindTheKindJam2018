using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundClip
{
    CollectCoin,
    Step1,
    Step2,
    Step3,
    Hit1,
    Hit2
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    [SerializeField]
    private AudioClip[] clips;
    private AudioSource currentAudioSource;
    [SerializeField]
    private AudioClip typingSound;

    private List<AudioSource> audioSources; 

	// Use this for initialization
	void Start () {
        currentAudioSource = GetComponent<AudioSource>();
        audioSources = new List<AudioSource>(); 
        for(int i = 0; i < 10; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioSources.Add(source);
        }
	}

    private void UpdateCurrentAudioSource()
    {
        AudioSource oldSource = currentAudioSource; 
        while(currentAudioSource == oldSource)
        {
            foreach(AudioSource source in audioSources)
            {
                if(source != oldSource)
                {
                    currentAudioSource = source;
                    break; 
                }
            }
        }
    }

    public void Play(SoundClip clip)
    {
        UpdateCurrentAudioSource(); 
        currentAudioSource.Stop(); 
        currentAudioSource.clip = clips[(int)clip];
        currentAudioSource.PlayOneShot(currentAudioSource.clip);
        //audioSource.Play();
    }

    public void PlayTypeSound()
    {
        float vol = Random.Range(0.2f, 1f);
        currentAudioSource.PlayOneShot(typingSound, vol); 
    }
}
