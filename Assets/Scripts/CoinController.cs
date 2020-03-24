using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public Sprite[] frameArray;
    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;
    // public event System.Action OnCoinCollected;

    [SerializeField] private float frameRate = 0.1f;

    float visibleHeightTreshold;
    public Vector2 speedMinMax;
    float speed;
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
        if(frameArray.Length == 0){
            print("Empty frames!");
            return;
        }
        timer+=Time.deltaTime;
        if(timer>=frameRate){
            timer-=frameRate;
            currentFrame = (currentFrame+1) % frameArray.Length;
            spriteRenderer.sprite = frameArray[currentFrame];
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime);   
        if(transform.position.y < visibleHeightTreshold){
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D triggerCollider){
        if(triggerCollider.tag == "Player"){
            // print("Collided with player");
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
            spriteRenderer.enabled = false;
            FindObjectOfType<LifeRemainingDisplayer>().addLives();
            Destroy(gameObject, source.clip.length);
        }
    }
}
