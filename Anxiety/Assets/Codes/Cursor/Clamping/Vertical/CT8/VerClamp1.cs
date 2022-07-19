public class VerClamp1 : VerClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor8>();
        rightCursor = FindObjectOfType<Cursor9>();
    }
    void Update()
    {
        if (Fear.hasDied && CheckPoint.checkPointName == "Moshrum (1)") TurnOnLeftCursor();
    }
}