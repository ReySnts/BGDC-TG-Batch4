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
    [Range(0f, 20f)] [SerializeField] float walkSpeed = 4f;
    [Range(0f, 20f)] public float jumpForce = 7.5f;
    [Header("Animation")]
    public Animator playerControlAnim = null;
    bool leftTurn = false;
    bool rightTurn = false;
    [SerializeField] bool isCrouch = false;
    [SerializeField] bool isRun = false;
    [SerializeField] bool startJump = false;
    [SerializeField] bool startLand = false;
    bool firstRightWalk = false;
    float tempX = 0f;
    float tempZ = 0f;
    [Header("Masking")]
    public Transform groundCheck = null;
    public LayerMask groundMask;
    [SerializeField] bool isGround = false;
    float groundMaxDist = 0.59f;
    [Header("Sounds")]
    [SerializeField] bool isPressed = false;
    public AudioSource walkSound = null;
    public AudioSource crouchSound = null;
    public AudioSource runSound = null;
    public AudioSource jumpSound = null;
    public AudioSource landSound = null;
    [Header("Event")]
    public UnityEvent OnLand = null;
    public void Landing()
    {
        landSound.Play();
        playerControlAnim.SetBool("IsJumping", false);
        startLand = false;
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
        if (!isPressed && (x != 0f || z != 0f))
        {
            walkSound.Play();
            isPressed = true;
        }
        else if (x == 0f && z == 0f)
        {
            walkSound.Stop();
            runSound.Stop();
            isPressed = false;
        }
        // Turn Left or Right
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
        // References
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
            if (!isCrouch)
            {
                crouchSound.Play();
                isCrouch = true;
            }
            runSound.Stop();
        }
        // Run
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            rigidBody.velocity = new Vector3(x * walkSpeed * 2f, 0f, z * walkSpeed * 2f);
            if (!isRun && (x != 0f || z != 0f))
            {
                runSound.Play();
                isRun = true;
            }
            walkSound.Stop();
        }
        // Normal
        else
        {
            rigidBody.velocity = new Vector3(x * walkSpeed, 0f, z * walkSpeed);
            if (isRun)
            {
                runSound.Stop();
                walkSound.Play();
                isRun = false;
            }
            if (isCrouch)
            {
                crouchSound.Play();
                isCrouch = false;
            }
        }
        // Turn Left or Right
        tempX = x;
        tempZ = z;
        // Set Animator
        playerControlAnim.SetFloat("WalkSpeed", Mathf.Abs(x * walkSpeed));
        playerControlAnim.SetFloat("ZWalkSpeed", Mathf.Abs(z * walkSpeed));
        playerControlAnim.SetBool("IsCrouching", isCrouch);
        // Jump
        if (isGround)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rigidBody.velocity += new Vector3(x * walkSpeed, 10f * jumpForce, 0f);
                playerControlAnim.SetBool("IsJumping", true);
                startJump = true;
            }
            else if (startLand) OnLand.Invoke();
            if (startJump)
            {
                jumpSound.Play();
                startJump = false;
                startLand = true;
            }
        }
        else
        {
            walkSound.Stop();
            runSound.Stop();
            crouchSound.Stop();
            rigidBody.velocity += new Vector3(x * walkSpeed, -jumpForce, 0f);
        }
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