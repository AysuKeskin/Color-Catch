using System;
public static class LevelManager
{
    private static LevelState currentLevelState = LevelState.InProgress;
    public static event Action OnLose;

    public static void SetLevelState(LevelState newState)
    {
        currentLevelState = newState;
        if (newState == LevelState.Failed)
        {
            // Trigger the OnLose event when the level state changes to Failed
            OnLose?.Invoke();
        }
    }

    public static LevelState GetLevelState()
    {
        return currentLevelState;
    }
}