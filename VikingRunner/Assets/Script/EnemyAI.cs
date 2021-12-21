using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public Text coinText;
    public Text TimeText;
    int movingSpeed = 3;
    int maxDistance = 10;

    public void GameOver() {
        PlayerPrefs.SetString("coin", coinText.text);
        PlayerPrefs.SetString("time", TimeText.text);
        SceneManager.LoadScene(2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Viking"))
        {
            GameOver();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);

        if (transform.localPosition != player.transform.localPosition)
        {
            transform.localPosition += transform.forward * movingSpeed * Time.deltaTime;
        }
        //teleport enemy if too far
        if (Vector3.Distance(transform.localPosition, player.transform.localPosition) >= maxDistance) {
            transform.localPosition = player.transform.localPosition - (transform.rotation * Vector3.forward * 5);
        }
    }
}
