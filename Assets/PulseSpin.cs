using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseSpin : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(Time.timeSinceLevelLoad * 750.0f, Vector3.up);
    }
}
