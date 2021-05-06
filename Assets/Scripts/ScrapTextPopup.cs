using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapTextPopup : MonoBehaviour
{
    public float Lifetime = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Lifetime); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
