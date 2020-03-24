using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{

    float screenHalfWidth;
    public float speed = 7;
    public GameObject explosionPrefab;

    public event System.Action OnPlayerDeath;
    // Start is called before the first frame update
    void Start() {
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidth = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }

    // Update is called once per frame
    void Update() {
        float input = Input.GetAxisRaw("Horizontal");
        float velocity = input * speed;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);

        if(transform.position.x < -screenHalfWidth){
            transform.position = new Vector2(screenHalfWidth, transform.position.y);
        }
        if(transform.position.x > screenHalfWidth){
            transform.position = new Vector2(- screenHalfWidth, transform.position.y);
        }
    }
    void OnTriggerEnter2D(Collider2D triggerCollider){
        if(triggerCollider.tag == "Falling Rock"){
            if(OnPlayerDeath!=null){
                OnPlayerDeath();
            }
            GameObject explosion = Instantiate(explosionPrefab, triggerCollider.gameObject.transform.position, triggerCollider.gameObject.transform.rotation);
            explosion.transform.localScale = 0.2f * triggerCollider.gameObject.transform.localScale;
            Destroy(triggerCollider.gameObject);
        }
    }
    public void applyPowerup(int powerUpType, float factor) {
        float timeout = 10;
        switch (powerUpType) {
            case 0: StartCoroutine(applyScale(factor, timeout));
                break;
            case 1: StartCoroutine(changeSpeed(factor, timeout));
                break;
        }
    }
    public IEnumerator applyScale(float factor, float timeout){
        transform.localScale = new Vector3(transform.localScale.x * factor, transform.localScale.y, transform.localScale.z);
        yield return new WaitForSeconds(timeout);
        transform.localScale = new Vector3(transform.localScale.x / factor, transform.localScale.y, transform.localScale.z);
        GetComponent<AudioSource>().Play();
    }
    public IEnumerator changeSpeed(float factor, float timeout){
        speed = speed*factor;
        yield return new WaitForSeconds(timeout);
        speed = speed/factor;
        GetComponent<AudioSource>().Play();
    }
}
