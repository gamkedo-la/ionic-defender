using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBurst : MonoBehaviour
{
    public GameObject bubbleEffect;
    Vector3 startScale;

    void Start() {
        bubbleEffect.SetActive(true);
        startScale = transform.localScale;
        EndBurst();
    }

    public void BurstUpdate(float newSize)
    {
        float flippedPerc = (1.0f - newSize * newSize);
        flippedPerc *= 2.5f; // accelerate wave
        if(flippedPerc>1.0f) {
            EndBurst();
            return;
        }
        Vector3 newScale = transform.localScale;
        newScale.x = newScale.y = flippedPerc * 20.0f;
        transform.localScale = newScale;
    }

    public void EndBurst() {
        transform.localScale = Vector3.zero; // hide it
    }
}
