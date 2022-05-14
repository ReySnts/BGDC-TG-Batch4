using UnityEngine;
using UnityEngine.Playables;
public class Cam2D : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public VCam1 vCam1 = null;
    float xTrigger = -156f;
    [SerializeField] bool isTimelinePlayed = false;
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    [SerializeField] float leftXClamp = -164f;
    [SerializeField] float rightXClamp = -50f;
    [SerializeField] float downYClamp = 9.8f;
    [SerializeField] float upYClamp = 13.45f;
    void Start()
    {
        vCam1 = FindObjectOfType<VCam1>();
        vCam1.enabled = false;
        lightCursor = FindObjectOfType<LightCursor>();
        playableDirector = FindObjectOfType<PlayableDirector>();
        playableDirector.Play();
    }
    void Update()
    {
        lightCursor.leftXClamp = this.leftXClamp;
        lightCursor.rightXClamp = this.rightXClamp;
        lightCursor.downYClamp = this.downYClamp;
        lightCursor.upYClamp = this.upYClamp;
        if (player.position.x >= xTrigger && !isTimelinePlayed)
        {
            playableDirector.initialTime = 0.2f;
            playableDirector.Play();
            isTimelinePlayed = true;
        }
        else if (!isTimelinePlayed)
        {
            playableDirector.time = 0f;
            vCam1.enabled = false;
        }
        else if (isTimelinePlayed && playableDirector.time >= 2f)
        {
            playableDirector.time = 2f;
            vCam1.enabled = true;
            isTimelinePlayed = false;
            this.enabled = false;
        }
    }
}