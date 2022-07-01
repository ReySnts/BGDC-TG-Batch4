public class HorClamp8 : HorClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor10>();
        rightCursor = FindObjectOfType<Cursor11>();
    }
}