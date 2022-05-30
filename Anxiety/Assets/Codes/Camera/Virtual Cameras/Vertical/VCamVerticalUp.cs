using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public abstract class VCamVerticalUp : MonoBehaviour
{
    public Transform player = null;
    [Header("References List")]
    public PlayableDirector playableDirector = null;
    public VCamVerticalDown vCamVerticalDown = null;
    [Header("Value Arrays")]
    public Vector2 yTrigger = new Vector2(0f, 0f);
    public bool isTimelinePlayed = false;
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    public Vector2 yClamp = new Vector2(9.8f, 13.45f);
    void Start()
    {
        lightCursor = FindObjectOfType<LightCursor>();
        isTimelinePlayed = false;
        yTrigger.y = vCamVerticalDown.yTrigger.y;
        vCamVerticalDown.enabled = false;
    }
    void Update()
    {
        lightCursor.downYClamp = player.position.y - 10.25814f + yClamp.x;
        lightCursor.upYClamp = player.position.y + yClamp.y - 10.25814f;
        if (!isTimelinePlayed)
        {
            vCamVerticalDown.enabled = false;
            if (player.position.x >= yTrigger.x && player.position.x < yTrigger.y)
            {
                playableDirector.initialTime = 0.2f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 2f)
        {
            playableDirector.time = 2f;
            vCamVerticalDown.enabled = true;
            isTimelinePlayed = false;
            enabled = false;
        }
    }
}