public class HorClamp3 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor3>();
        rightCursor = FindObjectOfType<Cursor4>();
    }
}