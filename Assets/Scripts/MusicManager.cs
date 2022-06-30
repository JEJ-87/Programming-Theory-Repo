using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    //ENCAPSULATION
    public static MusicManager instance { get; private set; }

    public AudioClip[] music;

    AudioSource m_audio;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        m_audio = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    //ABSTRACTION
    //Switch music
    public void SwitchMusic(int index)
    {
        m_audio.Stop();
        m_audio.PlayOneShot(music[index]);
    }
}
