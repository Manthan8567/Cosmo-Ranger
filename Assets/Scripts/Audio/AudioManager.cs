using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Singleton;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [System.Serializable]
    public struct SoundEffect
    {
        public string soundName;
        public AudioClip audioClip;
    }

    [SerializeField] SoundEffect[] soundEffects;

    private List<AudioSource> audioSources = new List<AudioSource>();


    private void Start()
    {
        // Create an Audio Source in Hierarchy (below the child of this)
        CreateAudioSource();
    }

    public AudioSource CreateAudioSource()
    {      
        GameObject audioSourceGO = new GameObject();

        audioSourceGO.name = "Audio Source";
        audioSourceGO.transform.parent = this.transform;

        AudioSource newAudioSource = audioSourceGO.AddComponent<AudioSource>();

        audioSources.Add(newAudioSource);

        return newAudioSource;
    }

    // Play sound effect by name
    public void PlaySoundEffect(string soundName)
    {
        foreach (SoundEffect soundEffect in soundEffects)
        {
            if (soundEffect.soundName == soundName)
            {
                PlaySoundEffect(soundEffect.audioClip);
            }
        }
    }

    // Play sound effect by audio clip
    public void PlaySoundEffect(AudioClip audioClip)
    {
        // Find an Audio Source which is not playing anything, and then use it to play the Audio Clip
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
                return;
            }
        }

        // If every audio sources are playing audio clips right now, create new one
        AudioSource newAudioSource = CreateAudioSource();
        newAudioSource.clip = audioClip;
        newAudioSource.Play();
    }
}
