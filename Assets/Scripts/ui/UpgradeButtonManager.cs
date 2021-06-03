using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonManager : MonoBehaviour
{
    Button button;

    public bool PrereqPurchased = true;

    public bool FullyPurchased = false;

    public int Cost;

    public Score score;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(FullyPurchased == true)
        {
            button.interactable = false;
        }
        
        if(button.interactable == false && score.GetScrap() >= Cost && PrereqPurchased == true && FullyPurchased == false)
        {
            button.interactable = true;
        }

        else if(button.interactable == true && score.GetScrap() < Cost)
        {
            button.interactable = false;
        }


    }

}
