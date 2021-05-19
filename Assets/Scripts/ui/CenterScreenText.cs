using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterScreenText : MonoBehaviour
{

    public Text text;

    float timer;

    string previousText;

    bool empty;

    // Start is called before the first frame update
    void Start()
    {
        text.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                previousText = text.text;
                text.text = " ";
                empty = true;

            }
        }
    }

    public void DisplayText(string TXT, float duration)
    {
        if(empty == false)
        {
            previousText = text.text;
        }

        text.text = TXT;

        timer = duration;

    }
}
