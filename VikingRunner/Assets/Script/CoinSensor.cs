using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSensor : MonoBehaviour
{
    public HUDDisplay hudDisplay;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Viking"))
        {
            hudDisplay.AddCoinCollected();
            Destroy(gameObject, 0.01f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hudDisplay = GameObject.FindObjectOfType<HUDDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
