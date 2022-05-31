using UnityEngine;
using UnityEngine.Playables;
public class VCamVerticalDown : MonoBehaviour
{
    [Header("References")]
    public Transform player = null;
    public PlayableDirector playableDirector = null;
    public VCamVerticalUp vCamVerticalUp = null;
    [Header("Values")]
    public float yTrigger = 0f;
    public bool isTimelinePlayed = false;
    void Awake()
    {
        vCamVerticalUp = FindObjectOfType<VCamVerticalUp>();
    }
    void Update()
    {
        if (!isTimelinePlayed)
        {
            playableDirector.time = 2f;
            vCamVerticalUp.enabled = false;
            if (player.position.y < yTrigger)
            {
                playableDirector.initialTime = 2.5f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 4f)
        {
            playableDirector.Stop();
            vCamVerticalUp.enabled = true;
            isTimelinePlayed = false;
            enabled = false;
        }
    }
}