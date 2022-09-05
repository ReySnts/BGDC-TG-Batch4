public abstract class VCamVerticalUp : VCamVertical
{
    public VCamVerticalDown vCamVerticalDown = null;
    protected void Update()
    {
        if (!isTimelinePlayed)
        {
            vCamVerticalDown.enabled = false;
            if (player.position.y >= yTrigger.x && player.position.y < yTrigger.y)
            {
                playableDirector.initialTime = 0.2f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 2f)
        {
            playableDirector.time = 2f;
            vCamVerticalDown.enabled = true;
            isTimelinePlayed = false;
            enabled = false;
        }
    }
}