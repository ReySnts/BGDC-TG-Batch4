using UnityEngine;
public class HandWallsMovement : MonoBehaviour
{
    public static HandWallsMovement objInstance = null;
    [Range(0f, 6f)] [SerializeField] float moveSpeed = 2f;
    public Vector3 initialPosition = Vector3.zero;
    string colliderName = "Player";
    bool isPlayerFelt = false;
    void Awake()
    {
        if (objInstance == null && SceneManagement.GetCurrentScene() == 3) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void Start()
    {
        initialPosition = transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == colliderName) isPlayerFelt = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == colliderName) isPlayerFelt = false;
    }
    void Update()
    {
        #region Feel Player
        if (!isPlayerFelt && !Fear.objInstance.isDie) moveSpeed = 2f;
        else moveSpeed = 0f;
        #endregion
        transform.position += (Vector3.right * moveSpeed * Time.deltaTime);
    }
}