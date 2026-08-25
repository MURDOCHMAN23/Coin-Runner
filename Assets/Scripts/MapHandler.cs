using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject[] obstacles;

    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private GameObject groundPrefab;

    [SerializeField]
    private Transform mapObjects;

    [Header("Generation")]
    [SerializeField]
    private float spawnDistance = 100f;

    [SerializeField]
    private float segmentLength = 20f;

    [Header("Cleanup")]
    [SerializeField]
    private float cleanupDistance = 30f;

    private readonly List<GameObject> spawnedObjects = new List<GameObject>();

    private float nextSpawnZ;
    private int segmentCount = 0;


    private void Start()
    {
        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();

        nextSpawnZ = transform.position.z;

        GenerateUntilAhead();
    }


    private void Update()
    {
        if (gameManager == null || gameManager.GameOverState)
            return;

        GenerateUntilAhead();
        CleanupBehind();
    }


    private void GenerateUntilAhead()
    {
        float playerZ = gameManager.PlayerDistance;

        while (nextSpawnZ < playerZ + spawnDistance)
        {
            GenerateSegment(nextSpawnZ);
            nextSpawnZ += segmentLength;
        }
    }


    private void GenerateSegment(float zPosition)
    {
        GenerateGround(zPosition);

        if (segmentCount == 0)
        {
            segmentCount++;
            return;
        }

        int objectCount = Random.Range(1, 3);

        List<int> availableLanes = new List<int> { -1, 0, 1 };

        bool coinSpawned = false;

        for (int i = 0; i < objectCount; i++)
        {
            int laneIndex = Random.Range(0, availableLanes.Count);
            int lane = availableLanes[laneIndex];

            availableLanes.RemoveAt(laneIndex);

            float laneX = lane * gameManager.LaneSpacing;

            bool spawnCoin = !coinSpawned && Random.value < 0.5f;

            if (spawnCoin && coinPrefab != null)
            {
                GenerateCoin(laneX, zPosition);
                coinSpawned = true;
            }
            else if (obstacles.Length > 0)
            {
                GenerateObstacle(laneX, zPosition);
            }
        }

        segmentCount++;
    }


    private void GenerateGround(float zPosition)
    {
        if (groundPrefab == null)
            return;

        GameObject ground = Instantiate(
            groundPrefab,
            new Vector3(0f, 0f, zPosition),
            Quaternion.identity,
            mapObjects
        );

        spawnedObjects.Add(ground);
    }


    private void GenerateObstacle(float xPosition, float zPosition)
    {
        GameObject obstaclePrefab =
            obstacles[Random.Range(0, obstacles.Length)];

        GameObject obstacle = Instantiate(
            obstaclePrefab,
            new Vector3(xPosition, 0f, zPosition),
            obstaclePrefab.transform.rotation,
            mapObjects
        );

        spawnedObjects.Add(obstacle);
    }


    private void GenerateCoin(float xPosition, float zPosition)
    {
        if (coinPrefab == null)
            return;

        GameObject coin = Instantiate(
            coinPrefab,
            new Vector3(xPosition, 1f, zPosition),
            coinPrefab.transform.rotation,
            mapObjects
        );

        spawnedObjects.Add(coin);
    }


    private void CleanupBehind()
    {
        float playerZ = gameManager.PlayerDistance;
        float cleanupZ = playerZ - cleanupDistance;

        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            GameObject spawnedObject = spawnedObjects[i];

            if (spawnedObject == null)
            {
                spawnedObjects.RemoveAt(i);
                continue;
            }

            if (spawnedObject.transform.position.z < cleanupZ)
            {
                Destroy(spawnedObject);
                spawnedObjects.RemoveAt(i);
            }
        }
    }


    public void ResetGeneration()
    {
        for (int i = mapObjects.childCount - 1; i >= 0; i--)
        {
            Destroy(mapObjects.GetChild(i).gameObject);
        }

        spawnedObjects.Clear();

        segmentCount = 0;
        nextSpawnZ = transform.position.z;
    }
}