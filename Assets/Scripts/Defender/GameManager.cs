using System;
using UnityEngine;

namespace Sandbox.Gary
{
    public class GameManager : MonoBehaviour
    {
        public GameObject gameOverUI;
        public static bool IsOver;

        private void Awake()
        {
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
    }
}
