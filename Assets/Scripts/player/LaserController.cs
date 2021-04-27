using System.Collections;
using System.Collections.Generic;
using player;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LaserShooter laser;

    public Transform DebugTarget;

    public float heatCoolDownSeconds;
    public float maxHeatSeconds;
    private float currentHeat;
    private bool overheat = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void Update()
	{
        Debug.Log(currentHeat);

		if(currentHeat >= maxHeatSeconds)
		{
            overheat = true;
        }

        if(overheat)
		{
            currentHeat -= Time.deltaTime;
		}

        if(currentHeat <= 0)
		{
            currentHeat = 0;
            overheat = false;
        }
	}

	// Update is called once per frame
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
