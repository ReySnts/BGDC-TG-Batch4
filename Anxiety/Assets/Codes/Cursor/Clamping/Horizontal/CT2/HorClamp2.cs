public class HorClamp2 : HorClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor2>();
        rightCursor = FindObjectOfType<Cursor3>();
    }
}