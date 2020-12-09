using UnityEngine;

namespace Sandbox.Gary
{
    public class Turret : MonoBehaviour
    {
        public float range;
        public float rotSpeed;
        private const string EnemyTag = "Enemy";
        [HideInInspector] public Transform target;

        [Header("Use Bullet")] public GameObject bulletPrefab;
        public float bulletRate = 2f;

        private Transform _bulletPoint;
        private float _countDown;

        [Header("Use Laser")] public bool useLaser;
        public LineRenderer lineRender;
        public float overTimeDmg = 30;
        public float slowPct = 0.3f;
        private EnemyAI _enemyMove;
        private EnemyHealth _enemyHp;
        public ParticleSystem impactEffect;
        public Light pointLight;

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
            if (!target)
            {
                if (!useLaser) return;
                lineRender.enabled = false;
                impactEffect.Stop();
                pointLight.enabled = false;
                return;
            }

            LockTarget();

            if (useLaser)
            {
                Laser();
            }
            else
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            _countDown -= Time.deltaTime;
            if (!(_countDown <= 0)) return;
            var bulletGo = Instantiate(bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
            var bullet = bulletGo.GetComponent<Bullet>();
            if (!bullet)
            {
                bullet = bulletGo.AddComponent<Bullet>();
            }

            bullet.SetTarget(target);
            _countDown = 1 / bulletRate;
        }

        private void Laser()
        {
            if (!lineRender.enabled)
            {
                lineRender.enabled = true;
                impactEffect.Play();
                pointLight.enabled = true;
            }

            // Damage enemy
            _enemyHp.Damage(overTimeDmg * Time.deltaTime, EnemyHealth.DamageType.Laser);

            // Slow Enemy
            _enemyMove.Slow(slowPct);

            // Draw the laser line
            var bulletPosition = _bulletPoint.position;
            var targetPosition = target.position;
            targetPosition.y += 1;
            lineRender.SetPosition(0, bulletPosition);
            lineRender.SetPosition(1, targetPosition);

            // Change Effect position and rotation 
            var dir = bulletPosition - targetPosition;
            impactEffect.transform.transform.position = targetPosition + dir.normalized * 1;
            impactEffect.transform.rotation = Quaternion.LookRotation(dir.normalized);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        private void UpdateTarget()
        {
            var enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
            var minDistance = Mathf.Infinity;
            Transform nearestEnemy = null;
            foreach (var enemy in enemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }

            if (minDistance < range)
            {
                target = nearestEnemy;
                _enemyHp = target.GetComponent<EnemyHealth>();
                _enemyMove = target.GetComponent<EnemyAI>();
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
