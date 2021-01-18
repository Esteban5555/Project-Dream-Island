using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigChest : MonoBehaviour
{
    public bool opened;
    public bool PlayerInRange;
    public Sprite chestOpened;
    SpriteRenderer sr;
    public GameObject Manager;

    public enum ItemInChest { 
        HeartContainer,
        sword,
        Lamp,
        RubberRing
    }

    public ItemInChest item;

    // Start is called before the first frame update
    void Start()
    {
        opened = false;
        PlayerInRange = false;
        sr = GetComponent<SpriteRenderer>();
        Manager = GameObject.Find("SceneManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (opened)
        {
            sr.sprite = chestOpened;
        }
        if (PlayerInRange && Input.GetKeyDown(KeyCode.R) && !opened)
        {
            Manager.GetComponent<CharacterManagerScript>().SetPlayerState(1);
            Manager.GetComponent<CharacterManagerScript>().setBigChest(true);
            Manager.GetComponent<CharacterManagerScript>().setItemInBigChest((int)item);
            opened = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCharacter")
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCharacter")
        {
            PlayerInRange = false;
        }
    }
}
