using UnityEngine;
public abstract class SetCursor : MonoBehaviour
{
    [Header("References")]
    public LightCursor lightCursor = null;
    public Light spotLight = null;
    public abstract void SetClamp();
    public abstract void SetLight();
}