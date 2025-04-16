using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PathGenerator : MonoBehaviour
{
    public static PathGenerator Instance; // Singleton instance

    public GameObject Path;
    public GameObject PathWaypoint;
    public int numberOfWaypoints = 8; // Number of vertices in the path
    private float spacing = 5; // Distance between each vertex
    public List<GameObject> pathWaypoints = new List<GameObject>(); // List to store the path vertices

    void Awake()
    {
        // Ensure there is only one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

        if (numberOfWaypoints % 2 != 0)
        {
            numberOfWaypoints += 1;
        }

        GeneratePath();
    }

    void GeneratePath()
    {
        Vector3 currentPosition = Vector3.zero;
        Vector3 newPosition = Vector3.zero;
        Vector3 currentPathPosition = Vector3.zero;
        Vector3 newPathPosition = Vector3.zero;

        for (int i = 0; i < numberOfWaypoints; i++)
        {
            GameObject waypoint = Instantiate(PathWaypoint, currentPosition, Quaternion.identity);

            pathWaypoints.Add(waypoint);

            float distanceX = i * spacing * 2;
            float distanceZ = Random.Range(-5, 6) * 5; // Random multiple of 5 between -25 and 25

            if (i % 2 == 0 && i != 0) // Si es par
            {
                newPosition = new Vector3(currentPosition.x, 0, distanceZ);
            }
            else // Si es impar
            {
                newPosition = new Vector3(distanceX, 0, currentPosition.z);
            }


            waypoint.transform.position = newPosition;
            waypoint.transform.parent = transform;
           
            GeneratePathBetween(currentPosition, newPosition);
            

            currentPosition = newPosition; // Actualizar la posición actual

        }

    }
    void GeneratePathBetween(Vector3 start, Vector3 end)
    {
        //GameObject firstpath = Instantiate(Path, start, Quaternion.identity);
        Vector3 direction = (end - start).normalized; // Dirección hacia el siguiente vértice
        float distance = Vector3.Distance(start, end); // Distancia total

        for (float d = 0; d <= distance; d += 5) // Coloca Path en intervalos de 5
        {
            Vector3 position = start + direction * d;
            GameObject path = Instantiate(Path, position, Quaternion.identity);
            path.transform.parent = transform;
        }
    }

}