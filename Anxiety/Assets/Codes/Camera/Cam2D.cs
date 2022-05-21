using UnityEngine;
using UnityEngine.Playables;
public class Cam2D : MonoBehaviour
{
    // Init Player Pos: (-164.2, 10.25814, 40)
    public Transform player = null;
    [Header("Timelines")]
    public PlayableDirector playableDirector = null;
    public PlayableDirector playableDirector2 = null;
    public PlayableDirector playableDirector3 = null;
    [Header("Transition Checks")]
    [SerializeField] bool isTimelinePlayed = false;
    [SerializeField] bool isTimeline2Played = false;
    [SerializeField] bool isTimeline3Played = false;
    [Header("Virtual Cameras")]
    public VCam1 vCam1 = null;
    public VCam2 vCam2 = null;
    public VCam3 vCam3 = null;
    [Header("X Transitions")]
    public Vector2 firstXTrigger = new Vector2(-156f, 0f);
    public Vector2 secondXTrigger = new Vector2(-132f, 0f);
    public float thirdXTrigger = -113f;
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    public Vector2 yClamp = new Vector2(9.8f, 13.45f);
    void Start()
    {
        lightCursor = FindObjectOfType<LightCursor>();

        vCam1 = FindObjectOfType<VCam1>();
        firstXTrigger.y = vCam1.xTrigger.y;
        vCam1.enabled = false;

        vCam2 = FindObjectOfType<VCam2>();
        secondXTrigger.y = vCam2.xTrigger.y;
        vCam2.enabled = false;

        vCam3 = FindObjectOfType<VCam3>();
        vCam3.enabled = false;
    }
    void Update()
    {
        lightCursor.downYClamp = yClamp.x;
        lightCursor.upYClamp = yClamp.y;

        if (!isTimelinePlayed)
        {
            vCam1.enabled = false;
            if (player.position.x >= firstXTrigger.x && player.position.x < firstXTrigger.y)
            {
                playableDirector.initialTime = 0.2f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 2f)
        {
            playableDirector.time = 2f;
            vCam1.enabled = true;
            isTimelinePlayed = false;
            enabled = false;
        }

        if (!isTimeline2Played)
        {
            vCam2.enabled = false;
            if (player.position.x >= secondXTrigger.x && player.position.x < secondXTrigger.y)
            {
                playableDirector2.initialTime = 0.2f;
                playableDirector2.Play();
                isTimeline2Played = true;
            }
        }
        else if (playableDirector2.time >= 2f)
        {
            playableDirector2.time = 2f;
            vCam2.enabled = true;
            isTimeline2Played = false;
            enabled = false;
        }

        if (!isTimeline3Played)
        {
            vCam3.enabled = false;
            if (player.position.x >= thirdXTrigger)
            {
                playableDirector3.initialTime = 0.2f;
                playableDirector3.Play();
                isTimeline3Played = true;
            }
        }
        else if (playableDirector3.time >= 2f)
        {
            playableDirector3.time = 2f;
            vCam3.enabled = true;
            isTimeline3Played = false;
            enabled = false;
        }
    }
}