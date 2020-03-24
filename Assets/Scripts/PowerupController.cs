using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    float visibleHeightTreshold;
    public Vector2 speedMinMax;
    float speed;
    private SpriteRenderer spriteRenderer;
    public float turnAngle = 360;
    // Start is called before the first frame update
    void Start()
    {
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, DifficultyManager.getCurrentDifficulty());
        visibleHeightTreshold = -Camera.main.orthographicSize - transform.localScale.y;        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);   
        if(transform.position.y < visibleHeightTreshold){
            Destroy(gameObject);
        }
        float angle = turnAngle*Time.deltaTime;
        // transform.RotateAround(Vector3.up ,angle);
        transform.Rotate(0, angle, 0);
    }
    void OnTriggerEnter2D(Collider2D triggerCollider){
        if(triggerCollider.tag == "Player"){
            // print("Collided with player");
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
            spriteRenderer.enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            float factor;
            float rand = Random.Range(0, 2);
            if(rand < 1) {
                factor = 0.5f;
            }
            else {
                factor = 2;
            }
            int type;
            if(gameObject.name.StartsWith("ScalePowerup")){
                type = 0;
            }
            else {
                type = 1;
            }
            print("Applying powerup factor = "+ factor);
            triggerCollider.gameObject.GetComponent<PlayerController>().applyPowerup(type, factor);
            Destroy(gameObject, source.clip.length);
        }
    }
}
