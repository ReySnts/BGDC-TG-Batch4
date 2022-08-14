using UnityEngine;
public class HandWallsMovement : MonoBehaviour
{
    public static HandWallsMovement objInstance = null;
    [Range(0f, 6f)] [SerializeField] float moveSpeed = 2f;
    Vector3 initialPosition = Vector3.zero;
    void Awake()
    {
        if (objInstance == null && SceneManagement.GetCurrentScene() == 3) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void ResetPosition()
    {
        transform.position = initialPosition;
    }
    public void CheckFeeling(bool isPlayerFelt)
    {
        if (isPlayerFelt) moveSpeed = 0f;
        else moveSpeed = 2f;
    }
    void OnEnable()
    {
        HandWall.stopMovement += CheckFeeling;
        Fear.objInstance.resetHandWallsPosition += ResetPosition;
    }
    void Start()
    {
        initialPosition = transform.position;
        Fear.objInstance.resetHandWallsPosition?.Invoke();
    }
    void Update()
    {
        transform.position += (Vector3.right * moveSpeed * Time.deltaTime);
    }
    void OnDisable()
    {
        HandWall.stopMovement -= CheckFeeling;
        Fear.objInstance.resetHandWallsPosition -= ResetPosition;
    }
}