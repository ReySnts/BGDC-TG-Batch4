using UnityEngine;
using UnityEngine.Playables;
public abstract class VCamVerticalDown : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public VCamVerticalUp vCamVerticalUp = null;
    [Header("Values")]
    public Vector2 yTrigger = new Vector2(0f, 0f);
    public bool isTimelinePlayed = false;
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    public Vector2 yClamp = new Vector2(0f, 0f);
    public abstract void SetVCam(float yTriggerX);
    void Awake()
    {
        lightCursor = FindObjectOfType<LightCursor>();
        vCamVerticalUp = FindObjectOfType<VCamVerticalUp>();
    }
    void Update()
    {
        lightCursor.downYClamp = yClamp.x;
        lightCursor.upYClamp = yClamp.y;
        if (!isTimelinePlayed)
        {
            playableDirector.time = 2f;
            vCamVerticalUp.enabled = false;
            if (player.position.x < yTrigger.x || player.position.x >= yTrigger.y)
            {
                playableDirector.initialTime = 2.5f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 4f)
        {
            playableDirector.Stop();
            vCamVerticalUp.enabled = true;
            isTimelinePlayed = false;
            enabled = false;
        }
    }
}