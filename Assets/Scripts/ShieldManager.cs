using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public Shield shield;

    public float RespawnTime;
    float RTreset;

    public bool shieldDown = true;

    public bool Purchased;

    // Start is called before the first frame update
    void Start()
    {
        RTreset = RespawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(shieldDown == true && Purchased == true)
        {
            RespawnTime -= Time.deltaTime;

            if(RespawnTime <= 0)
            {
                shield.gameObject.SetActive(true);
                shield.ResetShield();
                shieldDown = false;
            }
        }


    }

    public void ShieldRespawn()
    {
        shield.gameObject.SetActive(false);
        shieldDown = true;

    }

}
