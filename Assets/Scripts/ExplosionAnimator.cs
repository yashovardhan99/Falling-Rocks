using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimator : MonoBehaviour
{
    public Sprite[] frameArray;
    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;
    [SerializeField] private float frameRate = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>=frameRate){
            timer-=frameRate;
            currentFrame++;
            if(currentFrame >= frameArray.Length) {
                Destroy(gameObject);
            }
            else {
                spriteRenderer.sprite = frameArray[currentFrame];
            }
        }
    }
}
