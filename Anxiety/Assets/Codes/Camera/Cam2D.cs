using UnityEngine;
using UnityEngine.Playables;
public class Cam2D : MonoBehaviour
{
    public Transform player = null;
    [SerializeField] bool isTimelinePlayed = false;
    [Header("Timelines")]
    public PlayableDirector playableDirector = null;
    public PlayableDirector playableDirector2 = null;
    [Header("Virtual Cameras")]
    public VCam1 vCam1 = null;
    public VCam2 vCam2 = null;
    [Header("X Transitions")]
    public Vector2 firstXTrigger = new Vector2(-156f, 0f);
    public Vector2 secondXTrigger = new Vector2(-132f, 0f);
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    public Vector2 yClamp = new Vector2(9.8f, 13.45f);
    void Start()
    {
        vCam1 = FindObjectOfType<VCam1>();
        firstXTrigger.y = vCam1.xTrigger.y;
        vCam1.enabled = false;

        vCam2 = FindObjectOfType<VCam2>();
        secondXTrigger.y = vCam2.xTrigger.y;
        vCam2.enabled = false;

        lightCursor = FindObjectOfType<LightCursor>();
    }
    void Update()
    {
        lightCursor.downYClamp = yClamp.x;
        lightCursor.upYClamp = yClamp.y;
        if (!isTimelinePlayed)
        {
            vCam1.enabled = false;
            vCam2.enabled = false;
            if (player.position.x >= firstXTrigger.x && player.position.x < firstXTrigger.y)
            {
                playableDirector.initialTime = 0.2f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
            else if (player.position.x >= secondXTrigger.x && player.position.x < secondXTrigger.y)
            {
                playableDirector2.initialTime = 0.2f;
                playableDirector2.Play();
                isTimelinePlayed = true;
            }
        }
        else
        {
            if (playableDirector.time >= 2f)
            {
                playableDirector.time = 2f;
                vCam1.enabled = true;
                isTimelinePlayed = false;
                this.enabled = false;
            }
            if (playableDirector2.time >= 2f)
            {
                playableDirector2.time = 2f;
                vCam2.enabled = true;
                isTimelinePlayed = false;
                this.enabled = false;
            }
        }
    }
}