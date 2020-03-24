using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockController : MonoBehaviour
{

    float visibleHeightTreshold;
    public Vector2 speedMinMax;
    float speed;
    // Start is called before the first frame update
    void Start() {
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, DifficultyManager.getCurrentDifficulty());
        visibleHeightTreshold = -Camera.main.orthographicSize - transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);   
        if(transform.position.y < visibleHeightTreshold){
            Destroy(gameObject);
        }
    }
}
