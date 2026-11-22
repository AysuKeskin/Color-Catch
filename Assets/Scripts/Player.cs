using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Spawn spawn;
    [SerializeField] private Pooling pooling;
    public event Action<int> onScoreChanged;
    public event Action<int> onLivesChanged;
    private int order = 0;
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
    
    void Start(){
        Time.timeScale = 1f;
        LevelManager.SetLevelState(LevelState.InProgress);
        RemainingLives = 3;
        onLivesChanged?.Invoke(RemainingLives);
        onScoreChanged?.Invoke(score);
        spriteRenderer.color = ColorMap.Map[order];
    }

    void Update()
    {
        if(LevelManager.GetLevelState() != LevelState.InProgress)
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
        if(change == 1)
        {
            order = (order + 1) % ColorMap.Map.Count; 
            spriteRenderer.color = ColorMap.Map[order];
            
        }

        Vector2 movement = new Vector2(horizontal, 0f);
        transform.position += (Vector3)movement * speed * Time.deltaTime;
        CheckCollision();
    }
    private void CheckCollision()
    {
        foreach (Ball ball in spawn.SpawnedBalls)
        {
            if(ball != null && ball.spriteRenderer.color == spriteRenderer.color && CheckCollision(ball) == 1)
            {
                pooling.ReturnBall(ball);
                spawn.SpawnedBalls.Remove(ball);
                score += 10;
                onScoreChanged?.Invoke(score);
                ScoreProgress.CheckAndUpdateScore(score);
                break;
            }
            
        }
    }

    private int CheckCollision(Ball ball)
    {
        Bounds boundsA = ball.GetComponent<Renderer>().bounds;
        Bounds boundsB = GetComponent<Renderer>().bounds;

        // If the bounding boxes overlap, the distance is 0
        if (boundsA.Intersects(boundsB))
        {
            return 1;
        }
        return 0;

    }

    public void ResetPlayer()
    {
        RemainingLives = 3;
        score = 0;
        onScoreChanged?.Invoke(score);
        onLivesChanged?.Invoke(RemainingLives);
        Time.timeScale = 1f;
        order = 0;
        pooling.ResetPool();
        spriteRenderer.color = ColorMap.Map[order];
    }


}