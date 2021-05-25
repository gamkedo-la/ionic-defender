using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    public int StartingHP = 100;
    public int CurrentHP  = 100;

    public HpIndicator HP;

    // Start is called before the first frame update
    void Start()
    {
        HP.SetMax(StartingHP, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " collided with settlement");

        if(collision.gameObject.tag == "Enemy")
        {
            HitableEnemy hitableEnemy = collision.gameObject.GetComponent<HitableEnemy>();
            if(hitableEnemy != null)
			{
                TakeDamage(hitableEnemy.Damage);
            }
        }
    }

    public void TakeDamage(int Damage)
    {

        HP.TakeDamage(Damage);

    }
}
