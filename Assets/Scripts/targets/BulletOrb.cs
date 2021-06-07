using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOrb : MonoBehaviour
{
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 Temp = transform.position;

        Temp.y -= Speed * Time.deltaTime;

        transform.position = Temp;



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Settlement" || collision.gameObject.tag == "Shield")
        {
            Destroy(gameObject);
        }
    }
}
