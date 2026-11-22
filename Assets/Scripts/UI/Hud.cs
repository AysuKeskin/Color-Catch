using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
public class Hud : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private SpritesCatalog spritesCatalog;
    [SerializeField] private List<Image> lifeImages;
    void OnEnable()
    {
        gameManager.OnScoreChanged += UpdateScore;
        gameManager.OnLivesChanged += UpdateLives;
    }
    void OnDisable()
    {
        gameManager.OnScoreChanged -= UpdateScore;
        gameManager.OnLivesChanged -= UpdateLives;
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    private void UpdateLives(int lives)
    {
        foreach (var image in lifeImages)
        {
            if (lives > 0)
            {
                image.sprite = spritesCatalog.sprites[0];
            }
            else
            {
                image.sprite = spritesCatalog.sprites[1];
            }
            lives--;
        }
    }
}
