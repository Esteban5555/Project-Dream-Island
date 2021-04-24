using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    GameObject Manager;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("SceneManager");

        if (Manager.GetComponent<CharacterManagerScript>().GetPlayerPirateKey()) {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MainCharacter") {
            GameObject.Destroy(this.gameObject);
        }
    }
}
