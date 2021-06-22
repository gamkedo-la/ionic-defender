using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float HP;

    //[HideInInspector]
    public float MaxHP;

    public float RegenRate;
    public ShieldManager manager;

    public float DamagePerHit = 1;


    // Start is called before the first frame update
    void Start()
    {
        MaxHP = HP;
    }

    // Update is called once per frame
    void Update()
    {

        if (HP < MaxHP)
        {
            TakeDamage(-RegenRate * Time.deltaTime);
        }
    }


    public void TakeDamage(float Damage)
    {
        HP -= Damage;

        if (HP <= 0)
        {
            HP = 0;
            manager.ShieldRespawn();
        }
    }

    public void ResetShield()
    {
        HP = MaxHP;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if (collision.gameObject.GetComponent<HitableEnemy>() != null)
        //{
        //    collision.gameObject.GetComponent<HitableEnemy>().takeDamage(1);
        //}

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HitableEnemy hitableEnemy = collision.gameObject.GetComponent<HitableEnemy>();
        if (hitableEnemy != null)
        {
            hitableEnemy.takeDamage(DamagePerHit);
            TakeDamage(hitableEnemy.Damage);
            Debug.Log("hit enemy took damage");
        }
    }

}
