using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        GameObject concreteCoin = Instantiate(coin);
        concreteCoin.transform.parent = transform.GetChild(Random.Range(0, transform.childCount));
        concreteCoin.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
