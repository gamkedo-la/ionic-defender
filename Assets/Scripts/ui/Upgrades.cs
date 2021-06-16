using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;

public class Upgrades : MonoBehaviour
{

    int AutoTurret = 0;
    public GameObject TurretL;
    public GameObject TurretR;
    public GameObject ShieldManager;

    public LaserShooter laserShooter;


    public Score score;

    public UpgradeButtonManager BuyTurret;
    public UpgradeButtonManager UpTurret;
    public UpgradeButtonManager ShieldButtonManager;


    public float AdditionalTurretDamage = 2.0f;

    public float AdditionalLaserDamage = 15.0f;

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
            TurretL.GetComponent<AutoTurret>().UpgradeDamage(AdditionalTurretDamage);
            TurretR.GetComponent<AutoTurret>().UpgradeDamage(AdditionalTurretDamage);

            score.SpendScrap(BuyTurret.Cost);

            BuyTurret.Cost += 10;

            BuyTurret.UpdateToolTipText("Upgrade Auto Turret Damage");
        }


        if(AutoTurret == 0)
        {
            TurretL.SetActive(true);
            AutoTurret = 1;
            //UpTurret.PrereqPurchased = true;

            score.SpendScrap(BuyTurret.Cost);
            BuyTurret.UpdateToolTipText("Add a Second Auto Turret");


        }
        else if(AutoTurret == 1)
        {
            TurretR.SetActive(true);
            AutoTurret = 2;

            score.SpendScrap(BuyTurret.Cost);

            //BuyTurret.FullyPurchased = true;

            BuyTurret.Cost = 40;

            BuyTurret.UpdateToolTipText("Upgrade Auto Turret Damage");

        }

       


    }

    public void UpgradeTurrets()
    {
        laserShooter.laserDamage += AdditionalLaserDamage;

        score.SpendScrap(UpTurret.Cost);

        UpTurret.Cost += 30;

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
