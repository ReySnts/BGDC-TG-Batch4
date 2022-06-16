public class Cursor1 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -164f;
        lightCursor.rightXClamp = -155f;
        lightCursor.downYClamp = 10f;
        lightCursor.upYClamp = 12.8f;
    }
    public override void SetLight()
    {
        spotLight.range = 3.2f;
        spotLight.intensity = 1.5f;
        sphere.localScale = UnityEngine.Vector3.right * spotLight.range * 2f;
    }
}