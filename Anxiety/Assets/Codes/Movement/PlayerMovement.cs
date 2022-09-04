using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement objInstance = null;
    [Header("Body")]
    //public Transform shadow = null;
    public Rigidbody rigidBody = null;
    public SpriteRenderer spriteRenderer = null;
    public GameObject head = null;
    BoxCollider boxCol = null;
    CapsuleCollider capsCol = null;
    float x = 0f;
    float z = 0f;
    [Header("Move Speed")]
    [Range(0f, 20f)] [SerializeField] float walkSpeed = 4f;
    [Range(0f, 20f)] public float jumpForce = 2.5f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [Header("Animation")]
    public Animator playerControlAnim = null;
    public bool leftTurn = false;
    public bool rightTurn = false;
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
    void Awake()
    {
        if (
            objInstance == null && 
            SceneManagement.GetCurrentScene() != 0 && 
            SceneManagement.GetCurrentScene() != 5
        ) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void SetCollider()
    {
        if (isCrouch)
        {
            capsCol.enabled = false;
            boxCol.enabled = true;
        }
        else
        {
            //shadow.localScale = Vector3.one * 0.5f;
            boxCol.enabled = false;
            capsCol.enabled = true;
        }
    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerControlAnim = GetComponentInChildren<Animator>();
        boxCol = GetComponent<BoxCollider>();
        capsCol = GetComponent<CapsuleCollider>();
        spriteRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        SetCollider();
    }
    void Walk()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        if (!isPressed && (x != 0f || z != 0f))
        {
            try
            {
                walkSound.Play();
            }
            catch { }
            isPressed = true;
        }
        else if (x == 0f && z == 0f)
        {
            walkSound.Stop();
            runSound.Stop();
            isPressed = false;
        }
    }
    void TurnLeftOrRight()
    {
        if (Input.GetKeyDown(KeyCode.A) && x - tempX < 0f && !leftTurn)
        {
            transform.Rotate(Vector3.up * 180f);
            leftTurn = firstRightWalk = true;
            rightTurn = false;
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
    }
    void SetCrouch()
    {
        //shadow.localScale = new Vector3(1f, 0.5f, 1f);
        rigidBody.velocity = new Vector3(x * walkSpeed * 0.5f, 0f, z * walkSpeed * 0.5f);
        if (!isCrouch)
        {
            try
            {
                crouchSound.Play();
            }
            catch { }
            isCrouch = true;
        }
        runSound.Stop();
    }
    void SetRun()
    {
        rigidBody.velocity = new Vector3(x * walkSpeed * 2f, 0f, z * walkSpeed * 2f);
        if (!isRun && (x != 0f || z != 0f))
        {
            try
            {
                runSound.Play();
            }
            catch { }
            isRun = true;
        }
        walkSound.Stop();
    }
    void StopRun()
    {
        runSound.Stop();
        try
        {
            walkSound.Play();
        }
        catch { }
        isRun = false;
    }
    void StopCrouch()
    {
        try
        {
            crouchSound.Play();
        }
        catch { }
        isCrouch = false;
    }
    void SetNormal()
    {
        rigidBody.velocity = new Vector3(x * walkSpeed, 0f, z * walkSpeed);
        if (isRun) StopRun();
        if (isCrouch) StopCrouch();
    }
    void SetMoveMode()
    {
        if (Input.GetKey(KeyCode.LeftControl)) SetCrouch();
        else if (Input.GetKey(KeyCode.LeftShift)) SetRun();
        else SetNormal();
    }
    void AdjustTurn()
    {
        // Turn Left or Right
        tempX = x;
        tempZ = z;
    }
    void SetAnimator()
    {
        playerControlAnim.SetFloat("WalkSpeed", Mathf.Abs(x * walkSpeed));
        playerControlAnim.SetFloat("ZWalkSpeed", Mathf.Abs(z * walkSpeed));
        playerControlAnim.SetBool("IsCrouching", isCrouch);
    }
    void StartToJump()
    {
        velocity.y = jumpForce;
        if (
            !Physics.Raycast(
                head.transform.position,
                head.transform.TransformDirection(Vector3.up),
                0.2f,
                groundMask
            )
        ) rigidBody.velocity += new Vector3(x * walkSpeed, velocity.y, 0f);
        playerControlAnim.SetBool("IsJumping", true);
        startJump = true;
    }
    void StartLandAfterJump()
    {
        try
        {
            jumpSound.Play();
        }
        catch { }
        startJump = false;
        startLand = true;
    }
    void SetGravity()
    {
        if (isGround) velocity.y = -2f;
        if (
            Physics.Raycast(
                head.transform.position,
                head.transform.TransformDirection(Vector3.up),
                0.2f,
                groundMask
            )
        ) velocity.y = -0.5f;
        velocity.y += (gravity * Time.deltaTime);
        rigidBody.velocity += new Vector3(x * walkSpeed, velocity.y, 0f);
    }
    void JumpingOnAir()
    {
        walkSound.Stop();
        runSound.Stop();
        crouchSound.Stop();
        SetGravity();
    }
    void Jump()
    {
        if (isGround)
        {
            if (Input.GetKey(KeyCode.Space)) StartToJump();
            else if (startLand) OnLand.Invoke();
            if (startJump) StartLandAfterJump();
        }
        else JumpingOnAir();
    }
    public void Landing()
    {
        try
        {
            landSound.Play();
        }
        catch { }
        playerControlAnim.SetBool("IsJumping", false);
        startLand = false;
    }
    void Update()
    {
        Walk();
        TurnLeftOrRight();
        SetMoveMode();
        SetCollider();
        AdjustTurn();
        SetAnimator();
        Jump();
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
        )
        {
            isGround = true;
            velocity.y = -2f;
        }
        else isGround = false;
    }
}