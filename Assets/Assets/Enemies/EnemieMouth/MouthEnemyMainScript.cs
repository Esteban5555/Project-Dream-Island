using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthEnemyMainScript : MonoBehaviour
{
    public int Health;
    public int MaxHealth = 2;


    public float maxInmunityTime = 0.5f;
    public float inmunityTime = 0f;

    bool facingRight = true;

    public float swordForce = 100f;

    Rigidbody2D rb;
    AIEnemyMouth AIEnemyMouthScript;
    Transform splatSpawn;
    public GameObject deathSplat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AIEnemyMouthScript = GetComponent<AIEnemyMouth>();
        Health = MaxHealth;
        splatSpawn = transform.Find("SplatSpawn");
    }

    // Update is called once per frame
    void Update()
    {

        if (Health <= 0) {
            GameObject splat = Instantiate(deathSplat, splatSpawn);
            splat.transform.parent = this.gameObject.transform.parent;
            Destroy(this.gameObject);
        }

        if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
            facingRight = false;
        }
        else {
            if (!facingRight && rb.velocity.x >= 0) {
                Flip();
                facingRight = true;
            }
        }

        if (inmunityTime < maxInmunityTime)
        {
            inmunityTime = inmunityTime + Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SwordAtacks" && inmunityTime >= maxInmunityTime)
        {
            Vector2 force = (rb.transform.position - AIEnemyMouthScript.target.position).normalized * swordForce;
            rb.AddForce(force);
            Health--;
            inmunityTime = 0f;
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0);
    }
}
