public class Cursor4 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -138.5f;
        lightCursor.rightXClamp = -136f;
        lightCursor.downYClamp = 9f;
        lightCursor.upYClamp = 13f;
    }
    public override void SetLight()
    {
        spotLight.range = 3.2f;
        spotLight.intensity = 1.5f;
        sphere.localScale = UnityEngine.Vector3.right * spotLight.range * 2f;
    }
}