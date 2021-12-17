using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Ground : MonoBehaviour
{
    GroundSpawner groundSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Viking"))
        {
            groundSpawner.SpawnRoad();
            Destroy(gameObject, 10);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
