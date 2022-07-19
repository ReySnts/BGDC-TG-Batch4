using UnityEngine;
public class LightCursor : MonoBehaviour
{
    [Header("Pointer")]
    public Camera mainCam = null;
    public Transform player = null;
    float x = 0f;
    float y = 0f;
    [SerializeField] float z = 40.5f;
    [SerializeField] [Range(0.01f, 0.5f)] float zDiff = 0.1875f;
    Vector3 mousePos = new Vector3(0f, 0f, 0f);
    Vector3 worldPos = new Vector3(0f, 0f, 0f);
    [Header("Constraint")]
    public float leftXClamp = -164f;
    public float rightXClamp = -155f;
    public float downYClamp = 10f;
    public float upYClamp = 12.8f;
    void Update()
    {
        if (PlayerMovement.leftTurn) z = player.position.z + zDiff;
        else if (PlayerMovement.rightTurn) z = player.position.z - zDiff;
        mousePos = Input.mousePosition;
        mousePos.z = z;
        worldPos = mainCam.ScreenToWorldPoint(mousePos);
        x = Mathf.Clamp(worldPos.x, leftXClamp, rightXClamp);
        y = Mathf.Clamp(worldPos.y, downYClamp, upYClamp);
        transform.position = new Vector3(x, y, z);
    }
}