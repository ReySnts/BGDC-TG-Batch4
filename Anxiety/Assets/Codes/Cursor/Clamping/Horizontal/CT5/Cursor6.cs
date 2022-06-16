public class Cursor6 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -129f;
        lightCursor.rightXClamp = -119f;
        lightCursor.downYClamp = 4.5f;
        lightCursor.upYClamp = 10.5f;
    }
    public override void SetLight()
    {
        spotLight.range = 4.5f;
        spotLight.intensity = 2.25f;
        sphere.localScale = UnityEngine.Vector3.right * spotLight.range * 2f;
    }
}