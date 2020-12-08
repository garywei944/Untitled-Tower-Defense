using UnityEngine;

namespace Sandbox.Gary
{
    public class Turret : MonoBehaviour
    {
        public float range = 5.0f;
        public float rotSpeed = 10.0f;
        public float bulletRate = 2f;
        public GameObject bulletPrefab;

        public string enemyTag = "Enemy";

        [HideInInspector] public Transform target;
        private Transform _bulletPoint;
        private float _countDown;

        // Start is called before the first frame update
        private void Start()
        {
            _bulletPoint = gameObject.transform.Find("ShootPoint");
            InvokeRepeating(nameof(UpdateTarget), 0, 0.5f);
            _countDown = 1 / bulletRate;
        }


        // Update is called once per frame
        private void Update()
        {
            if (!target) return;
            LockTarget();
            _countDown -= Time.deltaTime;
            if (_countDown <= 0)
            {
                var bulletGo = Instantiate(bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
                var bullet = bulletGo.GetComponent<Bullet>();
                if (!bullet)
                {
                    bullet = bulletGo.AddComponent<Bullet>();
                }
                bullet.SetTarget(target);
                _countDown = 1 / bulletRate;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        private void UpdateTarget()
        {
            var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            var minDistance = Mathf.Infinity;
            Transform nearestEnemy = null;
            foreach (var enemy in enemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy.transform;
                }
            }

            if (minDistance < range)
            {
                target = nearestEnemy;
            }
            else
            {
                target = null;
            }
        }

        private void LockTarget()
        {
            var dir = target.position - transform.position;
            var rotation = Quaternion.LookRotation(dir);
            var lerpRot = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0, lerpRot.eulerAngles.y, 0));
        }
    }
}
