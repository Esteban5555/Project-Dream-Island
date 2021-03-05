using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody2D>().MovePosition(this.GetComponent<Rigidbody2D>().position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
            Debug.Log("Entering");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Exiting");
        }
    }
}
