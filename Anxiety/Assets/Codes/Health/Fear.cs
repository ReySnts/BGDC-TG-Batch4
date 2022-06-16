using UnityEngine;
using UnityEngine.UI;
public class Fear : MonoBehaviour
{
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