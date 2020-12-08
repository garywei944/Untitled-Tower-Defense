using UnityEngine;

namespace Sandbox.Gary
{
    public class GameManager : MonoBehaviour
    {
        public static bool IsOver;

        private void Update()
        {
            if (IsOver) return;
            if (PlayerStatus.Lives <= 0)
            {
                GameEnd();
            }
        }

        private static void GameEnd()
        {
            IsOver = true;
        }
    }
}
