using UnityEngine;
using UnityEngine.Playables;
public abstract class VCamHorizontal : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public Cam2D cam2D = null;
    [Header("Values")]
    public Vector2 xTrigger = new Vector2(0f, 0f);
    [SerializeField] bool isTimelinePlayed = false;
    public abstract void SetVCam(float xTriggerX);
    void Awake()
    {
        cam2D = FindObjectOfType<Cam2D>();
    }
    void Update()
    {
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
            enabled = false;
        }
    }
}