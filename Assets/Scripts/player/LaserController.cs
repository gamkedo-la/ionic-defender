using System.Collections;
using System.Collections.Generic;
using player;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LaserShooter laser;


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
            // some positive value to keep
            mousePosition.z = 10;
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            
            laser.ShootLaser(screenToWorldPoint);
        }
      
    }
}
