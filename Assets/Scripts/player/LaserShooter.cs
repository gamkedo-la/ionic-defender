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
        private Transform laserCenter;
        public float laserOffset = 1;
        public Transform debugHitPosition;
        // public Transform DebugHitPosition;

        // Start is called before the first frame update
        void Start()
        {
            laserCenter = transform;
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void ShootLaser(Vector3 endPoint)
        {
            Vector3 extendedEndpoint = new Vector3(endPoint.x, endPoint.y, laserCenter.transform.position.z);
            Vector3 direction = (extendedEndpoint - laserCenter.position).normalized;
            direction.Scale(new Vector3(laserOffset, laserOffset, laserOffset));
            Vector3 laserOrigin = laserCenter.position + direction;
            // TODO limit angle

            Vector3 extendedDirection = new Vector3(direction.x, direction.y, direction.z);
            extendedDirection.Scale(new Vector3(20, 20, 20));
            Vector3 laserExtended = laserCenter.position + extendedDirection;

            // Instantiate(Resources.Load("Debug/MarkerUndirectional"), laserOrigin, Quaternion.identity);
            ShootLaser(laserOrigin, laserExtended);
        }

        private void ShootLaser(Vector2 laserOrigin, Vector2 laserExtended)
        {
            Ray ray = new Ray(laserOrigin, laserExtended - laserOrigin);
            Debug.DrawLine(laserOrigin, laserExtended, Color.magenta, Time.deltaTime);

            int LayerMask = UnityEngine.LayerMask.GetMask("Hitable");
            RaycastHit2D hitInfo = Physics2D.Raycast(laserOrigin, laserExtended - laserOrigin, 1000f, LayerMask);
            
            if (hitInfo)
            {
                var hitableEnemy = hitInfo.collider.gameObject.GetComponent<HitableEnemy>();
                if (hitableEnemy != null)
                {
                    hitableEnemy.takeDamage(100 * Time.deltaTime);
                }

                debugHitPosition.position = hitInfo.point;
            }
        }
    }
}