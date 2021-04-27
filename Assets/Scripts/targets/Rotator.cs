using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed;

	private Vector3 rotationDirection;

	private void Start()
	{
		transform.rotation = Random.rotation;
		rotationDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
	}

	// Update is called once per frame
	void Update()
    {
        transform.Rotate(rotationDirection * rotationSpeed);
    }
}
