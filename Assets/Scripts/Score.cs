using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int WaveCount = 1;

    private int ScrapCount;
    private int TotalScrapCollected;

    public Text ScrapCountText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextWave()
    {
        WaveCount++;
    }

    public void SpendScrap(int cost)
    {
        ScrapCount -= cost;

        UpdateUI();
    }

    public void AddScrap(int amount)
    {
        ScrapCount += amount;
        TotalScrapCollected += amount;

        UpdateUI();
    }

    public void UpdateUI()
    {
        ScrapCountText.text = "Current Scrap: " + ScrapCount + "\nTotal Scrap Collected: " + TotalScrapCollected;
    }

}
