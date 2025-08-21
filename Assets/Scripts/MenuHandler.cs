using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public TMP_InputField userNameInput;
    public Button startButton;

    void Start()
    {
        // All'inizio disabilita il bottone
        startButton.interactable = false;

        // Ogni volta che cambia il testo, controlla
        userNameInput.onValueChanged.AddListener(delegate { ValidateInput(); });

        // Controllo iniziale (caso in cui ci sia già un nome salvato)
        ValidateInput();

    }

    void ValidateInput()
    {
        // Abilita il bottone solo se c’è testo
        startButton.interactable = !string.IsNullOrWhiteSpace(userNameInput.text);
    }
    public void StartGame()
    {
        if (PersistenceManager.Instance != null)
        {
            string name = userNameInput.text.Trim();
            PersistenceManager.Instance.CurrentUserName = name;
            Debug.Log($"Nome utente registrato: {PersistenceManager.Instance.CurrentUserName}");
        }
        else
        {
            Debug.LogWarning("PersistenceManager non trovato nel StartGame");
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}
