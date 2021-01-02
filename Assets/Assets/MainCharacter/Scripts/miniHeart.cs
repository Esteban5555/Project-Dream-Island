using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniHeart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCharacter")
        {
            Destroy(this.gameObject);
        }
    }
}
