using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class Fear : MonoBehaviour
{
    public static bool willShowHealth = false;
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
    public GameObject qTE = null;
    public Image qTEFill = null;
    [SerializeField] float handQTEDmg = 0.5f;
    [SerializeField] float handQTEDmgCooldown = 0.1f;
    bool isQTEFilled = false;
    float lastTimeDamaged = 0f;
    float currentTime = 0f;
    [Header("Die Effects")]
    public Animator playerControlAnim = null;
    public Animator backgroundControl = null;
    public PlayableDirector dieSoundTimeline = null;
    public PlayerMovement playerMovement = null;
    [Header("Checkpoint")]
    public GameObject player = null;
    public Death[] dieTraps = new Death[3];
    public static bool hasDied = false;
    void Start()
    {
        qTE.SetActive(false);
        qTEFill.fillAmount = fearMeter.value = fearMeter.minValue = minFearValue;
        fearMeter.maxValue = maxFearValue;
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
        yield return new WaitForSeconds(2f);
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
    void CheckCauseOfDeath()
    {
        if (isDieAfterEmptyMeter)
        {
            isDieAfterEmptyMeter = false;
            playerControlAnim.SetBool("IsDieAfterEmptyMeter", isDieAfterEmptyMeter);
        }
        else if (isDieAfterJump)
        {
            isDieAfterJump = false;
            playerControlAnim.SetBool("IsDieAfterJump", isDieAfterJump);
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
        yield return new WaitForSeconds(2f);
        SetRespawn();
        backgroundControl.SetBool("IsFadeIn", false);
        backgroundControl.SetBool("IsFadeOut", true);
        StartCoroutine(HoldRespawn());
    }
    public IEnumerator FadeIn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        backgroundControl.SetBool("IsFadeIn", true);
        StartCoroutine(FadeOut());
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
        if (Time.time - lastTimeDamaged < darkDamageCooldown) return;
        lastTimeDamaged = Time.time;
        if (!HandWall.turnOnMagnet)
        {
            if (GetLight.outOfLight) fearMeter.value += darkDamage;
            else if (fearMeter.value < maxFearValue) fearMeter.value -= darkDamage;
        }
    }
    IEnumerator HoldGrab()
    {
        yield return new WaitForSeconds(2f);
        HandWall.turnOnMagnet = HandWall.hasGrabbedPlayer = false;
        qTEFill.fillAmount = minFearValue;
    }
    void FillHandQTE()
    {
        playerMovement.rigidBody.isKinematic = false;
        playerMovement.enabled = playerMovement.rigidBody.useGravity = true;
        StartCoroutine(HoldGrab());
        isQTEFilled = true;
    }
    void FillingHandQTE()
    {
        if (!isQTEFilled)
        {
            if (Input.GetKeyDown(KeyCode.Return)) qTEFill.fillAmount += 0.125f;
            if (qTEFill.fillAmount == 1f) FillHandQTE();
            else if (currentTime >= 1f)
            {
                qTEFill.fillAmount -= 0.01f;
                currentTime = 0f;
            }
            else currentTime += Time.deltaTime;
        }
    }
    void EffectByHandQTE()
    {
        if (Time.time - lastTimeDamaged < handQTEDmgCooldown) return;
        lastTimeDamaged = Time.time;
        fearMeter.value += handQTEDmg;
    }
    void Update()
    {
        if (HandWall.hasGrabbedPlayer)
        {
            qTE.SetActive(true); //Hehe1
            EffectByHandQTE();
            FillingHandQTE();
        }
        else
        {
            qTE.SetActive(false); //Hehe2
            EffectByLight();
        }
        if (!isDie) DieAfterEmptyMeter();
    }
}