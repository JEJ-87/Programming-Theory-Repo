using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] TextMeshProUGUI hp_TXT;
    [SerializeField] TextMeshProUGUI highscore_TXT;
    [SerializeField] TextMeshProUGUI score_TXT;
    [SerializeField] TextMeshProUGUI challenge_TXT;

    // Update is called once per frame at end of OoO
    void LateUpdate()
    {
        if (GameManager.instance.score <= DataManager.instance.highscore)
        {
            highscore_TXT.text = "Highscore : " + DataManager.instance.bestPlayer + " ~ " + DataManager.instance.highscore;
        }
        else
        {
            highscore_TXT.text = "Highscore : " + DataManager.instance.playerName + " ~ " + GameManager.instance.score;
        }
        score_TXT.text = "Score : " + DataManager.instance.playerName + " ~ " + GameManager.instance.score;
        hp_TXT.text = "Life ~ " + player.playerHealth;
        challenge_TXT.text = "Challenge ~ " + GameManager.instance.challenge;
    }

    //ABSTRACTION
    public void ReturnToMainMenu()
    {
        if (GameManager.instance.score > DataManager.instance.highscore)
        {
            DataManager.instance.bestPlayer = DataManager.instance.playerName;
            DataManager.instance.highscore = GameManager.instance.score;
            DataManager.instance.SaveData();
        }
        MusicManager.instance.SwitchMusic(0);
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        if (GameManager.instance.score > DataManager.instance.highscore)
        {
            DataManager.instance.bestPlayer = DataManager.instance.playerName;
            DataManager.instance.highscore = GameManager.instance.score;
            DataManager.instance.SaveData();
        }
        SceneManager.LoadScene(1);
    }
}
