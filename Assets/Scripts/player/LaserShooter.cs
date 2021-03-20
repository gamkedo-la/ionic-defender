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
        public Transform LaserOrigin;
        public float LaserOffset = 1;
        

        // Start is called before the first frame update
        void Start()
        {
            if (LaserOrigin == null)
            {
                LaserOrigin = transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void ShootLaser(Vector3 endPoint)
        {
            Debug.DrawLine(LaserOrigin.position, endPoint, Color.magenta, 3f);
            endPoint.z = LaserOrigin.transform.position.z;
            
            Vector3 direction = (endPoint - LaserOrigin.position).normalized;
            direction.Scale(new Vector3(LaserOffset, LaserOffset, LaserOffset));
            Vector3 mufflePoint = LaserOrigin.position + direction;

            // TODO limit angle
            
            Instantiate(Resources.Load("Debug/MarkerUndirectional"), mufflePoint, Quaternion.identity);
        }
    }
}