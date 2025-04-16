using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Ground;
    private int numberOfWaypoints; // Number of Waypoints in the path
    public List<GameObject> pathWaypoints = new List<GameObject>(); // List to store the path Waypoints

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
        float zPlusRange = 50;

        Vector3 lastVertex = pathWaypoints[pathWaypoints.Count - 1].transform.position;
        Vector3 firstVertex = pathWaypoints[0].transform.position;

        GameObject ground = Instantiate(Ground, new Vector3((xRange - 10) / 2, -0.1f, 0f), Quaternion.identity);
        ground.transform.localScale = new Vector3(xRange, 0.1f, 2 * zPlusRange);
    }
}
