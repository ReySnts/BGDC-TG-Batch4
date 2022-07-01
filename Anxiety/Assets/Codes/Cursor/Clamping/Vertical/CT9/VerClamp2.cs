public class VerClamp2 : VerClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor9>();
        rightCursor = FindObjectOfType<Cursor10>();
    }
}