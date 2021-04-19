using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{

    public Slider Fill;


    public void SetFill(float I)
    {
        Fill.value = I;

    }

    public void SetMax(float J, bool reset)
    {
        Fill.maxValue = J;

        if (reset == true)
        {
            SetFill(J);
        }

    }


}