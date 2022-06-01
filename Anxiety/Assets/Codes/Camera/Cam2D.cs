using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Cam2D : MonoBehaviour
{
    int totalTransitions = 4;
    int camIdx = 0;
    public Transform player = null;
    [Header("References List")]
    public List<PlayableDirector> playDirList = new List<PlayableDirector>();
    public List<VCamHorizontal> vCamHorList = new List<VCamHorizontal>();
    [Header("Value Arrays")]
    public Vector2[] xTriggers = null;
    [SerializeField] bool[] isTimelinePlayed = null;
    [Header("Cursor Position")]
    public LightCursor lightCursor = null;
    public Vector2 yClamp = new Vector2(9.8f, 13.45f);
    void Start()
    {
        lightCursor = FindObjectOfType<LightCursor>();
        totalTransitions = vCamHorList.Count;
        isTimelinePlayed = new bool[totalTransitions];
        for (camIdx = 0; camIdx < totalTransitions; camIdx++)
        {
            isTimelinePlayed[camIdx] = false;
            xTriggers[camIdx].y = vCamHorList[camIdx].xTrigger.y;
            vCamHorList[camIdx].enabled = false;
        }
    }
    void Update()
    {
        lightCursor.downYClamp = player.position.y - 10.25814f + yClamp.x;
        lightCursor.upYClamp = player.position.y + yClamp.y - 10.25814f;
        for (camIdx = 0; camIdx < totalTransitions; camIdx++)
        {
            if (!isTimelinePlayed[camIdx])
            {
                vCamHorList[camIdx].enabled = false;
                if (player.position.x >= xTriggers[camIdx].x && player.position.x < xTriggers[camIdx].y)
                {
                    playDirList[camIdx].initialTime = 0.2f;
                    playDirList[camIdx].Play();
                    isTimelinePlayed[camIdx] = true;
                }
            }
            else if (playDirList[camIdx].time >= 2f)
            {
                playDirList[camIdx].time = 2f;
                vCamHorList[camIdx].enabled = true;
                isTimelinePlayed[camIdx] = false;
                enabled = false;
            }
        }
    }
}