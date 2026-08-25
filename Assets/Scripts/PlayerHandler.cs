using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float startSpeed = 5f;

    [SerializeField]
    private float speedGrowthRate = 0.1f;

    [SerializeField]
    private float startSteeringSpeed = 10f;

    [SerializeField]
    private float steeringSpeedGrowthRate = 0.1f;

    public float CurrentForwardSpeed { get; private set; }
    public float CurrentSteeringSpeed { get; private set; }

    [Header("State")]
    [SerializeField]
    private bool alive = true;

    [SerializeField]
    private int currentLane = 0;

    private float laneSpacing;
    private float startingZ;
    private Vector3 startingPosition;


    private void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }


    public void Initialize(float spacing)
    {
        laneSpacing = spacing;
        currentLane = 0;
        alive = true;
    }


    private void Update()
    {
        if (!alive)
            return;

        MoveForward();
        MoveTowardLane();
    }


    private void MoveForward()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();

        CurrentForwardSpeed = startSpeed;

        if (gameManager != null)
        {
            CurrentForwardSpeed += gameManager.PlayerDistance * speedGrowthRate;
        }

        transform.position += Vector3.forward * CurrentForwardSpeed * Time.deltaTime;
    }


    private void MoveTowardLane()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();

        CurrentSteeringSpeed = startSteeringSpeed;

        if (gameManager != null)
        {
            CurrentSteeringSpeed +=
                gameManager.PlayerDistance * steeringSpeedGrowthRate;
        }

        float targetX = currentLane * laneSpacing;

        Vector3 position = transform.position;

        position.x = Mathf.MoveTowards(
            position.x,
            targetX,
            CurrentSteeringSpeed * Time.deltaTime
        );

        transform.position = position;
    }


    public void MoveLeft()
    {
        if (currentLane > -1)
        {
            currentLane--;
        }
    }


    public void MoveRight()
    {
        if (currentLane < 1)
        {
            currentLane++;
        }
    }


    public void CoinTrigger()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.CoinCollected();
        }
    }


    public void ObstacleTrigger()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager != null)
        {
            gameManager.ObstacleHit();
        }
    }


    public float GetDistance()
    {
        return transform.position.z - startingZ;
    }


    public void SetAlive(bool state)
    {
        alive = state;
    }


    public void ResetPlayer()
    {
        transform.position = startingPosition;
        currentLane = 0;
        alive = true;
    }
}