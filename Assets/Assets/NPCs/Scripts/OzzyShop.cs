using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OzzyShop : MonoBehaviour
{
    public List<string> Sentences;

    int sentenceIndex;

    // Start is called before the first frame update
    public GameObject dialogueBox;
    public GameObject text;
    public GameObject Manager;

    public GameObject ShopBox;
    public GameObject shopText;

    CharacterManagerScript ManagerScript;

    EventSystem es;
    public GameObject firsOptionMenu;

    bool playerInRange;
    bool dialogueBoxShowing = false;
    bool ShopBoxShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("SceneManager");
        ManagerScript = Manager.GetComponent<CharacterManagerScript>();

        es = es = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        StartSentences(
            "Hello Traveler, have you come for my famous potions capables of givin youth even to the oldeest of folks?",
            "It has been some time since someone new appear in Town.                       What a great surprise",
            "Maybe you will be able to erase the curse thet has befallen upon us Adventurer.",
            "Please, help our Diety recover her offspring",
            "One of the Devine children is to the west in the Great sea of the West. Please hurry you may be our last hope"
            );
    }

    private void StartSentences(string a, string b, string c, string d, string e)
    {
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
            if (Input.GetButtonDown("Submit"))
            {
                if (!ShopBoxShowing && !dialogueBoxShowing)
                {
                    es.SetSelectedGameObject(firsOptionMenu);
                    ShopBox.SetActive(true);
                    shopText.GetComponent<Text>().text = "Do you want to extend your health permanently for only 3 COINS?";
                    //text.GetComponent<Text>().text = Sentences[sentenceIndex];
                    ManagerScript.SetPlayerState(2);
                    ShopBoxShowing = true;

                }
                else {
                    if (dialogueBoxShowing) {
                        dialogueBox.SetActive(false);
                        text.GetComponent<Text>().text = "";
                        ManagerScript.SetPlayerState(0);
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

    public void affirmativeButtonPressed() {
        ShopBox.SetActive(false);

        Debug.Log("Comprada una pocion");
        ShopBoxShowing = false;
        if (ManagerScript.GetPlayerCoins() < 3)
        {
            //No Buy Message
            dialogueBox.SetActive(true);
            text.GetComponent<Text>().text = "Do you think I run a charity? Go get some coins.";
            dialogueBoxShowing = true;
        }
        else {
            //Buy Message
            dialogueBox.SetActive(true);
            text.GetComponent<Text>().text = "Oh.. thank you good sir, you won't get disappointed";
            dialogueBoxShowing = true;
            ManagerScript.BuyingHealthPotion();
            FindObjectOfType<AudioManager>().Play("BuyItem");
        }
    }

    public void negativeButtonPressed()
    {
        ShopBox.SetActive(false);
        Debug.Log("No comprada una pocion");
        ShopBoxShowing = false;

        //No Buy Message
        dialogueBox.SetActive(true);
        text.GetComponent<Text>().text = "Do not waste my time boy, I am a busy person.";
        dialogueBoxShowing = true;
    }
}
