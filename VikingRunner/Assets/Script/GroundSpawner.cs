using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject straightRoad;
    public GameObject leftRoad;
    public GameObject rightRoad;
    public GameObject obstacle;
    public GameObject hole;
    Vector3 nextSpawnPoint;
    private int previousIndex = 0;
    private Quaternion previousRotation;
    private GameObject[] groundArray = new GameObject[5];

    public void SpawnRoad() {
        int nextIndex = Random.Range(0,5);

        //not allow continuous turns
        if (previousIndex == 1 && nextIndex == 1)
        {
            nextIndex = 0;
        }
        else if (previousIndex == 2 && nextIndex == 2)
        {
            nextIndex = 0;
        }

        GameObject nextGround = groundArray[nextIndex];
        Quaternion nextRotation =  previousRotation;

        if (previousIndex == 1)
        {
            nextRotation *= Quaternion.Euler(0, -90, 0);
        }
        else if (previousIndex == 2) {
            nextRotation *= Quaternion.Euler(0, 90, 0);
        }

        GameObject temp = Instantiate(nextGround, nextSpawnPoint, nextRotation);

        //update next position and rotataion
        nextSpawnPoint = temp.transform.GetChild(temp.transform.childCount - 1).transform.position;
        previousRotation = nextRotation;
        previousIndex = nextIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        // array intialize
        groundArray[0] = straightRoad;
        groundArray[1] = leftRoad;
        groundArray[2] = rightRoad;
        groundArray[3] = obstacle;
        groundArray[4] = hole;

        //position & rotation initialize
        nextSpawnPoint = transform.position;
        previousRotation = transform.rotation;

        //spawn first three road
        SpawnRoad();
        SpawnRoad();
        SpawnRoad();
       
    }


}
