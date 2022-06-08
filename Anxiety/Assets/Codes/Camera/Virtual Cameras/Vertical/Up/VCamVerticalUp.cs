using UnityEngine;
using UnityEngine.Playables;
public abstract class VCamVerticalUp : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public VCamVerticalDown vCamVerticalDown = null;
    [Header("Value Arrays")]
    public float yTrigger = 0f;
    public bool isTimelinePlayed = false;
    public abstract void SetVCam(float y);
    void Start()
    {
        isTimelinePlayed = false;
        vCamVerticalDown.enabled = false;
    }
    void Update()
    {
        if (!isTimelinePlayed)
        {
            vCamVerticalDown.enabled = false;
            if (player.position.y >= yTrigger)
            {
                playableDirector.initialTime = 0.2f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 2f)
        {
            playableDirector.time = 2f;
            vCamVerticalDown.enabled = true;
            isTimelinePlayed = false;
            enabled = false;
        }
    }
}