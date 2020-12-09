using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalZone : MonoBehaviour
{
    public static FinalZone instance;

    public GameObject winPanel;
    public Button nextButton;

    public GameObject lossPanel;
    public Button retryButton;
    public Button quitButton;

    public int reachNumber;
    public int winCondition = 5;

    private bool loss;

    private void Awake()
    {
        instance = this;
        reachNumber = 0;

        winPanel.SetActive(false);
        lossPanel.SetActive(false);
        loss = false;

        nextButton.onClick.AddListener(LoadNextPage);
        retryButton.onClick.AddListener(ReloadPage);
        quitButton.onClick.AddListener(GoToMainMenu);
    }

    public FinalZone GetInstance()
    {
        if (instance == null)
            instance = this;
        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(reachNumber >= winCondition)
        {
            //TODO win
            Debug.Log("win");
            winPanel.SetActive(true);
        }

        if (loss)
        {
            Debug.Log("loss");
            lossPanel.SetActive(true);
        }
    }

    public void Loss()
    {
        loss = true;
    }

    void LoadNextPage()
    {
        Debug.Log("click");
        SceneManager.LoadScene("WinPage", LoadSceneMode.Single);
    }

    void ReloadPage()
    {
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void AddReachNum()
    {
        reachNumber += 1;
        Debug.Log("reach: " + reachNumber);
    }
}
