public class Cursor8 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -110f;
        lightCursor.rightXClamp = -86f;
        lightCursor.downYClamp = 1f;
        lightCursor.upYClamp = 12f;
    }
    public override void SetLight()
    {
        spotLight.range = 4.8f;
        spotLight.intensity = 2.4f;
        sphere.localScale = UnityEngine.Vector3.right * spotLight.range * 2f;
    }
}