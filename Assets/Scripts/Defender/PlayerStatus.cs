using System;
using UnityEngine;

namespace Sandbox.Gary
{
    public class PlayerStatus : MonoBehaviour
    {
        public static readonly int startMoney = 500;
        public static int Money;
        public static readonly int startLives = 5;
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
