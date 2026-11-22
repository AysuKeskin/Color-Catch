using UnityEngine;
public static class ScoreProgress
{
    public static int Score { get { return PlayerPrefs.GetInt("Score", 0); } set { PlayerPrefs.SetInt("Score", value); } }

    public static void CheckAndUpdateScore(int newScore)
    {
        if (newScore > Score)
        {
            Score = newScore;

        }
    }


    public static void ResetScore()
    {
        Score = 0;

    }

}