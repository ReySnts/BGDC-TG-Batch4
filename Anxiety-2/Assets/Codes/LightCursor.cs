using UnityEngine;
public class LightCursor : MonoBehaviour
{
    public Camera mainCam = null;
    float x = 0f;
    float y = 0f;
    [SerializeField] float z = 40f;
    Vector3 mousePos = new Vector3(0f, 0f, 0f);
    Vector3 worldPos = new Vector3(0f, 0f, 0f);
    [Header("Constraint")]
    [SerializeField] float leftXClamp = -164f;
    [SerializeField] float rightXClamp = -50f;
    [SerializeField] float downYClamp = 9.5f;
    [SerializeField] float upYClamp = 13.45f;
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = z;
        worldPos = mainCam.ScreenToWorldPoint(mousePos);
        x = Mathf.Clamp(worldPos.x, leftXClamp, rightXClamp); // 10f, 90f
        y = Mathf.Clamp(worldPos.y, downYClamp, upYClamp); // 12f, 20f
        transform.position = new Vector3(x, y, z);
    }
}