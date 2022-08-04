public class VerClamp2 : VerClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor9>();
        rightCursor = FindObjectOfType<Cursor10>();
    }
    void Update()
    {
        if (Fear.objInstance.hasDied && CheckPoint.checkPointName == "Moshrum (2)") TurnOnLeftCursor();
    }
}