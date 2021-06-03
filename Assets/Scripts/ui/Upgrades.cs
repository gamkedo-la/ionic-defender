using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    int AutoTurret = 0;
    public GameObject TurretL;
    public GameObject TurretR;

    public Score score;

    public UpgradeButtonManager BuyTurret;
    public UpgradeButtonManager UpTurret;
    public UpgradeButtonManager Shield;


    public float AdditionalTurretDamage = 2.0f;


    private void Start()
    {
        TurretL.SetActive(false);
        TurretR.SetActive(false);
    }


    public void PurchaseTurret()
    {

        if(AutoTurret >= 2)
        {
            return;
        }


        if(AutoTurret == 0)
        {
            TurretL.SetActive(true);
            AutoTurret = 1;
            UpTurret.PrereqPurchased = true;

        }
        else if(AutoTurret == 1)
        {
            TurretR.SetActive(true);
            AutoTurret = 2;

            BuyTurret.FullyPurchased = true;

        }

        score.SpendScrap(BuyTurret.Cost);


    }

    public void UpgradeTurrets()
    {
        TurretL.GetComponent<AutoTurret>().UpgradeDamage(AdditionalTurretDamage);
        TurretR.GetComponent<AutoTurret>().UpgradeDamage(AdditionalTurretDamage);

        score.SpendScrap(UpTurret.Cost);


    }


}
