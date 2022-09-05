using UnityEngine;
using UnityEngine.Playables;
public abstract class VCamVertical : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    [Header("Values")]
    public Vector2 yTrigger = Vector2.zero;
    public bool isTimelinePlayed = false;
    protected float yTriggerX = 0f;
    protected float yTriggerY = 0f;
    protected abstract Vector2 SetVCam();
    protected void OnEnable()
    {
        yTrigger = SetVCam();
    }
}