using System;
using UnityEngine;

namespace Sandbox.Gary
{
    public class PlayerStatus : MonoBehaviour
    {
        public const int StartMoney = 500;
        public static int Money;

        private void Start()
        {
            Money = StartMoney;
        }
    }
}
