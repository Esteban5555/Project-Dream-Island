using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public int health;
    public bool facingRight = true;

    public enum Trinckets { Lamp, RubberRing, None, }

    public Trinckets itemInUse;

    public bool RubberRing = false;
    public bool noItem = true;
    public bool Lamp = false;
    public bool swimming = false;

    public float maxInmunityTime = 0.5f;
    public float inmunityTime = 0f;

    public float hitForce = 500f;

    float minChangeItemLapse = 0.5f;
    float changeItemLapse = 0f;

    GameObject player;
    public CharacterMovement characterMovementScript;
    Rigidbody2D rb;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        itemInUse = Trinckets.None;
        player = GameObject.Find("MainCharacter");
        characterMovementScript = player.GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody2D>();
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
                Debug.Log("presing Space");
                if (noItem)
                {
                    noItem = false;
                    Lamp = false;
                    RubberRing = true;
                    anim.SetBool("NoItem", false);
                    anim.SetBool("Lamp", false);
                    anim.SetBool("RubberRing", true);
                }
                else
                {
                    if (Lamp)
                    {
                        noItem = true;
                        Lamp = false;
                        RubberRing = false;
                        anim.SetBool("NoItem", true);
                        anim.SetBool("Lamp", false);
                        anim.SetBool("RubberRing", false);
                    }
                    else
                    {
                        noItem = false;
                        Lamp = true;
                        RubberRing = false;
                        anim.SetBool("NoItem", false);
                        anim.SetBool("Lamp", true);
                        anim.SetBool("RubberRing", false);
                    }
                }
                changeItemLapse = 0;
            }
        }

        if (changeItemLapse < 100) { changeItemLapse += Time.deltaTime; }

        if (inmunityTime < maxInmunityTime)
        {
            inmunityTime = inmunityTime + Time.deltaTime;
        }

    }

    public void Flip() {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0);
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
            rb.AddForce(force);
            Debug.Log(force);
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
