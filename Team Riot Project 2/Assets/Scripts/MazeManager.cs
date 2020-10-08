using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    //list of maze end locations
    public List<Transform> endLocations;

    //maze end prefab
    public GameObject endPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //get a random number and spawn the endpoint of the maze
        int mazeNum = Random.Range(0, 3);
        GameObject mazeEnd = Instantiate(endPrefab);
        mazeEnd.transform.position = endLocations[mazeNum].position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
