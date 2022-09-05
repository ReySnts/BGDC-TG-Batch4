public abstract class VCamVerticalDown : VCamVertical
{
    public VCamVerticalUp vCamVerticalUp = null;
    protected void Start()
    {
        enabled = false;
    }
    protected void Update()
    {
        if (!isTimelinePlayed)
        {
            playableDirector.time = 2f;
            vCamVerticalUp.enabled = false;
            if (player.position.y >= yTrigger.x && player.position.y < yTrigger.y)
            {
                playableDirector.initialTime = 2.5f;
                playableDirector.Play();
                isTimelinePlayed = true;
            }
        }
        else if (playableDirector.time >= 4f)
        {
            playableDirector.Stop();
            vCamVerticalUp.enabled = true;
            isTimelinePlayed = false;
            enabled = false;
        }
    }
}