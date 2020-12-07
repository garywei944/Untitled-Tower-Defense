using UnityEngine;

namespace Sandbox.Gary
{
    public class EnemyAI : MonoBehaviour
    {
        public float speed = 10;

        private Transform _target;
        private int _pointIndex;

        private Animator _animator;
        private CharacterController _controller;
        private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");


        private void Start()
        {
            _target = PathPoints.pathPoints[_pointIndex];
            _animator = gameObject.GetComponentInChildren<Animator>();
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            // Set animation
            _animator.SetInteger(AnimationPar, 1);

            // Update move
            var direction = _target.position - transform.position;
            const float err = 0.4f;
            if (direction.x > err)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (direction.x < -err)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
            else if (direction.z > err)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }


            _controller.Move(direction.normalized * (speed * Time.deltaTime));

            // Reach the current path point
            if (Vector3.Distance(_target.position, transform.position) < err / 2)
            {
                _pointIndex++;
                if (_pointIndex >= PathPoints.pathPoints.Length)
                {
                    Destroy(gameObject);
                    return;
                }

                _target = PathPoints.pathPoints[_pointIndex];
            }
        }
    }
}
