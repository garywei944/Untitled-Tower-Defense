using System;
using UnityEngine;

namespace Sandbox.Gary
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private int startMoney = 500;
        public static int Money;
        [SerializeField] private int startLives = 3;
        public static int Lives;

        private void Start()
        {
            Money = startMoney;
            Lives = startLives;
        }
    }
}
