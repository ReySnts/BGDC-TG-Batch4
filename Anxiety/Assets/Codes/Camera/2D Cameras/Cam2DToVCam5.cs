using UnityEngine;
using UnityEngine.Playables;
public class Cam2DToVCam5 : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public VCam5Horizontal vCam5Horizontal = null;
    [Header("Values")]
    public float xTrigger = 0f;
    public bool isTimelinePlayed = false;
    void Update()
    {
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