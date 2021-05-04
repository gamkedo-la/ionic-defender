using System.Collections;
using System.Collections.Generic;
using player;
using UnityEngine;
using UnityEngine.UI;

public class LaserController : MonoBehaviour
{
    public LaserShooter laser;

    public Transform DebugTarget;

    public float heatCoolDownSeconds;
    public float maxHeatSeconds;
    private float currentHeat;
    private bool overheat = false;

    public Slider heatSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void Update()
	{
        heatSlider.value = currentHeat / maxHeatSeconds * 100;

		if(currentHeat >= maxHeatSeconds)
		{
            overheat = true;
        }

        if(overheat)
		{
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			for (int i = 0; i < enemies.Length; i++)
			{
                Destroy(enemies[i]);
			}
            currentHeat -= Time.deltaTime;
		}

        if(currentHeat <= 0)
		{
            currentHeat = 0;
            overheat = false;
        }
	}

	void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !overheat)
        {
            // Heat increase
            currentHeat += Time.fixedDeltaTime;

            var mousePosition = Input.mousePosition;
            // some positive value
             mousePosition.z = 3;
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            // DebugTarget.position = screenToWorldPoint;
            
            laser.ShootLaser(screenToWorldPoint);
        }
        else
        {
            if(currentHeat > 0)
			{
                currentHeat -= Time.fixedDeltaTime;
            }
            laser.StopLaser();
        }
      
    }
}
