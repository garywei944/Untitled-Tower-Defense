using System;
using UnityEngine;

namespace Sandbox.Gary
{
    public class PlayerStatus : MonoBehaviour
    {
        public static int startMoney = 500;
        public static int Money;
        public static int startLives = 3;
        public static int Lives;

        public static int Rounds;

        private void Awake()
        {
            Money = startMoney;
            Lives = startLives;
            Rounds = 0;
        }

        public static void AddMoney()
        {
            Money += 50;
        }

    }
}
