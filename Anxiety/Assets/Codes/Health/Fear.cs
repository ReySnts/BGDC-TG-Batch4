using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class Fear : MonoBehaviour
{
    bool isDie = false;
    bool isDieAfterJump = false;
    bool isDieAfterEmptyMeter = false;
    [Header("Slider")]
    public Slider fearMeter = null;
    public float maxFearValue = 100f;
    [SerializeField] float minFearValue = 0f;
    [Header("Dark Damages")]
    [SerializeField] float darkDamage = 5f;
    [SerializeField] float darkDamageCooldown = 1f;
    float lastTimeDamagedByDark = 0f;
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
        fearMeter.value = fearMeter.minValue = minFearValue;
        fearMeter.maxValue = maxFearValue;
    }
    void Respawn()
    {
        StopAllCoroutines();
        playerMovement.enabled = true;
        foreach (Death death in dieTraps) death.isTriggered = false;
        isDie = false;
    }
    IEnumerator HoldRespawn()
    {
        yield return new WaitForSeconds(2f);
        backgroundControl.SetBool("IsFadeOut", false);
        Respawn();
    }
    void SetRespawn()
    {
        fearMeter.value = minFearValue;
        player.transform.position = FindObjectOfType<CheckPoint>().respawnPoint;
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
        playerControlAnim.SetBool("IsJumping", false);
        playerControlAnim.SetBool("IsCrouching", false);
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
        if (Time.time - lastTimeDamagedByDark < darkDamageCooldown) return;
        lastTimeDamagedByDark = Time.time;
        if (GetLight.outOfLight) fearMeter.value += darkDamage;
        else if (fearMeter.value < maxFearValue) fearMeter.value -= darkDamage;
    }
    void Update()
    {
        EffectByLight();
        if (!isDie) DieAfterEmptyMeter();
    }
}