using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace player
{
    [SelectionBase]
    public class LaserShooter : MonoBehaviour
    {
        public Transform LaserCenter;
        public float LaserOffset = 1;
        

        // Start is called before the first frame update
        void Start()
        {
            if (LaserCenter == null)
            {
                LaserCenter = transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void ShootLaser(Vector3 endPoint)
        {
            Vector3 extendedEndpoint = new Vector3(endPoint.x, endPoint.y, LaserCenter.transform.position.z);
            Vector3 direction = (extendedEndpoint - LaserCenter.position).normalized;
            direction.Scale(new Vector3(LaserOffset, LaserOffset, LaserOffset));
            Vector3 laserOrigin = LaserCenter.position + direction;
            // TODO limit angle

            Vector3 extendedDirection = new Vector3(direction.x, direction.y, direction.z);
            extendedDirection.Scale(new Vector3(20,20,20));
            Vector3 laserExtended = LaserCenter.position + extendedDirection;

            Instantiate(Resources.Load("Debug/MarkerUndirectional"), laserOrigin, Quaternion.identity);
            ShootLaser(laserOrigin, laserExtended);

        }

        private static void ShootLaser(Vector3 laserOrigin, Vector3 laserExtended)
        {
            Ray ray = new Ray(laserOrigin, laserExtended);
            RaycastHit hitInfo;
            
            if (Physics.Raycast(ray, out hitInfo, LayerMask.NameToLayer("Hitable")))
            {
                Debug.DrawLine(laserOrigin, laserExtended, Color.magenta, 3000f);
                var hitableEnemy = hitInfo.collider.gameObject.GetComponent<HitableEnemy>();
                if(hitableEnemy != null)
                {
                    hitableEnemy.takeDamage(100 * Time.deltaTime);
                }
            }
        }
    }
}