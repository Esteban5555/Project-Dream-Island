using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject target;

    public float speed = 5;

    Vector2 Direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("MainCharacter");

        Direction = (target.transform.position - this.transform.position).normalized;

        Invoke("AutoDestroy", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MainCharacter") {
            Destroy(this.gameObject);
        }
    }

    private void AutoDestroy() {
        Destroy(this.gameObject);
    }
}
