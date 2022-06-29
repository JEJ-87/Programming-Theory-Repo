using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [HideInInspector] public bool isGameOver = false;
    [HideInInspector] public int score;
    [HideInInspector] public int challenge;

    [Header("Settings")]
    [Min(0)] public int challengeThreshold;
    [SerializeField] GameObject gameOverGroup;

    int m_threshold;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

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

    //Increase the challenge level
    void ChallengeUp()
    {
        m_threshold += challengeThreshold;
        challenge++;
        SpawnManager.instance.SpawnEnemy();
    }

    public void GameOver()
    {
        gameOverGroup.SetActive(true);
        isGameOver = true;
    }
}
