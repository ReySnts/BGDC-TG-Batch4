using UnityEngine;
public class HandWallsMovement : MonoBehaviour
{
    bool isPlayerFelt = false;
    float moveSpeed = 2f;
    void FeelPlayer(bool isPlayerFelt)
    {
        moveSpeed = 0f;
        this.isPlayerFelt = isPlayerFelt;
    }
    void Start()
    {
        HandWall.stopMovement += FeelPlayer;
    }
    void Update()
    {
        transform.position += (Vector3.right * moveSpeed * Time.deltaTime);
    }
}