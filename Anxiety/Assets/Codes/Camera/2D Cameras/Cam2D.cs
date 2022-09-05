using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Cam2D : MonoBehaviour
{
    [Header("References List")]
    public Transform player = null;
    public List<PlayableDirector> playDirList = new List<PlayableDirector>();
    public List<VCamHorizontal> vCamHorList = new List<VCamHorizontal>();
    [Header("Values")]
    public Vector2[] xTriggers = null;
    public bool[] isTimelinePlayed = null;
    int totalTransitions = 3;
    int camIdx = 0;
    void Start()
    {
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