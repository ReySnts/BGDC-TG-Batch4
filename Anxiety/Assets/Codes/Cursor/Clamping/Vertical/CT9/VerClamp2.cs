public class VerClamp2 : VerClamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor9>();
        rightCursor = FindObjectOfType<Cursor10>();
    }
    void Update()
    {
        if (
            Fear.objInstance.hasDied && 
            CheckPoint.objInstance.checkPointName == CheckPoint.objInstance.colliderName + " (2)"
        ) TurnOnLeftCursor();
    }
}