using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    int AutoTurret = 0;
    public GameObject TurretL;
    public GameObject TurretR;
    public GameObject ShieldManager;


    public Score score;

    public UpgradeButtonManager BuyTurret;
    public UpgradeButtonManager UpTurret;
    public UpgradeButtonManager ShieldButtonManager;


    public float AdditionalTurretDamage = 2.0f;

    bool ShieldPurchased;

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

            BuyTurret.UpdateToolTipText("Add a second auto turret");

        }
        else if(AutoTurret == 1)
        {
            TurretR.SetActive(true);
            AutoTurret = 2;

            BuyTurret.FullyPurchased = true;
            BuyTurret.UpdateToolTipText("Maximum turrets purchased");

        }

        score.SpendScrap(BuyTurret.Cost);


    }

    public void UpgradeTurrets()
    {
        TurretL.GetComponent<AutoTurret>().UpgradeDamage(AdditionalTurretDamage);
        TurretR.GetComponent<AutoTurret>().UpgradeDamage(AdditionalTurretDamage);

        score.SpendScrap(UpTurret.Cost);

        UpTurret.Cost += 10;

        UpTurret.UpdateToolTipText(UpTurret.HoverText);

    }

    public void PurchaseShield()
    {
        if (ShieldPurchased == false)
        {

            ShieldManager.GetComponent<ShieldManager>().Purchased = true;
            ShieldManager.GetComponent<ShieldManager>().RespawnTime = .1f;
            //ShieldManager.GetComponent<ShieldManager>().shieldDown = false;

            //ShieldButtonManager.FullyPurchased = true;

            //ShieldButtonManager.Cost += 20;

            ShieldButtonManager.UpdateToolTipText("Upgrade Shield Strength");

            score.SpendScrap(ShieldButtonManager.Cost);

            ShieldPurchased = true;
        }
        else
        {
            ShieldManager.GetComponent<ShieldManager>().Upgrade();
            ShieldButtonManager.Cost += 20;
        }
    }

}
