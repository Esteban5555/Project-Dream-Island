using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MainCharacter : MonoBehaviour
{
    public int health;
    public bool facingRight = true;

    public enum Trinckets { Lamp, RubberRing, Sword, }

    public Trinckets itemInUse;

    //If Character has Items
    public bool RubberRing = true;
    public bool sword = true;
    public bool Lamp = true;

    public bool swimming = false;

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
        health = 3;
        itemInUse = Trinckets.Sword;
        player = GameObject.Find("MainCharacter");
        characterMovementScript = player.GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        if (facingRight && Input.GetAxisRaw("Horizontal") < 0)
        {
            Flip();
        }
        else { if (!facingRight && Input.GetAxisRaw("Horizontal") > 0) {
                Flip();
            } 
        }

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
            Vector2 force = (rb.transform.position - collision.transform.position).normalized * hitForce;
            rb.AddForce(force, ForceMode2D.Impulse);
            //rb.velocity = force;
            inmunityTime = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && inmunityTime >= maxInmunityTime)
        {
            Debug.Log("Estoy herido");
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
    }
}
