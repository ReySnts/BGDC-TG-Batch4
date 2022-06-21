using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Fear : MonoBehaviour
{
    public Animator playerControlAnim = null;
    [Header("Slider")]
    public Slider fearMeter = null;
    [SerializeField] float minFearValue = 0f;
    [SerializeField] float maxFearValue = 100f;
    [Header("Dark Damages")]
    [SerializeField] float darkDamage = 5f;
    [SerializeField] float darkDamageCooldown = 1f;
    float lastTimeDamagedByDark = 0f;
    void Start()
    {
        fearMeter.value = fearMeter.minValue = minFearValue;
        fearMeter.maxValue = maxFearValue;
    }
    IEnumerator GenerateTime()
    {
        yield return new WaitForSeconds(16f);
    }
    public void Die()
    {
        playerControlAnim.SetBool("IsDie", true);
        StartCoroutine(GenerateTime());
        SceneManagement.Restart();
    }
    void EffectByLight()
    {
        if (Time.time - lastTimeDamagedByDark < darkDamageCooldown) return;
        lastTimeDamagedByDark = Time.time;
        if (GetLight.outOfLight) fearMeter.value += darkDamage;
        else fearMeter.value -= darkDamage;
    }
    void Update()
    {
        EffectByLight();
    }
}