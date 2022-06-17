using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreenUI : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToHide;
    [SerializeField] TMP_InputField nameEntryText;
    [SerializeField] TMP_Text highscore;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.hasSaved)
        {
            highscore.text = GameManager.instance.bestPlayer + " ~ " + GameManager.instance.highscore.ToString();
        }
        else
        {
            foreach (GameObject obj in objectsToHide)
            {
                obj.SetActive(false);
            }
        }
    }

    //Start the game
    public void StartGame()
    {
        GameManager.instance.playerName = nameEntryText.text;
        SceneManager.LoadScene(1);
    }

    //Quit application
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
