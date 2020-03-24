using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeController : MonoBehaviour
{
    Text secondsText;
    // Start is called before the first frame update
    void Start()
    {
        secondsText = gameObject.GetComponent<Text>();
        FindObjectOfType<GameOverScript>().GameOverBroadcast += OnGameOver;
        InvokeRepeating("updateText", 0, 1);
        secondsText.text = "0";
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.T)){
            GetComponent<Text>().enabled = !GetComponent<Text>().enabled;
        }
    }

    // Update is called once per frame
    void updateText(){
        secondsText.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
    }
    void OnGameOver(){
        Destroy(gameObject);
    }
}
