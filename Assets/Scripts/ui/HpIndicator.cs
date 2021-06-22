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

    public float CurrentHP;
    public float TargetHP;

    public float DrainSpeed;

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
            CurrentHP -= (DrainSpeed * Time.deltaTime);

            if(CurrentHP < TargetHP)
            {
                CurrentHP = TargetHP;
            }            

            Fill.value = CurrentHP;
        }
        Vector3 scale = TargetVisualizer.transform.localScale;
        scale.y = TargetHP / Fill.maxValue;
        TargetVisualizer.transform.localScale = scale;

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

    public void TakeDamage(float Damage)
    {
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
        TargetHP = CurrentHP;
    }



}
