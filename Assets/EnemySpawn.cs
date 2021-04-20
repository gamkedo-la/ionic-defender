using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public float secondsToSpawn;
    public float spawnMinX;
    public float spawnMaxX;
    public float spawnY;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= secondsToSpawn)
        {
            Vector3 enemyPos = new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnY, 0f);
            timer = 0F;
            Instantiate(enemy, enemyPos, Quaternion.identity);
		}
    }
}
