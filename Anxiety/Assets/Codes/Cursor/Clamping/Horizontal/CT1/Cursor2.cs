public class Cursor2 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -153f;
        lightCursor.rightXClamp = -146f;
        lightCursor.downYClamp = 4f;
        lightCursor.upYClamp = 12.8f;
    }
    public override void SetLight()
    {
        spotLight.range = 3.6f;
        spotLight.intensity = 3f;
    }
}