using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fallingRockPrefab;
    public Vector2 secondsBetweenSpawnMinMax;
    public Vector2 spawnSizeMinMax;
    public float spawnAngleMax;
    float nextSpawnTime;
    Vector2 screenHalfSize;
    public GameObject[] powerups;
    public Vector2 powerUpDelay;
    private float nextPowerUpTime;
    // Start is called before the first frame update
    void Start()
    {
        nextPowerUpTime = Time.time + RandomFromDistribution.RandomRangeNormalDistribution(powerUpDelay.x, powerUpDelay.y, RandomFromDistribution.ConfidenceLevel_e._95);
        screenHalfSize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSpawnTime){
            float secondsBetweenSpawn = Mathf.Lerp(secondsBetweenSpawnMinMax.y, secondsBetweenSpawnMinMax.x, DifficultyManager.getCurrentDifficulty());
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            Vector2 spawnPosition = new Vector2(Random.Range(-screenHalfSize.x, screenHalfSize.x), screenHalfSize.y + spawnSize*fallingRockPrefab.transform.localScale.y);
            GameObject newRock = (GameObject) Instantiate (fallingRockPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            newRock.transform.localScale = Vector2.one * spawnSize;
            nextSpawnTime = Time.time + secondsBetweenSpawn;
        }
        if(Time.time >= nextPowerUpTime){
            float spawnAngle = Random.Range(-spawnAngleMax/2, spawnAngleMax/2);
            GameObject powerupPrefab = powerups[Mathf.CeilToInt(Random.Range(0, powerups.Length))];
            Vector2 spawnPosition = new Vector2(Random.Range(
                -screenHalfSize.x + powerupPrefab.transform.localScale.x, 
                screenHalfSize.x - powerupPrefab.transform.localScale.x),
                screenHalfSize.y + powerupPrefab.transform.localScale.y);
            Instantiate (powerupPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
            nextPowerUpTime = Time.time + RandomFromDistribution.RandomRangeNormalDistribution(powerUpDelay.x, powerUpDelay.y, RandomFromDistribution.ConfidenceLevel_e._95);
        }
    }
}
