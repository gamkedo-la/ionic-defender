using System.Collections;
using System.Collections.Generic;
using player;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LaserShooter laser;

    public Transform DebugTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            // some positive value
             mousePosition.z = 3;
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            DebugTarget.position = screenToWorldPoint;
            
            laser.ShootLaser(screenToWorldPoint);
        }
      
    }
}
