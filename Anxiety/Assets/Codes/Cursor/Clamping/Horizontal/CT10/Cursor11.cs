public class Cursor11 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -83f;
        lightCursor.rightXClamp = -70f;
        lightCursor.downYClamp = 35.7f;
        lightCursor.upYClamp = 36.9f;
    }
    public override void SetLight()
    {
        spotLight.range = 3.2f;
        spotLight.intensity = 1.5f;
    }
}