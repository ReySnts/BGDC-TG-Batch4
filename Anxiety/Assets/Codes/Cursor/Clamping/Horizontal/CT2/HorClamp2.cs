public class HorClamp2 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor2>();
        rightCursor = FindObjectOfType<Cursor3>();
    }
}