using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 1.0f;

    public Transform Target;

    private HitableEnemy hitableEnemy;
    private Renderer myMaterial;

    // Start is called before the first frame update
    void Start()
    {
        hitableEnemy = GetComponent<HitableEnemy>();
        myMaterial = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //1f - hitableEnemy.Health / hitableEnemy.maxHealth,
        transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        Color myColor = new Color(1f - hitableEnemy.Health / hitableEnemy.maxHealth, 0f, 0f, 1f);
        myMaterial.material.SetColor("_EmissionColor", myColor);
        myMaterial.material.EnableKeyword("_EMISSION");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Settlement")
        {
            hitableEnemy.die(false);
        }
    }
}
