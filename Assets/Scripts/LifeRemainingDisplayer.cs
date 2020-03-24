using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRemainingDisplayer : MonoBehaviour
{
    private List<GameObject> powerups;
    private float startPosition;
    private float gap = 0.1f;
    public GameObject livesObject;
    // Start is called before the first frame update
    void Start()
    {
        float screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        startPosition = -screenHalfWidth + livesObject.transform.localScale.x / 2;
        powerups = new List<GameObject>();
    }

    // Update is called once per frame
    public void addLives() {
        Vector2 spawnPosition = new Vector2(startPosition, Camera.main.orthographicSize - livesObject.transform.localScale.y);
        GameObject life = Instantiate(livesObject, spawnPosition, Quaternion.identity);
        // print(life.tag );
        startPosition +=  life.transform.localScale.x+ gap;
        powerups.Add(life);
    }
    public bool removeLife() {
        if(powerups.Count > 0) {
            GameObject lastPowerup = powerups[powerups.Count - 1];
            startPosition -= lastPowerup.transform.localScale.x + gap;
            powerups.Remove(lastPowerup);
            Destroy(lastPowerup);
            return true;
        }
        return false;
    }
}