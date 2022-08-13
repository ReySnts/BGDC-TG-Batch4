using UnityEngine;
using System.Collections;
public class StartSceneDoor : Door
{
    IEnumerator StartScene()
    {
        doorCollider.enabled = false;
        player.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        doorCollider.enabled = true;
        player.SetActive(true);
    }

    void Awake()
    {
        isOpened = false;
        doorControl.SetBool(animParamName, isOpened);
        StartCoroutine(StartScene());
    }
}