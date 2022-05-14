using UnityEngine;
using UnityEngine.Playables;
public class VCam1 : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public Cam2D cam2D = null;
    float xTrigger = -156f;
    [SerializeField] bool isTimelinePlayed = false;
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    [SerializeField] float leftXClamp = -164f;
    [SerializeField] float rightXClamp = -50f;
    [SerializeField] float downYClamp = 3.4f;
    [SerializeField] float upYClamp = 13.45f;
    void Start()
    {
        lightCursor = FindObjectOfType<LightCursor>();
        playableDirector = FindObjectOfType<PlayableDirector>();
        cam2D = FindObjectOfType<Cam2D>();
    }
    void Update()
    {
        lightCursor.leftXClamp = this.leftXClamp;
        lightCursor.rightXClamp = this.rightXClamp;
        lightCursor.downYClamp = this.downYClamp;
        lightCursor.upYClamp = this.upYClamp;
        if (player.position.x < xTrigger && !isTimelinePlayed)
        {
            playableDirector.initialTime = 2.5f;
            playableDirector.Play();
            isTimelinePlayed = true;
        }
        else if (!isTimelinePlayed)
        {
            playableDirector.time = 2f;
            cam2D.enabled = false;
        }
        else if (isTimelinePlayed && playableDirector.time >= 4f)
        {
            playableDirector.Stop();
            playableDirector.initialTime = 0f;
            cam2D.enabled = true;
            isTimelinePlayed = false;
            this.enabled = false;
        }
    }
}