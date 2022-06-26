using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class Fear : MonoBehaviour
{
    bool isDie = false;
    [Header("Slider")]
    public Slider fearMeter = null;
    public float maxFearValue = 100f;
    [SerializeField] float minFearValue = 0f;
    [Header("Dark Damages")]
    [SerializeField] float darkDamage = 5f;
    [SerializeField] float darkDamageCooldown = 1f;
    float lastTimeDamagedByDark = 0f;
    [Header("References")]
    public Animator playerControlAnim = null;
    public PlayableDirector dieSoundTimeline = null;
    void Start()
    {
        fearMeter.value = fearMeter.minValue = minFearValue;
        fearMeter.maxValue = maxFearValue;
    }
    public IEnumerator HoldRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManagement.Restart();
    }
    void SetDie()
    {
        FindObjectOfType<PlayerMovement>().enabled = false;
        isDie = true;
    }
    void DieAfterEmptyMeter()
    {
        if (fearMeter.value == maxFearValue)
        {
            StartCoroutine(HoldRestart(3f));
            playerControlAnim.SetBool("IsDieAfterEmptyMeter", true);
            dieSoundTimeline.Play();
            SetDie();
        }
    }
    public void DieAfterJump()
    {
        fearMeter.value = maxFearValue;
        playerControlAnim.SetBool("IsDieAfterJump", true);
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