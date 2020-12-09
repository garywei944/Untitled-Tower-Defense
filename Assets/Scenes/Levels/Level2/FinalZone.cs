using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalZone : MonoBehaviour
{
    public static FinalZone instance;

    public int reachNumber;
    public int winCondition = 5;

    private void Awake()
    {
        instance = this;
        reachNumber = 0;
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
            Debug.Log("win");
            SceneManager.LoadScene("WinPage", LoadSceneMode.Single);
        }
    }

    public void AddReachNum()
    {
        reachNumber += 1;
        Debug.Log("reach: " + reachNumber);
    }
}
