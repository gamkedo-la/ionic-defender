using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBreathingWobble : MonoBehaviour
{
    Quaternion origRot;
    // Start is called before the first frame update
    void Start()
    {
        origRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = origRot *
            Quaternion.AngleAxis(Mathf.Sin(Time.timeSinceLevelLoad*0.3f) * 1.5f, Vector3.up)
             *
            Quaternion.AngleAxis(Mathf.Sin(Time.timeSinceLevelLoad * 0.17f) * 1.0f, Vector3.right);
    }
}
