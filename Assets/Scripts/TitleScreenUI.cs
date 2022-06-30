using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreenUI : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToHide;
    [SerializeField] TMP_InputField nameEntryText;
    [SerializeField] TextMeshProUGUI highscore;
    [SerializeField] Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.instance.hasSaved)
        {
            nameEntryText.text = DataManager.instance.playerName;
            highscore.text = DataManager.instance.bestPlayer + " ~ " + DataManager.instance.highscore.ToString();
        }
        else
        {
            foreach (GameObject obj in objectsToHide)
            {
                obj.SetActive(false);
            }
        }
    }

    private void LateUpdate()
    {
        if (nameEntryText.text == "")
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

    //ABSTRACTION
    //Start the game
    public void StartGame()
    {
        DataManager.instance.playerName = nameEntryText.text;
        MusicManager.instance.SwitchMusic(1);
        SceneManager.LoadScene(1);
    }

    //reset the game
    public void resetButton()
    {
        DataManager.instance.DeleteSaveData();
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
