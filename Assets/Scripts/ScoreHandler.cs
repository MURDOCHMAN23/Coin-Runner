using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameManager gameManager;

    [Header("Testing")]
    public float distance;
    public int coinsCollected;

    [Header("Score")]
    public float coinMultiplier = 10f;

    [SerializeField]
    private float score;

    [Header("UI")]
    [SerializeField]
    private TMP_Text distanceText;

    [SerializeField]
    private TMP_Text coinsText;

    [SerializeField]
    private TMP_Text scoreText;


    private void Update()
    {
        if (gameManager != null)
        {
            distance = gameManager.PlayerDistance;
            coinsCollected = gameManager.CoinsCollected;
        }

        CalculateScore();
        UpdateUI();
    }


    private void CalculateScore()
    {
        score = distance + (coinsCollected * coinMultiplier);
    }


    private void UpdateUI()
    {
        if (distanceText != null)
        {
            distanceText.text = "Distance: " + distance.ToString("0");
        }

        if (coinsText != null)
        {
            coinsText.text = "Coins: " + coinsCollected;
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString("0");
        }
    }
}