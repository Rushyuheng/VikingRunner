using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDDisplay : MonoBehaviour
{
    private int coinCollected = 0;
    public Text coinText;
    public Text TimeText;

    public void AddCoinCollected() {
        coinCollected++;
        coinText.text = coinCollected.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeText.text = Time.timeSinceLevelLoad.ToString();
    }
}
