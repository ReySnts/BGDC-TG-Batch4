using UnityEngine;
using UnityEngine.Playables;
public class VCam3 : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector3 = null;
    public Cam2D cam2D = null;
    [Header("Values")]
    [SerializeField] float xTrigger = 0f;
    [SerializeField] bool isTimeline3Played = false;
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    [SerializeField] Vector2 yClamp = new Vector2(0f, 12f);
    void Awake()
    {
        lightCursor = FindObjectOfType<LightCursor>();
        cam2D = FindObjectOfType<Cam2D>();
        xTrigger = cam2D.thirdXTrigger;
    }
    void Update()
    {
        lightCursor.downYClamp = yClamp.x;
        lightCursor.upYClamp = yClamp.y;
        if (!isTimeline3Played)
        {
            playableDirector3.time = 2f;
            cam2D.enabled = false;
            if (player.position.x < xTrigger)
            {
                playableDirector3.initialTime = 2.5f;
                playableDirector3.Play();
                isTimeline3Played = true;
            }
        }
        else if (playableDirector3.time >= 4f)
        {
            playableDirector3.Stop();
            cam2D.enabled = true;
            isTimeline3Played = false;
            enabled = false;
        }
    }
}