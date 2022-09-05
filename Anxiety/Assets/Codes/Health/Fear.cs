using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class Fear : MonoBehaviour
{
<<<<<<< HEAD
    bool isDie = false;
    bool isDieAfterJump = false;
    bool isDieAfterEmptyMeter = false;
=======
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
>>>>>>> programming
    [Header("Slider")]
    public Slider fearMeter = null;
    public float maxFearValue = 100f;
    readonly float minFearValue = 0f;
    [Header("Dark Damages")]
<<<<<<< HEAD
    [SerializeField] float darkDamage = 5f;
    [SerializeField] float darkDamageCooldown = 1f;
    float lastTimeDamagedByDark = 0f;
=======
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
>>>>>>> programming
    [Header("Die Effects")]
    public Animator playerControlAnim = null;
    public Animator backgroundControl = null;
    public PlayableDirector dieSoundTimeline = null;
<<<<<<< HEAD
    PlayerMovement playerMovement = null;
    [Header("Checkpoint")]
    public GameObject player = null;
    public Death[] dieTraps = new Death[3];
    CheckPoint checkPoint = null;
=======
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
>>>>>>> programming
    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        checkPoint = FindObjectOfType<CheckPoint>();
    }
    void ResetQTE()
    {
<<<<<<< HEAD
        fearMeter.value = fearMeter.minValue = minFearValue;
        fearMeter.maxValue = maxFearValue;
    }
    void Respawn()
    {
        StopAllCoroutines();
        playerMovement.enabled = true;
        foreach (Death death in dieTraps) death.isTriggered = false;
        isDie = checkPoint.isTriggered = false;
    }
=======
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
>>>>>>> programming
    IEnumerator HoldRespawn()
    {
        yield return new WaitForSeconds(2f);
        backgroundControl.SetBool("IsFadeOut", false);
        #region Respawn
        isDie = hasDied = false;
        ReleasePlayer();
        foreach (Death death in dieTraps) if (death != null) death.isCollided = death.isTriggered = false;
        #endregion
    }
<<<<<<< HEAD
    void SetRespawn()
    {
        fearMeter.value = minFearValue;
        player.transform.position = checkPoint.respawnPoint;
        if (isDieAfterEmptyMeter)
=======
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
>>>>>>> programming
        {
            isDieAfterEmptyMeter = false;
            playerControlAnim.SetBool("IsDieAfterEmptyMeter", isDieAfterEmptyMeter);
        }
        else if (isDieAfterJump)
        {
            isDieAfterJump = false;
            playerControlAnim.SetBool("IsDieAfterJump", isDieAfterJump);
        }
<<<<<<< HEAD
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        SetRespawn();
=======
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
>>>>>>> programming
        backgroundControl.SetBool("IsFadeIn", false);
        backgroundControl.SetBool("IsFadeOut", true);
        StartCoroutine(HoldRespawn());
    }
    public IEnumerator FadeIn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        backgroundControl.SetBool("IsFadeIn", true);
        StartCoroutine(FadeOut());
<<<<<<< HEAD
    }
    void SetDie()
    {
        playerMovement.enabled = false;
        isDie = true;
    }
    void DieAfterEmptyMeter()
    {
        if (fearMeter.value == maxFearValue)
        {
            StartCoroutine(FadeIn(3f));
            isDieAfterEmptyMeter = true;
            playerControlAnim.SetBool("IsDieAfterEmptyMeter", isDieAfterEmptyMeter);
            dieSoundTimeline.Play();
            SetDie();
        }
    }
    public void DieAfterJump()
    {
        fearMeter.value = maxFearValue;
        isDieAfterJump = true;
        playerControlAnim.SetBool("IsDieAfterJump", isDieAfterJump);
        SetDie();
    }
    void EffectByLight()
    {
        if (Time.time - lastTimeDamagedByDark < darkDamageCooldown) return;
        lastTimeDamagedByDark = Time.time;
        if (GetLight.outOfLight) fearMeter.value += darkDamage;
        else if (fearMeter.value < maxFearValue) fearMeter.value -= darkDamage;
    }
    void Update()
    {
        EffectByLight();
        if (!isDie) DieAfterEmptyMeter();
=======
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
>>>>>>> programming
    }
}