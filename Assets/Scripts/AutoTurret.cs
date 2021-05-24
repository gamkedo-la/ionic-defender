using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public EnemySpawn ES;

    public GameObject Bullet;

    public GameObject CurrentTarget;

    public float ShotTimer;
    float STreset;

    public float BulletSpeed;
    public float Damage;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        STreset = ShotTimer;
        timer = ShotTimer;

    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject T in ES.ActiveEnemies)
        {
            if(CurrentTarget == null)
            {
                CurrentTarget = T;
            }
            else
            {
                if(Vector3.Distance(T.transform.position, transform.position) < 
                    Vector3.Distance(CurrentTarget.transform.position, transform.position))
                {
                    CurrentTarget = T;

                }
            }
        }

        if(CurrentTarget != null)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                Shoot();
                timer = STreset;
            }
        }
        else
        {
            timer = STreset;
        }

    }

    public void Shoot()
    {
        GameObject B = Instantiate(Bullet, transform.position, Quaternion.identity);
        B.transform.LookAt(CurrentTarget.transform.position);
        B.GetComponent<AutoBullet>().damage = Damage;
        B.GetComponent<AutoBullet>().speed = BulletSpeed;
    }

}
