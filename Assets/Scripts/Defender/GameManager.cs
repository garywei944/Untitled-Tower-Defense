using System;
using UnityEngine;

namespace Sandbox.Gary
{
    public class GameManager : MonoBehaviour
    {
        public GameObject gameOverUI;
        public GameObject winLevelUI;
        public PauseUI pauseUI;
        public static bool IsOver;
        private static GameManager _instance;

        public static GameManager Instance => _instance;

        private void Awake()
        {
            _instance = this;
            IsOver = false;
        }

        private void Update()
        {
            if (IsOver) return;
            if (PlayerStatus.Lives <= 0)
            {
                GameEnd();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseUI.SwitchUI();
            }
        }

        private void GameEnd()
        {
            IsOver = true;
            gameOverUI.SetActive(true);
        }

        public void GameWin()
        {
            IsOver = true;
            winLevelUI.SetActive(true);
        }

        public void ResetGame()
        {
            IsOver = false;
            PlayerStatus.Money = PlayerStatus.startMoney;
            PlayerStatus.Lives = PlayerStatus.startLives;
            PlayerStatus.Rounds = 0;
        }
    }
}
