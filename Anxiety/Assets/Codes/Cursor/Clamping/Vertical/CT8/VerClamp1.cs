public class VerClamp1 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor8>();
        rightCursor = FindObjectOfType<Cursor9>();
    }
}