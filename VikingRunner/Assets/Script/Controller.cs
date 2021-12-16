using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Controller : MonoBehaviour
{
    [SerializeField] Vector3 Direction = new Vector3(0, 0, 0);
    [SerializeField] float movingSpeed = 5f;
    [SerializeField] float rotationSpeed = 360f;
    [SerializeField] int jumpForce = 400;

    private Rigidbody rigidbody;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private RaycastHit raycastHit;
    private bool isOnGround = true,isRunning = false;

    //self define function
    private void BasicControl() {
        isRunning = false;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //rotate character
        if (Input.GetKey(KeyCode.D))
        {
            Direction =  new Vector3(0, horizontalInput, 0);
            transform.Rotate(rotationSpeed * Time.deltaTime * Direction);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Direction = new Vector3(0, horizontalInput, 0);
            transform.Rotate(rotationSpeed * Time.deltaTime * Direction);
        }

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

    private void MouseNavigation() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                navMeshAgent.SetDestination(raycastHit.point);
            }
        }
    }

    //avoid air jumping
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Ground")) {
            isOnGround = true;
        }
        
    }

    // Start is called before the first frame update (when enable)
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        BasicControl();
        animator.SetBool("isRunning", isRunning);
    }
}
