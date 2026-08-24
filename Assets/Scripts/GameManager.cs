using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Game Configuration")]
    [SerializeField]
    private float laneSpacing = 2f;

    [Header("Game State")]
    [SerializeField]
    private float playerDistance = 0f;

    [SerializeField]
    private int coinsCollected = 0;

    [SerializeField]
    private bool gameOver = false;

    [Header("References")]
    [SerializeField]
    private PlayerHandler playerHandler;


    private void Start()
    {
        InitializeGame();
    }


    private void Update()
    {
        if (gameOver)
            return;

        HandleInput();
        UpdateDistance();
    }


    private void InitializeGame()
    {
        gameOver = false;
        playerDistance = 0f;
        coinsCollected = 0;

        if (playerHandler != null)
        {
            playerHandler.Initialize(laneSpacing);
        }
    }


    private void HandleInput()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.aKey.wasPressedThisFrame ||
            Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            MoveLeft();
        }

        if (Keyboard.current.dKey.wasPressedThisFrame ||
            Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            MoveRight();
        }
    }


    private void MoveLeft()
    {
        if (playerHandler != null)
        {
            playerHandler.MoveLeft();
        }
    }


    private void MoveRight()
    {
        if (playerHandler != null)
        {
            playerHandler.MoveRight();
        }
    }


    private void UpdateDistance()
    {
        if (playerHandler != null)
        {
            playerDistance = playerHandler.GetDistance();
        }
    }


    public void CoinCollected()
    {
        coinsCollected++;
    }


    public void ObstacleHit()
    {
        GameOver();
    }


    private void GameOver()
    {
        gameOver = true;

        if (playerHandler != null)
        {
            playerHandler.SetAlive(false);
        }
    }
}