using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushScript : MonoBehaviour
{
    Animator anim;
    Transform spawnPoint;
    public GameObject heart;
    public float heartChance;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spawnPoint = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SwordAtacks") {
            anim.SetBool("dead", true);
        }
    }

    public void DestroyObject() {
        if (Random.Range(0f, 1f) <= heartChance) {
            GameObject h = Instantiate(heart, spawnPoint);
            h.transform.position = this.transform.position;
            h.transform.parent = this.gameObject.transform.parent;
            /*
            Rigidbody2D hrb = h.GetComponent<Rigidbody2D>();
            Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
            hrb.AddForce(randomDirection * 10);
            */
        }
        Destroy(this.gameObject);

    }
}
