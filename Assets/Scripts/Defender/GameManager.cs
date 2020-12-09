using System;
using UnityEngine;

namespace Sandbox.Gary
{
    public class GameManager : MonoBehaviour
    {
        public GameObject gameOverUI;
        public GameObject winLevelUI;
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
    }
}
