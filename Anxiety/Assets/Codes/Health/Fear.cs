using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class Fear : MonoBehaviour
{
    public static Fear objInstance = null;
    public static bool willShowHealth = false;
    public bool isDieAfterShock = false;
    bool isDie = false;
    bool isDieAfterJump = false;
    bool isDieAfterEmptyMeter = false;
    [Header("Slider")]
    public Slider fearMeter = null;
    public float maxFearValue = 100f;
    [SerializeField] float minFearValue = 0f;
    [Header("Dark Damages")]
    [SerializeField] float darkDamage = 5f;
    [SerializeField] float darkDamageCooldown = 0.5f;
    [Header("QTE")]
    public GameObject qTEImage = null;
    public Image qTEFill = null;
    public bool hasDoneQTE = false;
    [Range(0f, 1f)] [SerializeField] float qTEFillAdd = 0.125f;
    [SerializeField] float qTEFillDiffTime = 0.1f;
    [Range(-1f, 0f)] [SerializeField] float qTEFillDiff = -0.01f;
    [SerializeField] float handQTEDmg = 0.5f;
    [SerializeField] float handQTEDmgCooldown = 0.1f;
    float lastTimeDamaged = 0f;
    float currentTime = 0f;
    [Header("Die Effects")]
    public Animator playerControlAnim = null;
    public Animator backgroundControl = null;
    public PlayableDirector dieSoundTimeline = null;
    public AudioSource shockSound = null;
    public PlayerMovement playerMovement = null;
    float fadeOutTime = 2f;
    [Header("Checkpoint")]
    public GameObject player = null;
    public Death[] dieTraps = new Death[3];
    public bool hasDied = false;
    void Awake()
    {
        if (objInstance == null && SceneManagement.GetCurrentScene() != 1)
        {
            objInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        try
        {
            qTEFill.fillAmount = fearMeter.value = fearMeter.minValue = minFearValue;
            fearMeter.maxValue = maxFearValue;
            qTEImage.SetActive(false);
        }
        catch
        {
            qTEImage = null;
            qTEFill = null;
        }
    }
    void Respawn()
    {
        StopAllCoroutines();
        playerMovement.enabled = true;
        foreach (Death death in dieTraps) if (death != null) death.isTriggered = false;
        isDie = false;
    }
    IEnumerator HoldRespawn()
    {
        yield return new WaitForSeconds(fadeOutTime);
        backgroundControl.SetBool("IsFadeOut", false);
        Respawn();
    }
    void ResetAnimForRespawn()
    {
        playerControlAnim.SetFloat("WalkSpeed", 0f);
        playerControlAnim.SetFloat("ZWalkSpeed", 0f);
        playerControlAnim.SetBool("IsJumping", false);
        playerControlAnim.SetBool("IsCrouching", false);
    }
    void ReleasePlayer()
    {
        playerMovement.rigidBody.isKinematic = false;
        playerMovement.enabled = playerMovement.rigidBody.useGravity = true;
        qTEFill.fillAmount = 0f;
        qTEImage.SetActive(false);
    }
    void CheckCauseOfDeath()
    {
        if (isDieAfterShock)
        {
            isDieAfterShock = HandWall.startPulling = HandWall.hasPulled = false;
            playerControlAnim.SetBool("IsDieAfterShock", isDieAfterShock);
            ReleasePlayer();
        }
        else if (isDieAfterJump)
        {
            isDieAfterJump = false;
            playerControlAnim.SetBool("IsDieAfterJump", isDieAfterJump);
        }
        else if (isDieAfterEmptyMeter)
        {
            isDieAfterEmptyMeter = false;
            playerControlAnim.SetBool("IsDieAfterEmptyMeter", isDieAfterEmptyMeter);
        }
    }
    void SetRespawn()
    {
        fearMeter.value = minFearValue;
        player.transform.position = FindObjectOfType<CheckPoint>().respawnPoint;
        CheckCauseOfDeath();
        ResetAnimForRespawn();
        hasDied = true;
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeOutTime);
        SetRespawn();
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
        StartCoroutine(HandReturn());
        StartCoroutine(FadeOut());
    }
    void SetDie()
    {
        playerMovement.enabled = false;
        isDie = true;
    }
    public void DieAfterJump()
    {
        fearMeter.value = maxFearValue;
        isDieAfterJump = true;
        playerControlAnim.SetBool("IsDieAfterJump", isDieAfterJump);
        SetDie();
    }
    void DieAfterEmptyMeter()
    {
        StartCoroutine(FadeIn(3f));
        isDieAfterEmptyMeter = true;
        playerControlAnim.SetBool("IsDieAfterEmptyMeter", isDieAfterEmptyMeter);
        dieSoundTimeline.Play();
        SetDie();
    }
    void EffectByHandQTE()
    {
        if (Time.time - lastTimeDamaged < handQTEDmgCooldown) return;
        lastTimeDamaged = Time.time;
        fearMeter.value += handQTEDmg;
    }
    public IEnumerator ResetHand(float resetHandTime)
    {
        yield return new WaitForSeconds(resetHandTime);
        HandWall.turnOnMagnet = HandWall.hasGrabbedPlayer = hasDoneQTE = false;
    }
    void DieAfterShock()
    {
        isDieAfterShock = hasDoneQTE = true;
        playerControlAnim.SetBool("IsDieAfterShock", isDieAfterShock);
        shockSound.Play();
    }
    void StartQTE()
    {
        qTEImage.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Return)) qTEFill.fillAmount += qTEFillAdd;
        if (fearMeter.value == maxFearValue) DieAfterShock();
        else if (qTEFill.fillAmount == 1f)
        {
            ReleasePlayer();
            hasDoneQTE = true;
            StartCoroutine(ResetHand(1f));
        }
        else if (currentTime >= qTEFillDiffTime)
        {
            qTEFill.fillAmount += qTEFillDiff;
            currentTime = 0f;
        }
        else
        {
            currentTime += Time.deltaTime;
            EffectByHandQTE();
        }
    }
    void EffectByLight()
    {
        if (!isDie && fearMeter.value == maxFearValue) DieAfterEmptyMeter();
        else
        {
            if (Time.time - lastTimeDamaged < darkDamageCooldown) return;
            lastTimeDamaged = Time.time;
            if (GetLight.outOfLight) fearMeter.value += darkDamage;
            else if (fearMeter.value < maxFearValue) fearMeter.value -= darkDamage;
        }
    }
    void Update()
    {
        if (!HandWall.turnOnMagnet) EffectByLight();
        else if (HandWall.hasGrabbedPlayer && !hasDoneQTE) StartQTE();
    }
}