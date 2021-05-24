using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBullet : MonoBehaviour
{
    public float speed;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Temp = transform.position;

        Temp += transform.forward * speed * Time.deltaTime;

        transform.position = Temp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HitableEnemy>().takeDamage(damage);
            Destroy(gameObject);
        }
    }

}
