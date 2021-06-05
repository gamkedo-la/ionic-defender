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

    public string HoverText;

    public GameObject tooltip;

    public Text TTText;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;

        UpdateToolTipText(HoverText);
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

    public void ShowToolTip()
    {
        tooltip.SetActive(true);
    }

    public void HideToolTip()
    {
        tooltip.SetActive(false);
    }

    public void UpdateToolTipText(string newText)
    {
        Debug.Log("updating tool tip text");
        TTText.text = newText;

    }

}
