using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Controller : MonoBehaviour
{
    [SerializeField] private AnimationCurve swipeRotateAnimationCurve; // configure in inspector
    private Coroutine swipeRotateCoroutine = null;
    private float swipeRotationDuration = 0.5f; // duration of rotation in seconds

    Vector3 Direction = new Vector3(0, 0, 0);
    float movingSpeed = 5f;
    float rotationSpeed = 360f;
    int jumpForce = 300;

    private Rigidbody rigidbody;
    private Animator animator;
    private RaycastHit raycastHit;
    private bool isOnGround = true,isRunning = false;


    //self define function
    private IEnumerator HorizontalRotate(float degreesRight)
    {
        float t = 0;
        Quaternion startRot = transform.rotation;

        // update rotation until 
        while (t < 1f)
        {
            // let next frame occur
            yield return null;

            // update timer
            t = Mathf.Min(1f, t + Time.deltaTime / swipeRotationDuration);

            // Find how much rotation corresponds to time at t:
            float degrees = degreesRight * swipeRotateAnimationCurve.Evaluate(t);

            // Apply that amount of rotation to the starting rotation:
            transform.rotation = startRot * Quaternion.Euler(0f, degrees, 0f);
        }

        // allow for next swipe
        swipeRotateCoroutine = null;
    }

    private void BasicControl() {
        isRunning = false;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //rotate character
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (swipeRotateCoroutine != null)
            {
                StopCoroutine(swipeRotateCoroutine);
                swipeRotateCoroutine = null;
            }

            swipeRotateCoroutine = StartCoroutine(HorizontalRotate(90f));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (swipeRotateCoroutine != null)
            {
                StopCoroutine(swipeRotateCoroutine);
                swipeRotateCoroutine = null;
            }

            swipeRotateCoroutine = StartCoroutine(HorizontalRotate(-90f));
        }

        //move forward
        if (Input.GetKey(KeyCode.W)){
            Direction = transform.rotation * Vector3.forward;
            transform.localPosition += movingSpeed * Time.deltaTime * Direction;
            isRunning = true;
        }

        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            Direction = Vector3.up;
            rigidbody.AddForce(jumpForce * Direction);
            isOnGround = false;
        }
    }

    private void CheckFallToDeath() {
        if (transform.localPosition.y < -7)
        {
            EnemyAI enemyAI = FindObjectOfType<EnemyAI>();
            enemyAI.GameOver();
        }
    }


    //avoid air jumping
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Ground") || collision.gameObject.name.Contains("Bridge")) {
            isOnGround = true;
        }
        
    }

    // Start is called before the first frame update (when enable)
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        BasicControl();
        animator.SetBool("isRunning", isRunning);
        CheckFallToDeath();
    }
}
