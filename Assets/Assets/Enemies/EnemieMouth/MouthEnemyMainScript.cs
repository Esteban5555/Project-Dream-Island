using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthEnemyMainScript : MonoBehaviour
{
    public int Health;
    public int MaxHealth;

    bool facingRight = true;

    public float swordForce = 100f;

    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SwordAtacks")
        {
            if (facingRight) {
                Vector2 force = new Vector2(-1f, 0) * swordForce;
                rb.AddForce(force);
            }
            Debug.Log("SwordAttackSuccesfull");
        }
    }
}
