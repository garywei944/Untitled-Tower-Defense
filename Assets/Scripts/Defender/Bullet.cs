using UnityEngine;

namespace Sandbox.Gary
{
    public class Bullet : MonoBehaviour
    {
        private Transform _mTarget;
        public float speed = 80f;
        public float damage = 20;
        public float explosionRadius;

        public void SetTarget(Transform target)
        {
            _mTarget = target;
        }

        private void Update()
        {
            if (!_mTarget)
            {
                Destroy(gameObject);
                return;
            }

            var dir = _mTarget.position - transform.position;
            if (Vector3.Distance(_mTarget.position, transform.position) < speed * Time.deltaTime)
            {
                // Hit target
                HitTarget();
            }

            transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);
            transform.LookAt(_mTarget);
        }

        private void HitTarget()
        {
            if (explosionRadius > 0)
            {
                Explosion();
            }
            else
            {
                EnemyDamage(_mTarget);
            }

            Destroy(gameObject);
        }

        private void EnemyDamage(Component enemy)
        {
            var enemyHp = enemy.GetComponent<EnemyHealth>();
            if (enemyHp)
            {
                enemyHp.Damage(damage);
            }
        }

        private void Explosion()
        {
            var colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var item in colliders)
            {
                if (item.CompareTag("Enemy"))
                {
                    EnemyDamage(item);
                }
            }
        }
    }
}
