public class VerClamp2 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor9>();
        rightCursor = FindObjectOfType<Cursor10>();
    }
}