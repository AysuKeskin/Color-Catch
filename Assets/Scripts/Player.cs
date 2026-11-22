using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Spawn spawn;
    [SerializeField] private Pooling pooling;
    public event Action<int> onScoreChanged;
    public event Action<int> onLivesChanged;
    private int colorIndex = 0;
    private int remainingLives = 3;
    public int RemainingLives
    {
        get => remainingLives;
        set
        {
            if (remainingLives == value)
                return;

            remainingLives = value;
            onLivesChanged?.Invoke(value);
        }
    }
    private int score = 0;

    void Start()
    {
        Time.timeScale = 1f;
        LevelManager.SetLevelState(LevelState.InProgress);
        RemainingLives = 3;
        onLivesChanged?.Invoke(RemainingLives);
        onScoreChanged?.Invoke(score);
        spriteRenderer.color = ColorMap.Colors[colorIndex];
    }

    void Update()
    {
        if (LevelManager.GetLevelState() != LevelState.InProgress)
        {
            return;
        }
        float horizontal = 0f;
        int change = 0;


        if (Keyboard.current != null)
        {
            if ((Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) && transform.position.x > -3f) horizontal = -1f;
            if ((Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) && transform.position.x < 3f) horizontal = 1f;
            if (Keyboard.current.spaceKey.wasPressedThisFrame) change = 1;
        }
        if (change == 1)
        {
            colorIndex = (colorIndex + 1) % ColorMap.Colors.Length;
            spriteRenderer.color = ColorMap.Colors[colorIndex];

        }

        Vector2 movement = new Vector2(horizontal, 0f);
        transform.position += (Vector3)movement * speed * Time.deltaTime;
        CheckCollision();
    }
    private void CheckCollision()
    {
        for (int i = spawn.SpawnedBalls.Count - 1; i >= 0; i--)
        {
            Ball ball = spawn.SpawnedBalls[i];
            if (ball != null && ball.ColorIndex == colorIndex && IsTouching(ball))
            {
                pooling.ReturnBall(ball);
                spawn.SpawnedBalls.RemoveAt(i);
                score += 10;
                onScoreChanged?.Invoke(score);
                ScoreProgress.CheckAndUpdateScore(score);
            }
        }
    }

    private bool IsTouching(Ball ball)
    {
        return ball.spriteRenderer.bounds.Intersects(spriteRenderer.bounds);
    }

    public void ResetPlayer()
    {
        RemainingLives = 3;
        score = 0;
        onScoreChanged?.Invoke(score);
        onLivesChanged?.Invoke(RemainingLives);
        Time.timeScale = 1f;
        colorIndex = 0;
        pooling.ResetPool();
        spriteRenderer.color = ColorMap.Colors[colorIndex];
    }


}