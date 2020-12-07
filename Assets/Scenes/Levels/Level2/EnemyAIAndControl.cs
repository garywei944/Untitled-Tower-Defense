using UnityEngine;


public class EnemyAIAndControl : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 400f;
    public float gravity = 10f;
    private Vector3 moveDirection = Vector3.zero;

    private Transform _target;
    private int _pointIndex;

    private Animator _animator;
    private CharacterController controller;
    private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");

    public GameObject enemyCamera;
    public GameObject mainCamera;

    private bool AutoMoving;


    private void Start()
    {
        _target = PathPoints.pathPoints[_pointIndex];
        _animator = gameObject.GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        AutoMoving = true;

        enemyCamera.SetActive(false);
    }

    private void Update()
    {

        if (AutoMoving)
        {
            AutoMove();
        }
        else
        {
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    private void OnMouseUpAsButton()
    {
        AutoMoving = !AutoMoving;
        mainCamera.SetActive(false);
        enemyCamera.SetActive(true);
    }

    private void AutoMove()
    {
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


        controller.Move(direction.normalized * (speed * Time.deltaTime));

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
