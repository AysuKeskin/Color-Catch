using System;
using UnityEngine;
public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float bottomY = -5f;
    public SpriteRenderer spriteRenderer;
    public int ColorIndex { get; private set; }
    public event Action<Ball> OnMissed;

    public void SetColor(int index)
    {
        ColorIndex = index;
        spriteRenderer.color = ColorMap.Colors[index];
    }

    void Update()
    {
        if (LevelManager.GetLevelState() != LevelState.InProgress)
            return;

        transform.position += speed * Vector3.down * Time.deltaTime;
        if (transform.position.y < bottomY)
        {
            OnMissed?.Invoke(this);
        }
    }
}