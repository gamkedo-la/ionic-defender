using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpIndicator : MonoBehaviour
{
    public GameObject gameOverDialog;

    public GameObject[] playerObjectsToRemoveWhenDying;

    public Slider Fill;
    public GameObject TargetVisualizer;
    private float lastHitTime;

    public float CurrentHP;
    public float TargetHP;

    public float DrainSpeed;
    public float DrainSpeedIfHit;
    public float DurationHigherDrainAfterHit;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D)) {
            Debug.Log("instant death test");
            CurrentHP = -0.1f;
        }

        if (CurrentHP > TargetHP)
        {
            CurrentHP -= (GetDrain() * Time.deltaTime);

            if(CurrentHP < TargetHP)
            {
                CurrentHP = TargetHP;
            }

            Fill.value = CurrentHP;
        }

        if (TargetVisualizer != null)
        {
            Vector3 scale = TargetVisualizer.transform.localScale;
            scale.y = Math.Max(0,TargetHP / Fill.maxValue);
            TargetVisualizer.transform.localScale = scale;
        }

        if(CurrentHP <= 0 && gameOverDialog != null)
        {
            //Game Over
            gameOverDialog.SetActive(true);
            for(int i=0;i< playerObjectsToRemoveWhenDying.Length;i++) {
                playerObjectsToRemoveWhenDying[i].SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    private float GetDrain()
    {
        if (Time.time - lastHitTime <= DurationHigherDrainAfterHit)
        {
            return DrainSpeedIfHit;
        }
        else
        {
            return DrainSpeed;
        }
    }

    public void TakeDamage(float Damage)
    {
        lastHitTime = Time.time;
        TargetHP -= Damage;

        if(TargetHP <= 0)
        {
            TargetHP = 0;
        }
    }

    public void SetMax(float Max, bool reset)
    {
        Fill.maxValue = Max;

        

        if (reset == true)
        {
            Fill.value = Max;

            CurrentHP = Max;
            TargetHP = Max;
        }

    }

    public void NextWave(float BonusHP)
    {

        CurrentHP += BonusHP;
        if (CurrentHP > Fill.maxValue)
            CurrentHP = Fill.maxValue;
        TargetHP = CurrentHP;
    }



}
