using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance {  get; private set; }

    [SerializeField] private TMP_InputField bestScoreInputField;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private string bestScoreInputName;
    private string bestScoreInputName2;

    private int bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadScoreAndName();
        InputSelect();
    }

    public void InputSelect()
    {
        bestScoreText.text = "Best Score: " + bestScoreInputName + ": " + bestScore;
    }
    public void ReadInputField()
    {
        bestScoreInputName = bestScoreInputField.text;
        
        Debug.Log(bestScoreInputName);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public string GetBestScoreName()
    {
        return bestScoreInputName;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void SetBestScore(int score)
    {
        bestScore = score;
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScoreAll;
        public string bestScoreNameAll;
    }

    SaveData data;
    string json;

    public void SaveScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        
        if (!File.Exists(path))
        {
            data = new SaveData();
            data.bestScoreAll = bestScore;
            data.bestScoreNameAll = bestScoreInputName;

            json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        else if(data.bestScoreAll <= bestScore)
        {
            data = new SaveData();
            data.bestScoreAll = bestScore;
            data.bestScoreNameAll = bestScoreInputName;
            
            json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
        
    }

    public void LoadScoreAndName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScoreAll;
            bestScoreInputName = data.bestScoreNameAll;

        }
    }

    public string LoadBestScoreNameToMain()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
            
            return data.bestScoreNameAll;

        }
        else
        {
            return GetBestScoreName();// if first load
        }
    }
}
