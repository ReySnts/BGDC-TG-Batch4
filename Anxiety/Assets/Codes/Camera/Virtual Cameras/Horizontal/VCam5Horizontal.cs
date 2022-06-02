using UnityEngine;
using UnityEngine.Playables;
public class VCam5Horizontal : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public Cam2DToVCam5 cam2DToVCam5 = null;
    [Header("Value Arrays")]
    public float xTrigger = 0f;
    public bool isTimelinePlayed = false;
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    public Vector2 yClamp = new Vector2(16f, 25f);
    void Start()
    {
        cam2DToVCam5.player = player;
        cam2DToVCam5.playableDirector = playableDirector;
        cam2DToVCam5.lightCursor = lightCursor = FindObjectOfType<LightCursor>();
        cam2DToVCam5.xTrigger = xTrigger = -82f;
        cam2DToVCam5.isTimelinePlayed = isTimelinePlayed = false;
        cam2DToVCam5.enabled = false;
    }
    void Update()
    {
        lightCursor.downYClamp = yClamp.x;
        lightCursor.upYClamp = yClamp.y;
        if (!isTimelinePlayed)
        {
            cam2DToVCam5.enabled = false;
            if (player.position.x >= xTrigger)
            {
                playableDirector.initialTime = 0.2f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 2f)
        {
            playableDirector.time = 2f;
            cam2DToVCam5.enabled = true;
            isTimelinePlayed = false;
            enabled = false;
        }
    }
}