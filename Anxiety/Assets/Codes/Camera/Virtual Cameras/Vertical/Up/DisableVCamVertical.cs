using System.Collections.Generic;
using UnityEngine;
public class DisableVCamVertical : MonoBehaviour
{
    public List<GameObject> cameras = new List<GameObject>();
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            foreach (GameObject cam in cameras) cam.SetActive(false);
    }
}