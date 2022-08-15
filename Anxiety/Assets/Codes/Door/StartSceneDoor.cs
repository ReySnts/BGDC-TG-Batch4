using UnityEngine;
using System.Collections;
public class StartSceneDoor : Door
{
    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(waitTime);
        doorCollider.enabled = true;
        player.SetActive(true);
        try
        {
            Ending.objInstance.enabled = true;
        }
        catch
        {
            Ending.objInstance = null;
        }
    }
    void Awake()
    {
        isOpened = doorCollider.enabled = false;
        doorControl.SetBool(animParamName, isOpened);
        player.SetActive(false);
        try
        {
            Ending.objInstance.enabled = false;
        }
        catch
        {
            Ending.objInstance = null;
        }
        StartCoroutine(StartScene());
    }
}