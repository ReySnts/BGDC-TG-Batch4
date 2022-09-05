using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : MonoBehaviour
{
<<<<<<< HEAD
    public List<GameObject> checkPoints = new List<GameObject>();
    [Header("Values")]
    public bool isTriggered = false;
    public Vector3 respawnPoint = new Vector3(0f, 0f, 0f);
    float yDiff = 0.64214f;
    string colliderName = "Moshrum";
=======
    public static CheckPoint objInstance = null;
    [Header("Values")]
    public List<GameObject> checkPoints = new List<GameObject>();
    public List<Vector3> respawnPoints = new List<Vector3>();
    public readonly string colliderName = "Moshrum";
    public string checkPointName = null;
    readonly float yDiff = 0.64214f;
    bool isScriptEnabled = false;
    [Header("Blockades")]
    public List<Animator> spikeBlockade1Animators = new List<Animator>();
    public List<Animator> spikeBlockade2Animators = new List<Animator>();
    string spikeBlockAnimParamName = "IsActive";
    GameObject spikeBlockade1 = null;
    GameObject spikeBlockade2 = null;
    void Awake()
    {
        if (
            objInstance == null && 
            (
                SceneManagement.GetCurrentScene() == 1 || 
                SceneManagement.GetCurrentScene() == 2 ||
                SceneManagement.GetCurrentScene() == 3
            )
        ) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void OnEnable()
    {
        isScriptEnabled = true;
    }
    void OnDisable()
    {
        isScriptEnabled = false;
    }
>>>>>>> programming
    void Start()
    {
        respawnPoints.Add(transform.position);
        #region Set SpikeBlocks For Level 1
        try
        {
            (spikeBlockade1 = GameObject.Find("Border (8)")).SetActive(false);
            (spikeBlockade2 = GameObject.Find("Border (9)")).SetActive(false);
        }
        catch
        {
            spikeBlockade1 = spikeBlockade2 = null;
        }
        #endregion
    }
    void OnTriggerEnter(Collider other)
    {
        try
        {
            if (!isTriggered && other.name.Substring(0, colliderName.Length) == colliderName)
            {
<<<<<<< HEAD
=======
                if (checkPointName == null) checkPointName = other.name;
                else if (checkPointName.CompareTo(other.name) < 0) checkPointName = other.name;
                #region Turn On Blockade For Level 1
                try
                {
                    switch (checkPointName)
                    {
                        case "Moshrum (2)":
                            spikeBlockade1.SetActive(true);
                            foreach (Animator spikeBlock1Animator in spikeBlockade1Animators) spikeBlock1Animator.SetBool(spikeBlockAnimParamName, true);
                            break;
                        case "Moshrum (3)":
                            spikeBlockade2.SetActive(true);
                            foreach (Animator spikeBlock2Animator in spikeBlockade2Animators) spikeBlock2Animator.SetBool(spikeBlockAnimParamName, true);
                            GameObject.Find("VirtualCams").SetActive(false);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    spikeBlockade1 = spikeBlockade2 = null;
                    spikeBlockade1Animators = spikeBlockade2Animators = null;
                }
                #endregion
>>>>>>> programming
                checkPoints.Add(other.gameObject);
                respawnPoints.Add(checkPoints[0].transform.position + Vector3.up * yDiff);
                Destroy(checkPoints[0]);
                checkPoints.Clear();
                isTriggered = true;
            }
        }
        catch { }
    }
}