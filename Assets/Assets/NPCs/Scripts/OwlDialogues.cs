﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwlDialogues : MonoBehaviour
{
    public List<string> Sentences;

    int sentenceIndex;

    // Start is called before the first frame update
    public GameObject dialogueBox;
    public GameObject text;
    public GameObject Manager;

    bool playerInRange;
    bool dialogueBoxShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("SceneManager");

        StartSentences(
            "What do we have here... a lost soul it seems", 
            "It has been some time since someone new appear in Town.                       What a great surprise", 
            "Maybe you will be able to erase the curse thet has befallen upon us Adventurer.", 
            "Please, help our Diety recover her offspring", 
            "One of the Devine children is to the west in the Great sea of the West. Please hurry you may be our last hope"
            );
    }

    private void StartSentences(string a, string b, string c, string d, string e) {
        Sentences.Add(a);
        Sentences.Add(b);
        Sentences.Add(c);
        Sentences.Add(d);
        Sentences.Add(e);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.R)) {
                if (!dialogueBoxShowing)
                {
                    dialogueBox.SetActive(true);
                    text.GetComponent<Text>().text = Sentences[sentenceIndex];
                    Manager.GetComponent<CharacterManagerScript>().SetPlayerState(2);
                    dialogueBoxShowing = true;

                }
                else {
                    if (sentenceIndex < Sentences.Count - 1)
                    {
                        sentenceIndex++;
                        text.GetComponent<Text>().text = Sentences[sentenceIndex];
                    }
                    else
                    {
                        sentenceIndex = 0;
                        dialogueBox.SetActive(false);
                        text.GetComponent<Text>().text = "";
                        Manager.GetComponent<CharacterManagerScript>().SetPlayerState(0);
                        dialogueBoxShowing = false;
                    }
                }
            }
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