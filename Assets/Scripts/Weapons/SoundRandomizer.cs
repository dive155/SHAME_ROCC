using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRandomizer : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> sounds;

    [SerializeField]
    private bool playOnAwake = false;

    void Awake()
    {
        if (playOnAwake)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        int num = Random.Range(0, sounds.Count);
        audioSource.clip = sounds[num];
        audioSource.Play();
    }
}
