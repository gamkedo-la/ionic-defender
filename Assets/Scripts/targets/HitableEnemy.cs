using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableEnemy : MonoBehaviour
{
    public float Health = 1;
    public int Damage = 1;

    public int Scrap;

    public float ScrapDecayRate;

    private float ScrapDecayTimer;

    public Score score;

    public GameObject ScrapText;

    public GameObject explosionParticles;

    // Start is called before the first frame update
    void Start()
    {
        ScrapDecayTimer = ScrapDecayRate;
        score = GameObject.FindGameObjectWithTag("GameController").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        ScrapDecayTimer -= Time.deltaTime;

        if (ScrapDecayTimer <= 0)
        {
            Scrap -= 1;

            ScrapDecayTimer = ScrapDecayRate;
        }
    }

    public void takeDamage(float damage)
    {
        this.Health -= damage;
        Debug.Log("took damage "+damage.ToString());
        // would it make sense to decouple (extract) this logic?
        if(this.Health<=0)
        {
            die();
        }
    }

    public void die()
    {
        Debug.Log("died");
        score.AddScrap(Scrap);
        GameObject T = Instantiate(ScrapText, transform.position, Quaternion.identity);
        if (explosionParticles != null)
        {
            GameObject explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        }
        T.GetComponent<TextMesh>().text = Scrap.ToString();
        Destroy(this.gameObject);
    }
}
