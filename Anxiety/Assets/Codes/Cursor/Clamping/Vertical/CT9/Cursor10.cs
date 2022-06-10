public class Cursor10 : SetCursor
{
    public override void SetClamp()
    {
        lightCursor.leftXClamp = -110f;
        lightCursor.rightXClamp = -86f;
        lightCursor.downYClamp = 30f;
        lightCursor.upYClamp = 35f;
    }
    public override void SetLight()
    {
        spotLight.range = 4.8f;
        spotLight.intensity = 2.4f;
    }
}