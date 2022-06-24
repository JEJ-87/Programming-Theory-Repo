using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance { get; private set; }

    [HideInInspector] public string playerName;
    public int score;

    [Header("Data To Save")]
    public string bestPlayer;
    public int highscore;
    public bool hasSaved;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    //Persistent Data To Save
    [System.Serializable]
    class DataToSave
    {
        public string bestPlayer;
        public int highscore;
        public bool hasSaved;
    }

    public void SaveData()
    {
        hasSaved = true;

        DataToSave data = new DataToSave();
        data.bestPlayer = bestPlayer;
        data.highscore = highscore;
        data.hasSaved = hasSaved;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DataToSave data = JsonUtility.FromJson<DataToSave>(json);
            bestPlayer = data.bestPlayer;
            highscore = data.highscore;
            hasSaved = data.hasSaved;
        }
    }

    public void DeleteSaveData()
    {
        bestPlayer = null;
        highscore = 0;
        hasSaved = false;

        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            SceneManager.LoadScene(0);
        }
    }
}