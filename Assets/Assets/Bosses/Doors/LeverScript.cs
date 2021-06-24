using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    bool playerInRange = false;

    private DoorManager manager;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("SceneManager").GetComponent<DoorManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetButtonDown("Submit")) {
            FindObjectOfType<AudioManager>().Play("Lever");
            manager.setLever();
            anim.SetBool("pressed", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCharacter")
        {
            playerInRange = true;
            Debug.Log("PlayerInRange");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCharacter")
        {
            playerInRange = false;
            Debug.Log("NotPlayerInRange");
        }
    }
}
