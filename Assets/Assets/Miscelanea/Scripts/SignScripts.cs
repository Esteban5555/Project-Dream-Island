using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignScripts : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject text;
    public GameObject Manager;
    public string message;

    bool playerInRange;
    bool dialogueBoxShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("SceneManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.R) && !dialogueBoxShowing)
            {
                dialogueBox.SetActive(true);
                text.GetComponent<Text>().text = message;
                Manager.GetComponent<CharacterManagerScript>().SetPlayerState(2);
                dialogueBoxShowing = true;
            }
            else {
                if (Input.GetKeyDown(KeyCode.R) && dialogueBoxShowing) {
                    dialogueBox.SetActive(false);
                    text.GetComponent<Text>().text = "";
                    Manager.GetComponent<CharacterManagerScript>().SetPlayerState(0);
                    dialogueBoxShowing = false;
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCharacter") {
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
