using System.Collections;
using System.Collections.Generic;
using player;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LaserController : MonoBehaviour
{
    public static Action OnLaserStart;
    public static Action OnLaserStop;
    public static Action OnPulseWaveActivated;
    public LaserShooter laser;
    public GameObject movingHead;

    public Transform DebugTarget;

    public float heatCoolDownSeconds;
    public float maxHeatSeconds;
    private float currentHeat;
    private bool overheat = false;
    public float dampening;
    private bool destroyEnemies = false;
    private bool isShooting = false;

    public Slider heatSlider;
    public PulseBurst heatWave;

    private void Awake()
    {
        OnLaserStart += () => HandleLaserShootingToggle(true);
        OnLaserStop += () => HandleLaserShootingToggle(false);
        OnPulseWaveActivated += HandlePulseWaveActivation;
    }

    private void ProcessShootingInput()
    {
        if(false == isShooting && Input.GetMouseButtonDown(0))
        {
            OnLaserStart?.Invoke();
        }

        if(isShooting && Input.GetMouseButtonUp(0))
        {
            OnLaserStop?.Invoke();
        }
    }

	private void Update()
	{
        ProcessShootingInput();

        heatSlider.value = currentHeat / maxHeatSeconds * 100;

		if(currentHeat >= maxHeatSeconds)
		{
            overheat = true;
            destroyEnemies = true;
        }

        if(overheat)
		{
            OnLaserStop?.Invoke();

            if (destroyEnemies)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    HitableEnemy hitableEnemy = enemies[i].GetComponent<HitableEnemy>();
                    if (hitableEnemy != null)
                    {
                        hitableEnemy.die(true);
                    }
                }
                destroyEnemies = false;
            }
            heatWave.BurstUpdate(currentHeat/maxHeatSeconds);
            OnPulseWaveActivated?.Invoke();
            currentHeat -= Time.deltaTime;
		}

        if(currentHeat < 0)
		{
            currentHeat = 0;
            overheat = false;
        }
	}

	private void FixedUpdate()
    {
        var mousePosition = Input.mousePosition;
        // some positive value
        mousePosition.z = 3;
        Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        // DebugTarget.position = screenToWorldPoint;

        UpdateLaserHeadRotation(screenToWorldPoint);
        
        if (Input.GetMouseButton(0) && !overheat)
        {
            // Heat increase
            currentHeat += Time.fixedDeltaTime;


            laser.ShootLaser(screenToWorldPoint);
        }
        else
        {
            if(currentHeat > 0)
			{
                currentHeat -= Time.fixedDeltaTime * dampening;
            }

            laser.StopLaser();
        }
    }
    
    private void UpdateLaserHeadRotation(Vector2 destination)
    {
        Vector2 newDirection = destination - (Vector2)movingHead.transform.position;
        var temp = Quaternion.LookRotation(newDirection).eulerAngles;
        temp.x += 90;
        movingHead.transform.rotation = Quaternion.Euler(temp);
    }

    private void HandleLaserShootingToggle(bool isOn)
    {
        isShooting = isOn;
    }

    private void HandlePulseWaveActivation()
    {
        SoundFXManager.PlayOneShot(SoundFxKey.PulseWave);
    }
}
