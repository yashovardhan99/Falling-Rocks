using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverScript : MonoBehaviour
{

    public GameObject gameOverScreen;
    public Text secondsSurvivedUI;
    public AudioClip gameOverAudio;
    public AudioSource backgroundMusicSource;
    private AudioSource source;
    public event System.Action GameOverBroadcast;
    bool gameOver;

    void Start() {
        FindObjectOfType<PlayerController>().OnPlayerDeath += OnGameOver;
    }
    // Update is called once per frame
    void Update()
    {
        if(gameOver){
            if(Input.GetKeyDown(KeyCode.Space)){
                SceneManager.LoadScene("Game Scene");

            }
            if(Input.GetKeyDown(KeyCode.Escape)) {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #endif
                Application.Quit();
            }
        }
        
    }
    void OnGameOver(){
        source = GetComponent<AudioSource>();
        // print(source.isActiveAndEnabled);
        source.volume = 1;
        source.PlayOneShot(gameOverAudio, 1.0f);
        // print(source.isPlaying);
            
        if(FindObjectOfType<LifeRemainingDisplayer>().removeLife()){
            gameOver = false;
        }
        else{
            GameOverBroadcast();
            Destroy(FindObjectOfType<PlayerController>().gameObject);
            backgroundMusicSource.Stop();
            gameOverScreen.SetActive(true);
            secondsSurvivedUI.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
            gameOver = true;
            StartCoroutine(playBackgroundMusic(20));
        }
    }
    IEnumerator playBackgroundMusic(float time) {
        yield return new WaitForSeconds(time);
        source.priority = 160;
        source.volume = 0.1f;
        source.Play();
    }
}
