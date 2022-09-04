using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class Fear : MonoBehaviour
{
    public static Fear objInstance = null;
    #region Booleans
    [Header("Booleans")]
    public static bool willShowHealth = false;
    public bool isDieAfterShock = false;
    public bool isDie = false;
    bool isDieAfterJump = false;
    bool isDieAfterEmptyMeter = false;
    bool isDieAfterShockBecauseSpike = false;
    #endregion
    [Header("Slider")]
    public Slider fearMeter = null;
    public float maxFearValue = 100f;
    readonly float minFearValue = 0f;
    [Header("Dark Damages")]
    [SerializeField] readonly float darkDamage = 5f;
    [SerializeField] readonly float darkDamageCooldown = 0.5f;
    #region QTE
    [Header("QTE")]
    public GameObject qTE = null;
    public Image qTEFill = null;
    public bool hasDoneQTE = false;
    bool hasStartedQTE = false;
    [Range(0f, 1f)] [SerializeField] readonly float qTEFillAdd = 0.125f;
    [SerializeField] readonly float qTEFillDiffTime = 0.1f;
    [Range(-1f, 0f)] [SerializeField] readonly float qTEFillDiff = -0.01f;
    [SerializeField] readonly float handQTEDmg = 0.5f;
    [SerializeField] readonly float handQTEDmgCooldown = 0.1f;
    float lastTimeDamaged = 0f;
    float currentTime = 0f;
    #endregion
    #region Die Effects
    [Header("Die Effects")]
    public Animator playerControlAnim = null;
    public Animator backgroundControl = null;
    public PlayableDirector dieSoundTimeline = null;
    public AudioSource shockSound = null;
    public GameObject playerMoveSounds = null;
    readonly float fadeOutTime = 2f;
    #endregion
    [Header("Checkpoint")]
    public GameObject player = null;
    public Death[] dieTraps = new Death[40];
    public bool hasDied = false; // for lightCursorClamp.
    int currCheckpointIdx = 0;
    int currTotalCheckpoint = 0;
    Vector3 closestRespawnPoint = Vector3.zero;
    void Awake()
    {
        if (
            objInstance == null && 
            (
                SceneManagement.GetCurrentScene() == 2 ||
                SceneManagement.GetCurrentScene() == 3
            )
        ) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void ResetQTE()
    {
        try
        {
            qTEFill.fillAmount = minFearValue;
            qTE.SetActive(false);
        }
        catch
        {
            qTEFill = null;
            qTE = null;
        }
    }
    void Start()
    {
        #region Setup Fear Meter
        fearMeter.value = fearMeter.minValue = minFearValue;
        fearMeter.maxValue = maxFearValue;
        fearMeter.interactable = false;
        #endregion
        ResetQTE();
    }
    void ReleasePlayer()
    {
        ResetQTE();
        PlayerMovement.objInstance.rigidBody.isKinematic = false;
        PlayerMovement.objInstance.enabled = PlayerMovement.objInstance.rigidBody.useGravity = true;
        playerMoveSounds.SetActive(true);
    }
    public IEnumerator ResetHand(float resetHandTime)
    {
        yield return new WaitForSeconds(resetHandTime);
        HandWall.turnOnMagnet = HandWall.hasGrabbedPlayer = hasDoneQTE = hasStartedQTE = false;
    }
    void SetDie()
    {
        fearMeter.value = maxFearValue;
        PlayerMovement.objInstance.enabled = false;
        playerMoveSounds.SetActive(false);
        isDie = true;
    }
    public void DieAfterShockBecauseSpike()
    {
        isDieAfterShockBecauseSpike = true;
        playerControlAnim.SetBool("IsDieAfterShock", isDieAfterShockBecauseSpike);
        SetDie();
    }
    public void DieAfterJump()
    {
        isDieAfterJump = true;
        playerControlAnim.SetBool("IsDieAfterJump", isDieAfterJump);
        SetDie();
    }
    IEnumerator HoldRespawn()
    {
        yield return new WaitForSeconds(fadeOutTime);
        backgroundControl.SetBool("IsFadeOut", false);
        #region Respawn
        isDie = hasDied = false;
        ReleasePlayer();
        foreach (Death death in dieTraps) if (death != null) death.isCollided = death.isTriggered = false;
        #endregion
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeOutTime);
        #region Set Respawn
        fearMeter.value = minFearValue;
        #region Reset Player Position
        currTotalCheckpoint = CheckPoint.objInstance.respawnPoints.Count;
        closestRespawnPoint = CheckPoint.objInstance.respawnPoints[0];
        for (currCheckpointIdx = 1; currCheckpointIdx < currTotalCheckpoint; currCheckpointIdx++)
            if (closestRespawnPoint.x < CheckPoint.objInstance.respawnPoints[currCheckpointIdx].x)
                closestRespawnPoint = CheckPoint.objInstance.respawnPoints[currCheckpointIdx];
        player.transform.position = closestRespawnPoint;
        #endregion
        #region Check Cause Of Death
        if (isDieAfterShock)
        {
            isDieAfterShock = HandWall.startPulling = HandWall.hasPulled = false;
            playerControlAnim.SetBool("IsDieAfterShock", isDieAfterShock);
        }
        else if (isDieAfterJump)
        {
            isDieAfterJump = false;
            playerControlAnim.SetBool("IsDieAfterJump", isDieAfterJump);
        }
        else if (isDieAfterShockBecauseSpike)
        {
            isDieAfterShockBecauseSpike = false;
            playerControlAnim.SetBool("IsDieAfterShock", isDieAfterShockBecauseSpike);
        }
        else if (isDieAfterEmptyMeter)
        {
            isDieAfterEmptyMeter = false;
            playerControlAnim.SetBool("IsDieAfterEmptyMeter", isDieAfterEmptyMeter);
        }
        #endregion
        #region Reset Animations For Respawn
        playerControlAnim.SetFloat("WalkSpeed", 0f);
        playerControlAnim.SetFloat("ZWalkSpeed", 0f);
        playerControlAnim.SetBool("IsJumping", false);
        playerControlAnim.SetBool("IsCrouching", false);
        #endregion
        #region Reset HandWalls Position
        try
        {
            HandWallsMovement.objInstance.transform.position = HandWallsMovement.objInstance.initialPosition;
        }
        catch
        {
            HandWallsMovement.objInstance = null;
        }
        #endregion
        hasDied = true;
        #endregion
        backgroundControl.SetBool("IsFadeIn", false);
        backgroundControl.SetBool("IsFadeOut", true);
        StartCoroutine(HoldRespawn());
    }
    IEnumerator HandReturn()
    {
        yield return new WaitForSeconds(1f);
        HandWall.hasPulled = true;
    }
    public IEnumerator FadeIn(float fadeInTime)
    {
        yield return new WaitForSeconds(fadeInTime);
        backgroundControl.SetBool("IsFadeIn", true);
        StartCoroutine(FadeOut());
        if (HandWallsMovement.objInstance != null) StartCoroutine(HandReturn());
    }
    void Update()
    {
        if (!HandWall.turnOnMagnet)
        {
            #region Effect By Light
            if (!isDie && fearMeter.value == maxFearValue)
            {
                #region Die After Empty Meter
                StartCoroutine(FadeIn(3f));
                isDieAfterEmptyMeter = true;
                playerControlAnim.SetBool("IsDieAfterEmptyMeter", isDieAfterEmptyMeter);
                dieSoundTimeline.Play();
                SetDie();
                #endregion
            }
            else
            {
                if (Time.time - lastTimeDamaged < darkDamageCooldown) return;
                lastTimeDamaged = Time.time;
                if (GetLight.outOfLight) fearMeter.value += darkDamage;
                else if (fearMeter.value < maxFearValue) fearMeter.value -= darkDamage;
            }
            #endregion
        }
        else if (HandWall.hasGrabbedPlayer && !hasDoneQTE)
        {
            #region Start QTE
            if (!hasStartedQTE)
            {
                qTE.SetActive(true);
                hasStartedQTE = true;
            }
            if (fearMeter.value == maxFearValue)
            {
                #region Die After Shock
                isDieAfterShock = hasDoneQTE = true;
                playerControlAnim.SetBool("IsDieAfterShock", isDieAfterShock);
                shockSound.Play();
                #endregion
            }
            else if (qTEFill.fillAmount == 1f)
            {
                ReleasePlayer();
                hasDoneQTE = true;
                StartCoroutine(ResetHand(1f));
            }
            else if (Input.GetKeyDown(KeyCode.Return)) qTEFill.fillAmount += qTEFillAdd;
            else if (currentTime >= qTEFillDiffTime)
            {
                qTEFill.fillAmount += qTEFillDiff;
                currentTime = 0f;
            }
            else
            {
                currentTime += Time.deltaTime;
                #region Effect By Hand QTE
                if (Time.time - lastTimeDamaged < handQTEDmgCooldown) return;
                lastTimeDamaged = Time.time;
                fearMeter.value += handQTEDmg;
                #endregion
            }
            #endregion
        }
    }
}