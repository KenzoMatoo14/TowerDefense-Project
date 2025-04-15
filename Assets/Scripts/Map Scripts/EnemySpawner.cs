using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner spawner;

    public GameObject[] EnemyPrefabs; // Array of different immigrant prefabs
    public int NumberEnemy = 10;
    private int spawnedCount = 0; // Tracks how many have been spawned
    private float currentTime; // Time to next spawn

    void Awake()
    {
        if (spawner == null)
            spawner = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentTime = 5f;
    }

    public int getNumberInmigrants()
    {
        return NumberEnemy;
    }

    void Update()
    {
        if (spawnedCount >= NumberEnemy)
            return;

        // Countdown the timer
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            if (spawnedCount >= NumberEnemy) return;

            // Ensure PathGenerator exists and has path vertices
            if (PathGenerator.Instance == null || PathGenerator.Instance.pathWaypoints.Count == 0)
            {
                Debug.LogWarning("PathGenerator is not ready or has no vertices!");
                return;
            }

            // Get the first vertex position
            Vector3 pathPos = PathGenerator.Instance.pathWaypoints[0].transform.position;
            Vector3 spawnPosition = new Vector3(pathPos.x, 3f, pathPos.z);

            // Randomly select an Enemy type
            int randomIndex = Random.Range(0, EnemyPrefabs.Length);
            GameObject selectedPrefab = EnemyPrefabs[randomIndex];

            // Spawn the selected Enemy
            GameObject newInmigrant = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            //Debug.Log("Enemy spawned after " + currentTime + " seconds");

            spawnedCount++; // Increase the count

            currentTime = 2f;
        }
    }
}
