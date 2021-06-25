using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBreathingWobble : MonoBehaviour
{
    Quaternion origRot;

    private bool doEffect = true;
    private void Awake()
    {
        GameController.OnGameStartedChanged +=  HandleGameStarted;
    }
    void Start()
    {
        origRot = transform.rotation;
    }

    private void HandleGameStarted(bool gameStarted)
    {
        doEffect = true;
    }

    void Update()
    {
        if(false == doEffect)
        {
            return;
        }

        transform.rotation = origRot *
            Quaternion.AngleAxis(Mathf.Sin(Time.timeSinceLevelLoad*0.3f) * 1.5f, Vector3.up)
             *
            Quaternion.AngleAxis(Mathf.Sin(Time.timeSinceLevelLoad * 0.17f) * 1.0f, Vector3.right);
    }
}
