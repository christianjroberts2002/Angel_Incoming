using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] float maxJumpHeight;
    [SerializeField] float maxJumpHeightOriginal;

    [SerializeField] float downardMultiplier = 3f;

    [SerializeField] private float jumpForce;

    [SerializeField] bool isGrounded;
    [SerializeField] private bool canBeGrounded;
    [SerializeField] private float waitTime;

    [SerializeField] private bool isMoving;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private float rotSpeed;
    [SerializeField] private float mg;

    private Vector3 targetVBeforeJump;

    private bool forward;
    private bool backward;
    private bool right;
    private bool left;

    [SerializeField] private float hor;
    [SerializeField] private float vert;

    private float maxRBVelocity = 1f;



    private Vector3 mousePos;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        maxJumpHeightOriginal = maxJumpHeight;
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        
    }

    private void Update()
    {
        MovePlayer();
        LookAtMouse();
    }

    void FixedUpdate()
    {
        Jump();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(!isGrounded && canBeGrounded)
        {
            Debug.Log("collision");
            if(other.tag == "Ground")
            {
                isGrounded = true;
                Debug.Log("ground");
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGrounded)
        {
            //rb.AddRelativeForce(Vector3.down * (jumpForce), ForceMode.Impulse);
        }
        
    }


    private void MovePlayer()
    {
        hor = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");

        if (Input.anyKey)
        {
            isMoving = true;
        }
        if (!Input.anyKey && isGrounded)
        {
            isMoving = false;

            //rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            

        }

        Forward();
        Backward();
        Right();
        Left();

        if (!isGrounded)
        {
            return;
        }


        if (vert > 0)
        {
            forward = true;

        }
        else
        {
            forward = false;
        }
        if (vert < 0)
        {
            backward = true;

        }
        else
        {
            backward = false;
        }
        if (hor > 0)
        {
            right = true;

        }
        else
        {
            right = false;
        }
        if (hor < 0)
        {
            left = true;

        }
        else
        {

            left = false;
        }

    }

    private void Forward()
    {
        Vector3 forwardDir = new Vector3(-.5f, 0, .5f);
        if (forward)
        {
                transform.Translate(forwardDir * speed * Time.deltaTime, Space.World);
        }


    }

    private void Backward()
    {
        Vector3 backwardDir = new Vector3(.5f, 0, -.5f);
        if (backward)
            transform.Translate(backwardDir * speed * Time.deltaTime, Space.World);
    }

    private void Right()
    {
        Vector3 righttDir = new Vector3(.5f, 0, .5f);
        if (right)
            transform.Translate(righttDir * speed * Time.deltaTime, Space.World);

    }
    private void Left()
    {
        Vector3 leftDir = new Vector3(-.5f, 0, -.5f);
        if (left)
            transform.Translate(leftDir * speed * Time.deltaTime, Space.World);
    }

    private void Jump()
    {
        
        if (Input.GetKey(KeyCode.Space) && isGrounded && rb.velocity.y < maxRBVelocity)
        {
            maxJumpHeight = maxJumpHeightOriginal + transform.position.y;
            targetVBeforeJump = MousePosition.Instance.GetMousePosition();

            rb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            Debug.Log(rb.velocity.y);
            StartCoroutine(WaitForOnGround());
        }

        if(transform.position.y > maxJumpHeight)
        {
            

            rb.AddRelativeForce(Vector3.down * (jumpForce * downardMultiplier), ForceMode.Force);
        }
    }


    private void LookAtMouse()
    {
        Vector3 mousePosition = MousePosition.Instance.GetMousePosition();
        Vector3 targetDir = mousePosition - transform.position;

        Vector3 dir = Vector3.RotateTowards(transform.forward, targetDir, rotSpeed * Time.deltaTime, mg);

        dir = new Vector3(dir.x, 0, dir.z);
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    private IEnumerator WaitForOnGround()
    {
        canBeGrounded = false;
        yield return new WaitForSeconds(waitTime);
        canBeGrounded = true;

    }

    

}
