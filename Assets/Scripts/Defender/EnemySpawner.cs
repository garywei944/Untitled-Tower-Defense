using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Sandbox.Gary
{
    public class EnemySpawner : MonoBehaviour
    {
        public static int EnemyAlive;

        public bool unlimit;
        public Wave[] waveEnemy;
        public float spawnInterval = 5.0f;
        public Text TimerText;
        public List<GameObject> prefabs;

        private Transform _spawnPoint;

        private float _countDown;
        private int _waveIndex;

        private readonly Random _random = new Random();

        // Start is called before the first frame update
        private void Start()
        {
            _countDown = spawnInterval;
            _spawnPoint = gameObject.transform.Find("SpawnPoint");
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameManager.IsOver)
            {
                EnemyAlive = 0;
                return;
            }

            if (EnemyAlive > 0)
            {
                return;
            }

            if (!unlimit)
            {
                if (_waveIndex == waveEnemy.Length)
                {
                    GameManager.Instance.GameWin();
                    enabled = false;
                }
            }

            _countDown -= Time.deltaTime;
            _countDown = Mathf.Clamp(_countDown, 0, Mathf.Infinity);
            var msg = _countDown > 0 ? $"Next Wave in {_countDown:00.00}s" : $"Wave {PlayerStatus.Rounds + 1}";

            TimerText.text = msg;
            if (_countDown <= 0)
            {
                _countDown = spawnInterval;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            StartCoroutine(WaveEnemy());
        }

        private IEnumerator WaveEnemy()
        {
            if (!unlimit)
            {
                if (_waveIndex >= waveEnemy.Length)
                {
                    yield break;
                }
            }

            PlayerStatus.Rounds++;

            var index = _random.Next(3);

            var wave = _waveIndex >= waveEnemy.Length
                ? new Wave(prefabs[index], _random.Next(3, 12 + PlayerStatus.Rounds * 2), _random.Next(10) / 10.0f)
                : waveEnemy[_waveIndex];
            EnemyAlive = wave.count;

            for (var i = 0; i < wave.count; i++)
            {
                Instantiate(wave.enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
                yield return new WaitForSeconds(0.3f);
            }

            _waveIndex++;
        }
    }
}
