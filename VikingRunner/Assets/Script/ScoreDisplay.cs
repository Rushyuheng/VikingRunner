using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text coinText;
    public Text TimeText;



    // Start is called before the first frame update
    void Start()
    {
        TimeText.text = PlayerPrefs.GetString("time");
        coinText.text = PlayerPrefs.GetString("coin");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
