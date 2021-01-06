using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    //Health
    private int health;
    private int maxHealth;

    private int minMaxHealth = 4;

    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public Sprite coin;
    public Sprite SwordSprite;
    public Sprite LampSprite;
    public Sprite RubberRingSprite;

    public Transform itemRecivedLocation;

    public Image[] ItemsAvailable;

    public bool facingRight = true;

    public bool recivedItem;

    //Coins

    public bool inChest;
    public int coins;

    //Items
    public enum Trinckets { Lamp, RubberRing, Sword, }

    public Trinckets itemInUse;

       //If Character has Items
    public bool RubberRing = true;
    public bool sword = true;
    public bool Lamp = true;

    public bool swimming = false;

    //Action Button pressed

    public bool actionButton;

    //Inmunity After Hit
    public float maxInmunityTime = 0.5f;
    public float inmunityTime = 0f;

    public float hitForce = 100f;

    //Time to change Items
    float minChangeItemLapse = 0.5f;
    float changeItemLapse = 0f;

    GameObject player;
    public GameObject CandleLight;
    public CharacterMovement characterMovementScript;
    Rigidbody2D rb;
    SpriteRenderer sr;

    //public Light2D candleLight;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        itemInUse = Trinckets.Sword;
        player = GameObject.Find("MainCharacter");
        characterMovementScript = player.GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        health = 4;
        maxHealth = 4;
        if (maxHealth < minMaxHealth) { maxHealth = minMaxHealth; }

        coins = 0;
        inChest = false;

        recivedItem = false;

        actionButton = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Input

        if (changeItemLapse >= minChangeItemLapse) {
            if (Input.GetKeyDown(KeyCode.Space) && !characterMovementScript.moving && !swimming)
            {
                changeItem();
                anim.SetInteger("ItemInUse", (int)itemInUse);
                changeItemLapse = 0;
            }
        }


        if (changeItemLapse < 100) { changeItemLapse += Time.deltaTime; }

        //setting inmunity time
        if (inmunityTime < maxInmunityTime)
        {
            sr.enabled = !sr.enabled;
            inmunityTime = inmunityTime + Time.deltaTime;
        }
        else {
            if (!sr.enabled) {
                sr.enabled = true;
            }
        }

        //Set the candle Light
        if (itemInUse == Trinckets.Lamp)
        {
            CandleLight.SetActive(true);
        }
        else {
            CandleLight.SetActive(false);
        }

        //Health System

        for (int i = 0; i < hearts.Length; i++) {

            if (i < health / 2)
            {
                hearts[i].sprite = fullHeart;
            }
            else {
                if (((health % 2) != 0) && i < ((health + 1) / 2))
                {
                    hearts[i].sprite = halfHeart;
                }
                else {
                    hearts[i].sprite = emptyHeart;
                }
            }

            if (i < (maxHealth / 2))
            {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }

        //Item System
        if (!(sword == false && Lamp == false && RubberRing == false))
        {
            for (int i = 0; i < ItemsAvailable.Length; i++)
            {
                ItemsAvailable[i].enabled = false;
                if ((int)itemInUse == i)
                {
                    ItemsAvailable[i].enabled = true;
                }
            }
        }
        else {
            for (int i = 0; i < ItemsAvailable.Length; i++)
            {
                ItemsAvailable[i].enabled = false;
            }
        }

        //action Button Pressed

        if (Input.GetKeyDown(KeyCode.R))
        {
            actionButton = true;
        }
        else {
            actionButton = false;
        }

        //Opening MiniChest

        if (inChest && actionButton) {
            recivedItem = true;
            coins++;
        }

        //if item recived

        if (recivedItem)
        {
            //show Sprite of item
            anim.SetBool("RecivedItem", true);
            characterMovementScript.enabled = false;
            StartCoroutine(stopRecivedItem());
            itemRecivedLocation.GetComponent<SpriteRenderer>().sprite = coin;
        }
        else {
            //flipCharacter
            if (facingRight && Input.GetAxisRaw("Horizontal") < 0)
            {
                Flip();
            }
            else
            {
                if (!facingRight && Input.GetAxisRaw("Horizontal") > 0)
                {
                    Flip();
                }
            }
        }
    }

    IEnumerator stopRecivedItem(){
        //deactivate sprite of item
        yield return new WaitForSeconds(1f);
        Debug.Log("ItemRecived");
        recivedItem = false;
        anim.SetBool("RecivedItem", false);
        characterMovementScript.enabled = true;
        itemRecivedLocation.GetComponent<SpriteRenderer>().sprite = null;
        yield return null;
    }
    public void Flip() {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0);
    }


    void changeItem() {
        switch (itemInUse) {
            case Trinckets.Lamp:
                if (RubberRing)
                {
                    itemInUse = Trinckets.RubberRing;
                }
                else { if (sword) {
                        itemInUse = Trinckets.Sword;
                    } 
                }
                break;
            case Trinckets.Sword:
                if (Lamp)
                {
                    itemInUse = Trinckets.Lamp;
                }
                else
                {
                    if (RubberRing)
                    {
                        itemInUse = Trinckets.RubberRing;
                    }
                }
                break;
            case Trinckets.RubberRing:
                if (sword)
                {
                    itemInUse = Trinckets.Sword;
                }
                else
                {
                    if (Lamp)
                    {
                        itemInUse = Trinckets.Lamp;
                    }
                }
                break;
        }
    }

    void AddHeartConteiner() {
        maxHealth = maxHealth + 2;
    }

    void replenishOneHeart() {
        if (health + 2 > maxHealth) {
            health = maxHealth;
            return;
        }
        health = health + 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.tag == "WaterTrigger")
        {
            swimming = true;
            anim.SetBool("Swimming", true);
        }
        
        if (collision.tag == "Enemy" && inmunityTime >= maxInmunityTime) {
            Debug.Log("Estoy herido");
            health--;
            Vector2 force = (rb.transform.position - collision.transform.position).normalized * hitForce;
            rb.AddForce(force, ForceMode2D.Impulse);
            //rb.velocity = force;
            inmunityTime = 0f;
        }

        if (collision.tag == "heart") {
            replenishOneHeart();
        }


        if (collision.tag == "Chest")
        {
            inChest = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && inmunityTime >= maxInmunityTime)
        {
            health--;
            Vector2 force = (rb.transform.position - collision.transform.position).normalized * hitForce;
            rb.AddForce(force, ForceMode2D.Impulse);
            //rb.velocity = force;
            inmunityTime = 0f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == null) return;

        if (collision.tag == "WaterTrigger")
        {
            swimming = false;
            anim.SetBool("Swimming", false);
        }

        if (collision.tag == "Chest")
        {
            inChest = false;
        }
    }
}
