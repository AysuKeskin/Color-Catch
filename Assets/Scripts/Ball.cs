using UnityEngine;
public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Spawn spawn;
    private Player player;
    public SpriteRenderer spriteRenderer;
    private Pooling pooling;
    public int ColorIndex { get; private set; }

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
        if (transform.position.y < -5f)
        {
            player.RemainingLives--;
            if (player.RemainingLives <= 0)
            {
                Debug.Log("Game Over");
                Time.timeScale = 0f;
                LevelManager.SetLevelState(LevelState.Failed);
            }

            pooling.ReturnBall(this);
            spawn.SpawnedBalls.Remove(this);
        }


    }

    public void Init(Player player, Spawn spawn, Pooling pooling)
    {
        this.player = player;
        this.spawn = spawn;
        this.pooling = pooling;
    }

}