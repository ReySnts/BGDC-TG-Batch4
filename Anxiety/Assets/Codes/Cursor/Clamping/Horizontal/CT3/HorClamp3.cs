public class HorClamp3 : HorClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor3>();
        rightCursor = FindObjectOfType<Cursor4>();
    }
}