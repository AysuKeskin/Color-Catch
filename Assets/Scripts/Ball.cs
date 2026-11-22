using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = -2f;
    private Spawn spawn;
    private Player player;
    public SpriteRenderer playerSpriteRenderer;
    public SpriteRenderer spriteRenderer;
    
    void Update()
    {
        if (LevelManager.GetLevelState() != LevelState.InProgress)
            return;
        
       transform.position +=  speed * new Vector3(0f, 1f, 0f) * Time.deltaTime;
       if(transform.position.y < -5f)
       {
            player.RemainingLives--;
            if(player.RemainingLives <= 0)
            {
                Debug.Log("Game Over");
                Time.timeScale = 0f; 
                LevelManager.SetLevelState(LevelState.Failed);
            }
            
            GetComponentInParent<Pooling>().ReturnBall(this);
            spawn.SpawnedBalls.Remove(this);
       }


    }

    public void Init(Player player, Spawn spawn)
    {
        this.player = player;
        this.spawn = spawn;
        this.playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
    }
    
}