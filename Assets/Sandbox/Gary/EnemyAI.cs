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


        // Start is called before the first frame update
        private void Start()
        {
            _target = PathPoints.pathPoints[_pointIndex];
            _animator = gameObject.GetComponentInChildren<Animator>();
            _controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        private void Update()
        {
            _animator.SetInteger("AnimationPar", 1);
            var direction = _target.position - transform.position;
            Debug.Log(direction);
            // transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
            _controller.Move(direction.normalized * (speed * Time.deltaTime));
            if (!(Vector3.Distance(_target.position, transform.position) < 0.2f)) return;
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
