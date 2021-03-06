using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacterion : MonoBehaviour
{
    public float height = 1;
    public float width = 2;

    public float CenterX;
    public float CenterY;

    public float Speed;

    float X;
    float Y;

    public float Angle = 90;

    public Vector3 StartingPos;

    public bool Active = false;

    public float ShootTimer;
    float STReset;

    public GameObject BulletOrb;

    // Start is called before the first frame update
    void Start()
    {
        FindStartPos();
        STReset = ShootTimer;
    }

    // Update is called once per frame
    void Update()
    {

        if (Active == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, StartingPos, Mathf.Abs(Speed * Time.deltaTime));

            if(Vector3.Distance(transform.position, StartingPos) < Mathf.Abs(Speed * Time.deltaTime))
            {
                Active = true;
            }
        }
        else
        {
            Angle += Time.deltaTime * Speed;

            X = CenterX + (width * Mathf.Cos(Angle));
            Y = CenterY + (height * Mathf.Sin(Angle));

            transform.position = new Vector3(X, Y, 0);

            ShootTimer -= Time.deltaTime;

            if(ShootTimer <= 0)
            {
                Instantiate(BulletOrb, transform.position, Quaternion.identity);
                ShootTimer = STReset;
            }
        }

    }


    public void FindStartPos()
    {

        CenterX = Random.Range(-5.0f, 5.0f);

        CenterY = Random.Range(5.0f, 6.0f);

        StartingPos = new Vector3(CenterX, CenterY + (height / 2), 0);

    }
}
