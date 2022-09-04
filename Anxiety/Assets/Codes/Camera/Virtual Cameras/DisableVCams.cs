using UnityEngine;
public class DisableVCams : MonoBehaviour
{
    GameObject spikeBlockade = null;
    readonly string colliderName = "Player";
    bool isTriggered = false;
    void Start()
    {
        (spikeBlockade = GameObject.Find("Border (9)")).SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.name == colliderName)
        {
            isTriggered = true;
            spikeBlockade.SetActive(true);
            GameObject.Find("VirtualCams").SetActive(false);
        }
    }
}