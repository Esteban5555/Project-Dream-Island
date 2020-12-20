using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthEnemyMainScript : MonoBehaviour
{
    public int Health;
    public int MaxHealth;


    public float maxInmunityTime = 0.5f;
    public float inmunityTime = 0f;

    bool facingRight = true;

    public float swordForce = 100f;

    Rigidbody2D rb;
    AIEnemyMouth AIEnemyMouthScript;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AIEnemyMouthScript = GetComponent<AIEnemyMouth>();
    }

    // Update is called once per frame
    void Update()
    {
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
            inmunityTime = 0f;
        }
    }
}
