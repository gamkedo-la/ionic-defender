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

        private Vector2 laserOrigin;
        private Vector3 laserExtended;

        private LineRenderer lineRenderer;
        // public Transform DebugHitPosition;

        // Start is called before the first frame update
        void Start()
        {
            laserCenter = transform;
            lineRenderer = gameObject.GetComponent<LineRenderer>();
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
            laserOrigin = laserCenter.position + direction;
            // TODO limit angle

            Vector3 extendedDirection = new Vector3(direction.x, direction.y, direction.z);
            extendedDirection.Scale(new Vector3(20, 20, 20));
            laserExtended = laserCenter.position + extendedDirection;

            // Instantiate(Resources.Load("Debug/MarkerUndirectional"), laserOrigin, Quaternion.identity);
            ShootLaser(laserOrigin, laserExtended);
        }

        private void ShootLaser(Vector2 laserOrigin, Vector2 laserExtended)
        {
            Ray ray = new Ray(laserOrigin, laserExtended - laserOrigin);
            Debug.DrawLine(laserOrigin, laserExtended, Color.magenta, Time.deltaTime);
            VisualizeLaser(true);
            
            int LayerMask = UnityEngine.LayerMask.GetMask("Hitable");
            RaycastHit2D hitInfo = Physics2D.Raycast(laserOrigin, laserExtended - laserOrigin, 1000f, LayerMask);

            if (hitInfo)
            {
                var hitableEnemy = hitInfo.collider.gameObject.GetComponent<HitableEnemy>();
                if (hitableEnemy != null)
                {
                    hitableEnemy.takeDamage(100 * Time.deltaTime);
                }

                // debugHitPosition.position = hitInfo.point;
            }
        }

        private void VisualizeLaser(bool showLaser)
        {
            if (showLaser != lineRenderer.enabled)
            {
                lineRenderer.enabled = showLaser;
            }

            if (showLaser)
            {
                lineRenderer.SetPosition(0, laserOrigin);
                lineRenderer.SetPosition(1, laserExtended);
            }
        }

        public void StopLaser()
        {
            VisualizeLaser(false);
        }
    }
}