using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Ground;
    public GameObject emptyObject;
    private int numberOfWaypoints; // Number of Waypoints in the path
    public List<GameObject> pathWaypoints = new List<GameObject>(); // List to store the path Waypoints
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); // List to save the path's location

    void Start()
    {
        StartCoroutine(WaitForPathGenerator());
    }

    IEnumerator WaitForPathGenerator()
    {
        while (PathGenerator.Instance == null || PathGenerator.Instance.pathWaypoints.Count == 0)
        {
            //Debug.Log("Waiting for PathGenerator to populate pathVertices...");
            yield return null; // Waits until next frame
        }

        // Grab the number of vertices from PathGenerator
        numberOfWaypoints = PathGenerator.Instance.numberOfWaypoints;
        pathWaypoints = PathGenerator.Instance.pathWaypoints;

        //Debug.Log("pathVertices populated. Proceeding with map generation.");
        GenerateMap();
    }

    void GenerateMap()
    {
        float xRange = numberOfWaypoints * 10 + 10;
        float zMinusRange = -50;
        float zPlusRange = 50;

        Vector3 lastVertex = pathWaypoints[pathWaypoints.Count - 1].transform.position;
        Vector3 firstVertex = pathWaypoints[0].transform.position;

        GameObject ground = Instantiate(Ground, new Vector3((xRange - 10) / 2, -0.1f, 0f), Quaternion.identity);
        ground.transform.localScale = new Vector3(xRange, 0.1f, 2 * zPlusRange);

        // Llenamos occupiedPositions con las posiciones de los caminos
        FillOccupiedPositions();

        for (float i = 5; i < xRange - 20; i += 5)
        {
            for (float j = zMinusRange + 10; j <= zPlusRange - 10; j += 5)
            {
                Vector3 position = new Vector3(i, 0f, j);

                if (!occupiedPositions.Contains(position))
                {
                    GameObject placer = Instantiate(emptyObject, position, Quaternion.identity);
                    placer.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }
    void FillOccupiedPositions()
    {
        occupiedPositions.Clear();

        for (int i = 0; i < pathWaypoints.Count - 1; i++)
        {
            Vector3 start = pathWaypoints[i].transform.position;
            Vector3 end = pathWaypoints[i + 1].transform.position;

            Vector3 direction = (end - start).normalized;
            float distance = Vector3.Distance(start, end);

            // Agregamos los puntos intermedios cada 5 unidades
            for (float d = 0; d <= distance; d += 5)
            {
                Vector3 position = start + direction * d;
                occupiedPositions.Add(position);
            }
        }
    }
}
