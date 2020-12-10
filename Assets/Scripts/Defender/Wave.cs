using System;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Sandbox.Gary
{
    [Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public int count;
        public float rate;

        public Wave(GameObject enemyPrefab, int count, float rate)
        {
            this.enemyPrefab = enemyPrefab;
            this.count = count;
            this.rate = rate;
        }
    }
}
