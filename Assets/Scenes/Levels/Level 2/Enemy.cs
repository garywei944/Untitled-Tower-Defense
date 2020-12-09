using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;
    public float gravity = 10f;
    private Vector3 moveDirection = Vector3.zero;

    private Transform _target;
    private int _pointIndex;

    private Animator _animator;
    private CharacterController controller;
    private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");
    private const float err = 3f;

    public GameObject enemyCamera;
    public GameObject mainCamera;

    private bool autoMoving;
    private bool thirdPersonView;

    public GameObject finalZone;


    private void Start()
    {
        _target = PathPoints.pathPoints[_pointIndex];
        _animator = gameObject.GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        autoMoving = true;

        enemyCamera.SetActive(false);
        mainCamera = GameObject.Find("MainCamera");
        finalZone = GameObject.Find("Final");
    }

    private void Update()
    {
        
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

        // Reach the current path point
        if (Vector3.Distance(_target.position, transform.position) < err / 2)
        {
            _pointIndex++;
            if (_pointIndex >= PathPoints.pathPoints.Length)
            {
                Destroy(gameObject);
                Debug.Log("reach end");
                finalZone.GetComponent<FinalZone>().reachNumber += 1;
                return;
            }

            _target = PathPoints.pathPoints[_pointIndex];
        }

        if(Vector3.Distance(transform.position, new Vector3(0, 0, 0)) < err / 2)
        {
            Debug.Log("reach end");
            Die();
            finalZone.GetComponent<FinalZone>().reachNumber += 1;
            return;
        }
    }

    public void Die()
    {
        mainCamera.SetActive(true);
        enemyCamera.SetActive(false);
        Destroy(gameObject);
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

    }
}
