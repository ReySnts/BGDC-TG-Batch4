public class VerClamp2 : Clamp
{
    protected override void SetCursor()
    {
        leftCursor = FindObjectOfType<Cursor9>();
        rightCursor = FindObjectOfType<Cursor10>();
    }
<<<<<<< HEAD
=======
    void Update()
    {
        if (
            Fear.objInstance.hasDied && 
            CheckPoint.objInstance.checkPointName == CheckPoint.objInstance.colliderName + " (2)"
        ) TurnOnLeftCursor();
    }
>>>>>>> programming
}