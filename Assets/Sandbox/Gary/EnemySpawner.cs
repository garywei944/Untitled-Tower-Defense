using System.Collections;
using UnityEngine;

namespace Sandbox.Gary
{
    public class EnemySpawner : MonoBehaviour
    {
        public static int EnemyAlive;

        public Wave[] waveEnemy;
        public float spawnInterval = 5.0f;

        private Transform _spawnPoint;
        private float _countDown;
        private int _waveIndex;


        // Start is called before the first frame update
        private void Start()
        {
            _countDown = spawnInterval;
            _spawnPoint = gameObject.transform.Find("SpawnPoint");
        }

        // Update is called once per frame
        private void Update()
        {
            if (EnemyAlive > 0)
            {
                return;
            }

            if (_waveIndex == waveEnemy.Length)
            {
                // TODO: Victory
                Debug.Log("Victory");
            }

            _countDown -= Time.deltaTime;
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
            if (_waveIndex >= waveEnemy.Length)
            {
                yield break;
            }

            var wave = waveEnemy[_waveIndex];
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
