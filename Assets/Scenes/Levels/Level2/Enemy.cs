using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float HP = 100f;
    public float speed = 10f;
    public float turnSpeed = 50f;
    public float gravity = 10f;
    private Vector3 moveDirection = Vector3.zero;

    private Transform _target;
    private int _pointIndex;

    private Animator _animator;
    private CharacterController controller;
    private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");

    public GameObject enemyCamera;
    public GameObject mainCamera;

    private bool autoMoving;
    private bool thirdPersonView;


    private void Start()
    {
        _target = PathPoints.pathPoints[_pointIndex];
        _animator = gameObject.GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        autoMoving = true;

        enemyCamera.SetActive(false);
        mainCamera = GameObject.Find("MainCamera");
    }

    private void Update()
    {
        if(HP <= 0)
        {
            if (thirdPersonView)
            {
                OnMouseUpAsButton();
            }
            Destroy(gameObject);
        }
        
        
        if (autoMoving)
        {
            AutoMove();
        }
        else
        {
            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            {
                _animator.SetInteger("AnimationPar", 1);
            }
            else
            {
                _animator.SetInteger("AnimationPar", 0);
            }
            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;



            moveDirection.y -= gravity * Time.deltaTime;

            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    private void OnMouseUpAsButton()
    {
        autoMoving = !autoMoving;
        thirdPersonView = !thirdPersonView;
        mainCamera.SetActive(!thirdPersonView);
        enemyCamera.SetActive(thirdPersonView);
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
