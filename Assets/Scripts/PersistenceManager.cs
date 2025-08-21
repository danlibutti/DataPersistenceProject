using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;

    // Riferimento corrente al TMP_Text registrato dalla scena
    public TMP_Text userNamePlusHighscore;

    public string UserName;
    public string CurrentUserName;
    public int UserHighScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadUserData();

        // Ascolta i caricamenti scena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Non toccare la UI qui, la registrazione avviene tramite UserNameUI
    }

    /// <summary>
    /// Registra il TMP_Text della scena corrente e aggiorna la UI
    /// </summary>
    public void RegisterUserNameText(TMP_Text textComponent)
    {
        if (textComponent != null)
        {
            userNamePlusHighscore = textComponent;
            UpdateUI();
        }
    }

    /// <summary>
    /// Aggiorna il testo registrato
    /// </summary>
    public void UpdateUI()
    {
        if (userNamePlusHighscore != null)
        {
            userNamePlusHighscore.text = $"Highscore: {UserName} : {UserHighScore}";
        }
    }

    /// <summary>
    /// Imposta un nuovo username e aggiorna UI e file
    /// </summary>
    public void SetUserName(string newName)
    {
        UserName = newName;
        SaveUserData();
        UpdateUI();
    }

    // --- Serializzazione dati ---
    [System.Serializable]
    class SaveData
    {
        public string UserName;
        public int UserScore;
    }

    public void SaveUserData()
    {
        SaveData data = new SaveData
        {
            UserName = CurrentUserName,
            UserScore = UserHighScore
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadUserData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            UserName = data.UserName;
            UserHighScore = data.UserScore;

            UpdateUI();
        }
    }
}

