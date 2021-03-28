using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptainDialogueScript : MonoBehaviour
{
    public List<string> SentencesNoKey;
    public List<string> SentencesKey;

    public List<string> Sentences;

    int sentenceIndex = 0;

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

        StartSentencesNoKey(
            "Ohh... an Adventurer in this lands, how odd...",
            "Nevermind, Can you help me find the Keys of my house?",
            "If you do, I will give you my most precious tresure."
            );

        StartSentencesKey(
        "Thank you, Thank you, thank you. May The divine be with you",
        "Ohh yeah I almost forgot, you can have my RUBBER RING",
        "Take care of it, it has help me in my many adventures"
        );
    }

    private void StartSentencesNoKey(string a, string b, string c)
    {
        SentencesNoKey.Add(a);
        SentencesNoKey.Add(b);
        SentencesNoKey.Add(c);
    }

    private void StartSentencesKey(string a, string b, string c)
    {
        SentencesKey.Add(a);
        SentencesKey.Add(b);
        SentencesKey.Add(c);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (Manager.GetComponent<CharacterManagerScript>().mainCharacterScript.GetPirateKey())
                {
                    if (!dialogueBoxShowing)
                    {
                        dialogueBox.SetActive(true);
                        text.GetComponent<Text>().text = SentencesKey[sentenceIndex];
                        Manager.GetComponent<CharacterManagerScript>().SetPlayerState(2);
                        dialogueBoxShowing = true;

                    }
                    else
                    {
                        if (sentenceIndex < SentencesKey.Count - 1)
                        {
                            sentenceIndex++;
                            text.GetComponent<Text>().text = SentencesKey[sentenceIndex];
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
                else
                {
                    if (!dialogueBoxShowing)
                    {
                        dialogueBox.SetActive(true);
                        text.GetComponent<Text>().text = SentencesNoKey[sentenceIndex];
                        Manager.GetComponent<CharacterManagerScript>().SetPlayerState(2);
                        dialogueBoxShowing = true;

                    }
                    else
                    {
                        if (sentenceIndex < SentencesNoKey.Count - 1)
                        {
                            sentenceIndex++;
                            text.GetComponent<Text>().text = SentencesNoKey[sentenceIndex];
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
