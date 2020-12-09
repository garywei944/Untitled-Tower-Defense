using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Button startButton;
    public Button helpButton;
    public Button quitButton;
    public Button levelOne;
    public Button levelTwo;
    public Button backButtonLevel;
    public Button backButtonHelp;
    public Button aboutButton;
    public Button backButtonAbout;

    public GameObject mainMenuPanel;
    public GameObject chooseLevelPanel;
    public GameObject helpPanel;
    public GameObject aboutPanel;

    private Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuPanel.SetActive(true);
        chooseLevelPanel.SetActive(false);
        helpPanel.SetActive(false);
        aboutPanel.SetActive(false);
        startButton.onClick.AddListener(ChooseLevel);
        helpButton.onClick.AddListener(ShowInstruction);

        levelButtons = new Button[2];
        levelButtons[0] = levelOne;
        levelButtons[1] = levelTwo;
        for(int i = 0; i < 2; i++)
        {
            int index = i;
            levelButtons[i].onClick.AddListener(delegate ()
            {
                LoadLevel(index + 1);
            });
        }

        aboutButton.onClick.AddListener(ShowAbout);
        backButtonHelp.onClick.AddListener(BackToMainMenu);
        backButtonLevel.onClick.AddListener(BackToMainMenu);
        backButtonAbout.onClick.AddListener(BackToMainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowAbout()
    {
        aboutPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    void ShowInstruction()
    {
        helpPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        chooseLevelPanel.SetActive(false);
        helpPanel.SetActive(false);
        aboutPanel.SetActive(false);
    }

    void LoadLevel(int level)
    {
        string levelName = "Level";
        levelName = levelName + level;
        Debug.Log(levelName);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    void ChooseLevel()
    {
        chooseLevelPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
