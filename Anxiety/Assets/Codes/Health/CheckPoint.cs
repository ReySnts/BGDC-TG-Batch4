using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    public static CheckPoint objInstance = null;
    public List<GameObject> checkPoints = new List<GameObject>();
    [Header("Values")]
    public static string checkPointName = null;
    public Vector3 respawnPoint = new Vector3(0f, 0f, 0f);
    bool isScriptEnabled = false;
    string colliderName = "Moshrum";
    float yDiff = 0.64214f;
    void Awake()
    {
        if (
            objInstance == null && 
            (
                SceneManagement.GetCurrentScene() == 1 || 
                SceneManagement.GetCurrentScene() == 2
            )
        ) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        respawnPoint = transform.position;
    }
    void OnDisable()
    {
        isScriptEnabled = false;
    }
    void OnEnable()
    {
        isScriptEnabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        try
        {
            if (isScriptEnabled && other.name.Substring(0, colliderName.Length) == colliderName)
            {
                checkPointName = other.name;
                checkPoints.Add(other.gameObject);
                respawnPoint = checkPoints[0].transform.position + Vector3.up * yDiff;
                Destroy(checkPoints[0]);
                checkPoints.Clear();
            }
        }
        catch { }
    }
}