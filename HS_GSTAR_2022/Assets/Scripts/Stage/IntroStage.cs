public sealed class IntroStage : Stage
{
    public override void StageEnter()
    {
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
    }

    public void StartEasyGame()
    {
        StageManager.Instance.DifficultyMultiple = 1;
        StageManager.Instance.NextStage();
    }
    
    public void StartNormalGame()
    {
        StageManager.Instance.DifficultyMultiple = 1.5f;
        StageManager.Instance.NextStage();
    }
    
    public void StartHardGame()
    {
        StageManager.Instance.DifficultyMultiple = 2;
        StageManager.Instance.NextStage();
    }
}
