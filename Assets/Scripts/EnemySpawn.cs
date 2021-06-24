using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public float secondsToSpawn;
    public float spawnMinX;
    public float spawnMaxX;
    public float spawnY;
    public float enemiesPerWave;
    public float reduceSpawnTime;
    public Text waveCountText;
    public float minSpawnTime;

    public float timer = 0f;
    private int waveCount = 1;
    private int enemiesSpawned = 0;

    public HpIndicator PlayerHP;

    public Score score;

    public GameObject ScrapText;

    public Transform[] EnemyTargetPoints;

    public float TimeBetweenWaves;

    public CenterScreenText screenText;

    [HideInInspector]
    public List<GameObject> ActiveEnemies = new List<GameObject>();

    void Start()
    {
        waveCountText.text = "Day " + waveCount;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= secondsToSpawn && enemiesSpawned < enemiesPerWave)
        {
            timer = 0F;
            enemiesSpawned++;
            
            Vector3 enemyPos = new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnY, 0f);
            int index = Random.Range(0, enemies.Length);
            GameObject E = Instantiate(enemies[index], enemyPos, Quaternion.identity);

            if(E.GetComponent<Asteroid>() != null)
            {
                E.GetComponent<Asteroid>().Target = EnemyTargetPoints[Random.Range(0, EnemyTargetPoints.Length)];
            }
            if(E.GetComponent<HitableEnemy>() != null)
            {
                E.GetComponent<HitableEnemy>().score = score;
                E.GetComponent<HitableEnemy>().ScrapText = ScrapText;
                E.GetComponent<HitableEnemy>().ES = this;
            }

            ActiveEnemies.Add(E);

		}

        if(enemiesSpawned >= enemiesPerWave && ActiveEnemies.Count == 0)
		{
            if(secondsToSpawn - reduceSpawnTime > minSpawnTime)  // Avoid negative spawn time
            {
                secondsToSpawn -= reduceSpawnTime;

                // Unity was doing weird things with the subtraction, 
                // so this next bit will imit the value to 1 decimal place
                secondsToSpawn = Mathf.Round(secondsToSpawn * 10) / 10; 
            }
            else
			{
                secondsToSpawn = minSpawnTime;
            }
            waveCount++;
            score.NextWave();
            enemiesSpawned = 0;
            waveCountText.text = "Day " + waveCount;

            timer = -TimeBetweenWaves;

            screenText.DisplayText("Wave " + (waveCount -1) + " Cleared", TimeBetweenWaves);
            SoundFXManager.PlayOneShot(SoundFxKey.WaveCleared);

            PlayerHP.NextWave(10); // Edit this number to change bonus HP given per wave

        } // Enf of if increase wave
    } // End of Update


    public void EnemyDeath(GameObject E)
    {
        ActiveEnemies.Remove(E);
    }

}
