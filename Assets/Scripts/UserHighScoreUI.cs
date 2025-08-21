using UnityEngine;
using TMPro;

public class UserHighScoreUI : MonoBehaviour
{
    public TMP_Text userNameText;

    void Start()
    {
        if (PersistenceManager.Instance != null && userNameText != null)
        {
            PersistenceManager.Instance.RegisterUserNameText(userNameText);
        }
    }
}
