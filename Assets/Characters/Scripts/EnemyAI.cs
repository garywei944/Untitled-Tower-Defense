using UnityEngine;

namespace Sandbox.Gary
{
    public class EnemyAI : MonoBehaviour
    {
        public float startSpeed = 10;
        private float _moveSpeed;

        private Transform _target;
        private int _pointIndex;

        private Animator _animator;
        private CharacterController _controller;
        private Canvas _canvas;
        private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");
        private AudioSource _audioSource;
        public AudioClip enterClip;


        private void Start()
        {
            _target = PathPoints.pathPoints[_pointIndex];
            _animator = gameObject.GetComponentInChildren<Animator>();
            _controller = GetComponent<CharacterController>();
            _canvas = gameObject.GetComponentInChildren<Canvas>();
            _moveSpeed = startSpeed;
            _audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
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

            _controller.Move(direction.normalized * (_moveSpeed * Time.deltaTime));

            // Fix the HP bar rotation
            _canvas.transform.rotation = Quaternion.Euler(45, 0, 0);

            // Reach the current path point
            if (Vector3.Distance(_target.position, transform.position) < err / 2)
            {
                _pointIndex++;
                if (_pointIndex >= PathPoints.pathPoints.Length)
                {
                    PathEnd();
                    return;
                }

                _target = PathPoints.pathPoints[_pointIndex];
            }

            // Reset speed
            _moveSpeed = startSpeed;
        }

        private void PathEnd()
        {
            if (PlayerStatus.Lives > 0)
            {
                PlayerStatus.Lives--;
            }
            else
            {
                Debug.Log("Lose");
            }

            EnemySpawner.EnemyAlive--;
            _audioSource.PlayOneShot(enterClip);
            Destroy(gameObject);
        }

        public void Slow(float pct)
        {
            _moveSpeed = startSpeed * (1 - pct);
        }
    }
}
