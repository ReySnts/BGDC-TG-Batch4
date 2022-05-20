using UnityEngine;
using UnityEngine.Playables;
public class VCam2 : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public Cam2D cam2D = null;
    [Header("Values")]
    public Vector2 xTrigger = new Vector2(0f, -116f);
    [SerializeField] bool isTimelinePlayed = false;
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    [SerializeField] Vector2 yClamp = new Vector2(3.4f, 13.45f);
    void Awake()
    {
        lightCursor = FindObjectOfType<LightCursor>();
        cam2D = FindObjectOfType<Cam2D>();
        xTrigger.x = cam2D.secondXTrigger.x;
    }
    void Update()
    {
        lightCursor.downYClamp = yClamp.x;
        lightCursor.upYClamp = yClamp.y;
        if (!isTimelinePlayed)
        {
            playableDirector.time = 2f;
            cam2D.enabled = false;
            if (player.position.x < xTrigger.x || player.position.x >= xTrigger.y)
            {
                playableDirector.initialTime = 2.5f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 4f)
        {
            playableDirector.Stop();
            cam2D.enabled = true;
            isTimelinePlayed = false;
            this.enabled = false;
        }
    }
}