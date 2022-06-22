public class HorClamp8 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor10>();
        rightCursor = FindObjectOfType<Cursor11>();
    }
}