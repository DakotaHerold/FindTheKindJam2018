using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundClip
{
    CollectCoin,
    Step1,
    Step2,
    Step3
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    [SerializeField]
    private AudioClip[] clips;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    public void Play(SoundClip clip)
    {
        audioSource.clip = clips[(int)clip];
        audioSource.Play();
    }
}
