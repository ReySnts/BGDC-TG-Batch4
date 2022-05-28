using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Body")]
    public Rigidbody rigidBody = null;
    public Transform shadow = null;
    BoxCollider boxCol = null;
    CapsuleCollider capsCol = null;
    float x = 0f;
    float z = 0f;
    [Header("Move Speed")]
    [Range(0f, 20f)] [SerializeField] float walkSpeed = 10f;
    [Range(0f, 20f)] [SerializeField] float jumpForce = 15f;
    [Header("Animation")]
    public Animator playerControlAnim = null;
    [SerializeField] bool leftTurn = false;
    [SerializeField] bool rightTurn = false;
    bool firstRightWalk = false;
    bool isCrouch = false;
    float tempX = 0f;
    [Header("Masking")]
    public Transform groundCheck = null;
    public LayerMask groundMask;
    [SerializeField] bool isGround = false;
    [SerializeField] float groundMaxDist = 0.06f;
    [Header("Event")]
    public UnityEvent OnLand = null;
    public void Landing()
    {
        playerControlAnim.SetBool("IsJumping", false);
    }
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        playerControlAnim = this.GetComponentInChildren<Animator>();
        boxCol = this.GetComponent<BoxCollider>();
        capsCol = this.GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        // Walk
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.A) && x - tempX < 0f && !leftTurn)
        {
            transform.Rotate(Vector3.up * 180f);
            leftTurn = true;
            rightTurn = false;
            firstRightWalk = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && x - tempX > 0f && !rightTurn)
        {
            transform.Rotate(Vector3.up * 180f);
            rightTurn = true;
            leftTurn = false;
            if (!firstRightWalk)
            {
                transform.Rotate(Vector3.up * 180f);
                firstRightWalk = true;
            }
        }
        shadow.localScale = Vector3.one * 0.5f;
        boxCol.enabled = false;
        capsCol.enabled = true;
        // Crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rigidBody.velocity = new Vector3(x * walkSpeed * 0.5f, 0f, z * walkSpeed * 0.5f);
            shadow.localScale = new Vector3(1f, 0.5f, 1f);
            capsCol.enabled = false;
            boxCol.enabled = true;
            isCrouch = true;
        }
        // Run
        else if (Input.GetKey(KeyCode.LeftShift))
            rigidBody.velocity = new Vector3(x * walkSpeed * 2f, 0f, z * walkSpeed * 2f);
        // Normal
        else
        {
            rigidBody.velocity = new Vector3(x * walkSpeed, 0f, z * walkSpeed);
            isCrouch = false;
        }
        tempX = x;
        playerControlAnim.SetFloat("WalkSpeed", Mathf.Abs(x * walkSpeed));
        playerControlAnim.SetFloat("ZWalkSpeed", Mathf.Abs(z * walkSpeed));
        playerControlAnim.SetBool("IsCrouching", isCrouch);
        // Jump
        if (isGround && Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity += new Vector3(x * walkSpeed * 0.5f, 10f * jumpForce, 0f);
            playerControlAnim.SetBool("IsJumping", true);
        }
        else if (!isGround) rigidBody.velocity += new Vector3(x * walkSpeed * 0.5f, -jumpForce, 0f);
        else if (isGround) OnLand.Invoke();
    }
    void FixedUpdate()
    {
        if (
            Physics.Raycast(
                groundCheck.position,
                Vector3.down,
                groundMaxDist,
                groundMask
            )
        ) isGround = true;
        else isGround = false;
    }
}