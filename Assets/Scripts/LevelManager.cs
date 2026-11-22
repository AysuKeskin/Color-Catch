using UnityEngine;
using System;
public static class LevelManager
{
    private static LevelState currentLevelState = LevelState.InProgress;
    public static event Action onLose;

    public static void SetLevelState(LevelState newState)
    {
        currentLevelState = newState;
        if (newState == LevelState.Failed)
        {
            // Trigger the onLose event when the level state changes to Failed
            onLose?.Invoke();
        }
    }

    public static LevelState GetLevelState()
    {
        return currentLevelState;
    }
}