using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using System;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private float xLimit = 3f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [FormerlySerializedAs("pooling")]
    [SerializeField] private BallPool ballPool;
    public event Action OnBallCaught;
    private int colorIndex = 0;

    void Start()
    {
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
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) horizontal = -1f;
            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) horizontal = 1f;
            if (Keyboard.current.spaceKey.wasPressedThisFrame) change = 1;
        }
        if (change == 1)
        {
            colorIndex = (colorIndex + 1) % ColorMap.Colors.Length;
            spriteRenderer.color = ColorMap.Colors[colorIndex];

        }

        Vector2 movement = new Vector2(horizontal, 0f);
        transform.position += (Vector3)movement * speed * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        transform.position = pos;

        CheckCollision();
    }
    private void CheckCollision()
    {
        for (int i = ballPool.ActiveBalls.Count - 1; i >= 0; i--)
        {
            Ball ball = ballPool.ActiveBalls[i];
            if (ball != null && ball.ColorIndex == colorIndex && IsTouching(ball))
            {
                ballPool.Despawn(ball);
                OnBallCaught?.Invoke();
            }
        }
    }

    private bool IsTouching(Ball ball)
    {
        return ball.spriteRenderer.bounds.Intersects(spriteRenderer.bounds);
    }

    public void ResetPlayer()
    {
        colorIndex = 0;
        spriteRenderer.color = ColorMap.Colors[colorIndex];
    }


}