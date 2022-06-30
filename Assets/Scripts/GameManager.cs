using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ENCAPSULATION
    public static GameManager instance { get; private set; }

    [HideInInspector] public bool isGameOver = false;
    [HideInInspector] public int score;
    [HideInInspector] public int challenge;

    [Header("Settings")]
    [Min(0)] public int challengeThreshold;
    [SerializeField] GameObject gameOverGroup;
    [SerializeField] AudioClip[] audioClips;

    AudioSource m_Audio;

    int m_threshold;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        m_Audio = GetComponent<AudioSource>();

        m_threshold = challengeThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= m_threshold)
        {
            ChallengeUp();
        }
    }

    //ABSTRACTION
    //Increase the challenge level
    void ChallengeUp()
    {
        m_threshold += challengeThreshold;
        challenge++;
        m_Audio.PlayOneShot(audioClips[0]);
        SpawnManager.instance.SpawnEnemy();
    }

    //Enter GameOver state
    public void GameOver()
    {
        gameOverGroup.SetActive(true);
        m_Audio.PlayOneShot(audioClips[1]);
        isGameOver = true;
    }
}
