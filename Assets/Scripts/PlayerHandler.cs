using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float forwardSpeed = 5f;

    [SerializeField]
    private float laneChangeSpeed = 10f;

    [Header("State")]
    [SerializeField]
    private bool alive = true;

    [SerializeField]
    private int currentLane = 0;

    private float laneSpacing;
    private float startingZ;


    public void Initialize(float spacing)
    {
        laneSpacing = spacing;
        currentLane = 0;
        alive = true;

        startingZ = transform.position.z;
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
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
    }


    private void MoveTowardLane()
    {
        float targetX = currentLane * laneSpacing;

        Vector3 position = transform.position;
        position.x = Mathf.MoveTowards(
            position.x,
            targetX,
            laneChangeSpeed * Time.deltaTime
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
}