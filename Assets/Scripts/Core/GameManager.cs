using System;
using UnityEngine;
using UnityEngine.Serialization;

// Owns the game rules: lives, score, start, game over and restart.
public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [FormerlySerializedAs("pooling")]
    [SerializeField] private BallPool ballPool;
    [SerializeField] private int startLives = 3;
    [SerializeField] private int pointsPerBall = 10;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnLivesChanged;

    private int score;
    private int lives;

    void OnEnable()
    {
        player.OnBallCaught += HandleBallCaught;
        ballPool.OnBallMissed += HandleBallMissed;
    }

    void OnDisable()
    {
        player.OnBallCaught -= HandleBallCaught;
        ballPool.OnBallMissed -= HandleBallMissed;
    }

    void Start()
    {
        StartGame();
    }

    public void Restart()
    {
        ballPool.ResetPool();
        player.ResetPlayer();
        StartGame();
    }

    private void StartGame()
    {
        score = 0;
        lives = startLives;
        OnScoreChanged?.Invoke(score);
        OnLivesChanged?.Invoke(lives);

        Time.timeScale = 1f;
        LevelManager.SetLevelState(LevelState.InProgress);
    }

    private void HandleBallCaught()
    {
        score += pointsPerBall;
        OnScoreChanged?.Invoke(score);
        ScoreProgress.CheckAndUpdateScore(score);
    }

    private void HandleBallMissed()
    {
        if (LevelManager.GetLevelState() != LevelState.InProgress)
            return;

        lives--;
        OnLivesChanged?.Invoke(lives);

        if (lives <= 0)
        {
            Time.timeScale = 0f;
            LevelManager.SetLevelState(LevelState.Failed);
        }
    }
}
