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
        private Transform LaserCenter;
        public float LaserOffset = 1;

        public Transform DebugHitPosition;
        // public Transform DebugHitPosition;

        // Start is called before the first frame update
        void Start()
        {
            LaserCenter = transform;
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
            extendedDirection.Scale(new Vector3(20, 20, 20));
            Vector3 laserExtended = LaserCenter.position + extendedDirection;

            // Instantiate(Resources.Load("Debug/MarkerUndirectional"), laserOrigin, Quaternion.identity);
            ShootLaser(laserOrigin, laserExtended);
        }

        private void ShootLaser(Vector3 laserOrigin, Vector3 laserExtended)
        {
            Ray ray = new Ray(laserOrigin, laserExtended - laserOrigin);
            RaycastHit hitInfo;
            Debug.DrawLine(laserOrigin, laserExtended, Color.magenta, Time.deltaTime);

            int LayerMask = UnityEngine.LayerMask.GetMask("Hitable");
            if (Physics.Raycast(ray, out hitInfo, 1000f ,LayerMask))
            {
                var hitableEnemy = hitInfo.collider.gameObject.GetComponent<HitableEnemy>();
                if (hitableEnemy != null)
                {
                    // hitableEnemy.takeDamage(100 * Time.deltaTime);
                }

                DebugHitPosition.position = hitInfo.point;
            }
        }
    }
}