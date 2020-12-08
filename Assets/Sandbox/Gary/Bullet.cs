using UnityEngine;

namespace Sandbox.Gary
{
    public class Bullet : MonoBehaviour
    {
        private Transform _mTarget;
        public float speed = 80f;
        public float damage = 20;

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
            EnemyDamage();
            Destroy(gameObject);
        }

        private void EnemyDamage()
        {
            var enemyHp = _mTarget.GetComponent<EnemyHealth>();
            if (enemyHp)
            {
                enemyHp.Damage(damage);
            }
        }
    }
}
