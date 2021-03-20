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
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = 10;
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            Instantiate(Resources.Load("Debug/MarkerUndirectional"), screenToWorldPoint, Quaternion.identity);
            laser.ShootLaser(screenToWorldPoint);
        }
      
    }
}
