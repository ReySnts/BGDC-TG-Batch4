using UnityEngine;
using UnityEngine.Playables;
public class Cam2DToVCam5 : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public VCam5Horizontal vCam5Horizontal = null;
    public LightCursor lightCursor = null;
    [Header("Values")]
    public float xTrigger = 0f;
    public bool isTimelinePlayed = false;
    Vector2 yClamp = new Vector2(9.8f, 13.45f);
    void Update()
    {
        lightCursor.downYClamp = player.position.y - 10.25814f + yClamp.x;
        lightCursor.upYClamp = player.position.y + yClamp.y - 10.25814f;
        if (!isTimelinePlayed)
        {
            playableDirector.time = 2f;
            vCam5Horizontal.enabled = false;
            if (player.position.x < xTrigger)
            {
                playableDirector.initialTime = 2.5f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 4f)
        {
            playableDirector.Stop();
            vCam5Horizontal.enabled = true;
            isTimelinePlayed = false;
            enabled = false;
        }
    }
}