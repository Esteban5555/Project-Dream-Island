using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettyBat : MonoBehaviour
{
    public int Health;
    public int MaxHealth = 2;


    public float maxInmunityTime = 0.5f;
    public float inmunityTime = 0f;

    public float swordForce = 100f;

    Rigidbody2D rb;
    BettyBatAI BettyBatAIscript;
    Transform splatSpawn;
    public GameObject deathSplat;

    GameObject Manager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Manager = GameObject.Find("SceneManager");
        BettyBatAIscript = GetComponent<BettyBatAI>();
        Health = MaxHealth;
        splatSpawn = transform.Find("SplatSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            GameObject splat = Instantiate(deathSplat, splatSpawn);
            splat.transform.parent = this.gameObject.transform.parent;
            Destroy(this.gameObject);
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
            Vector2 force = (rb.transform.position - BettyBatAIscript.target.position).normalized * swordForce;
            rb.AddForce(force);
            Health = Health - Manager.GetComponent<CharacterManagerScript>().GetSwordAttackDamage();
            inmunityTime = 0f;
        }
    }
}
