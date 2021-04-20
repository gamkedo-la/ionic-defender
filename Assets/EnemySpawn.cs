using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public float secondsToSpawn;
    public float spawnMinX;
    public float spawnMaxX;
    public float spawnY;
    public float enemiesPerWave;
    public float reduceSpawnTime;
    public Text waveCountText;
    public float minSpawnTime;

    private float timer = 0f;
    private int waveCount = 1;
    private int enemiesSpawned = 0;

    void Start()
    {
        waveCountText.text = "Day " + waveCount;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= secondsToSpawn)
        {
            timer = 0F;
            enemiesSpawned++;
            
            Vector3 enemyPos = new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnY, 0f);
            Instantiate(enemy, enemyPos, Quaternion.identity);
		}

        if(enemiesSpawned >= enemiesPerWave)
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
            enemiesSpawned = 0;
            waveCountText.text = "Day " + waveCount;
        } // Enf of if increase wave
    } // End of Update
}
