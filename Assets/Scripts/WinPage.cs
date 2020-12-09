using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPage : MonoBehaviour
{
    public Button mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.onClick.AddListener(GoToMainMenu);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
