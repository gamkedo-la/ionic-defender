using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableEnemy : MonoBehaviour
{
    public float Health = 1;
    public int Damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        this.Health -= damage;
        Debug.Log("took damage "+damage.ToString());
        // would it make sense to decouple (extract) this logic?
        if(this.Health<=0)
        {
            die();
        }
    }

    public void die()
    {
        Debug.Log("died");
        Destroy(this.gameObject);
    }
}
