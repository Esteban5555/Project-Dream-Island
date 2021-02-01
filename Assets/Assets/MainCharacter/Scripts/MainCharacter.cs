using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    public enum States { Normal, Chest, Sign }

    private States state = States.Normal;

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

    //Chests
    public bool MiniChestOpened = false;
    public bool BigChestOpened = false;
    int ItemInchest;
    bool recivedItem = false;

    //Sign
    public bool SignOpened = false;

    public int coins;
    private GameObject CoinText;

    //Items
    public enum Trinckets { Lamp, RubberRing, Sword, }

    public Trinckets itemInUse;

       //If Character has Items
    public bool RubberRing = false;
    public bool sword = false;
    public bool Lamp = false;

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
    public CharacterSwordAttack characterSwordScript;
    Rigidbody2D rb;
    SpriteRenderer sr;

    //public Light2D candleLight;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        itemInUse = Trinckets.Sword;
        player = GameObject.Find("MainCharacter");
        CoinText = GameObject.Find("CoinText");
        characterMovementScript = player.GetComponent<CharacterMovement>();
        characterSwordScript = player.GetComponent<CharacterSwordAttack>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        health = 4;
        maxHealth = 4;
        if (maxHealth < minMaxHealth) { maxHealth = minMaxHealth; }

        coins = 0;

        actionButton = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case States.Normal:
                PlayerFrozen(false);
                anim.SetBool("Sword", sword);
                //Input

                if (changeItemLapse >= minChangeItemLapse)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && !characterMovementScript.moving && !swimming)
                    {
                        changeItem();
                        changeItemLapse = 0;
                    }
                }


                if (changeItemLapse < 100) { changeItemLapse += Time.deltaTime; }

                //setting inmunity time
                if (inmunityTime < maxInmunityTime)
                {
                    sr.enabled = !sr.enabled;
                    PlayerFrozen(true);
                    anim.SetBool("Moving", false);
                    inmunityTime = inmunityTime + Time.deltaTime;
                }
                else
                {
                    PlayerFrozen(false);
                    if (!sr.enabled)
                    {
                        sr.enabled = true;
                    }
                }

                //Set the candle Light
                if (itemInUse == Trinckets.Lamp)
                {
                    CandleLight.SetActive(true);
                }
                else
                {
                    CandleLight.SetActive(false);
                }

                //Health System

                for (int i = 0; i < hearts.Length; i++)
                {

                    if (i < health / 2)
                    {
                        hearts[i].sprite = fullHeart;
                    }
                    else
                    {
                        if (((health % 2) != 0) && i < ((health + 1) / 2))
                        {
                            hearts[i].sprite = halfHeart;
                        }
                        else
                        {
                            hearts[i].sprite = emptyHeart;
                        }
                    }

                    if (i < (maxHealth / 2))
                    {
                        hearts[i].enabled = true;
                    }
                    else
                    {
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
                else
                {
                    for (int i = 0; i < ItemsAvailable.Length; i++)
                    {
                        ItemsAvailable[i].enabled = false;
                    }
                }

                PlayerFliping();

                break;

            case States.Chest:

                //object in chest
                if (!recivedItem)
                {
                    if (BigChestOpened)
                    {

                        if (ItemInchest == 0)
                        {
                            AddHeartConteiner();
                        }
                        else
                        {
                            if (ItemInchest == 1)
                            {
                                SetSword(true);
                                anim.SetBool("Sword", true);
                            }
                            else
                            {
                                if (ItemInchest == 2)
                                {
                                    SetLamp(true);
                                }
                                else
                                {
                                    SetRubberRing(true);
                                }
                            }
                        }
                        bigChestOpened();
                        BigChestOpened = false;
                    }
                    else
                    {
                        if (MiniChestOpened)
                        {
                            coins++;
                            miniChestOpened();
                            setCoinsInCanvas();
                            MiniChestOpened = false;
                        }
                    }
                }
                else {
                    recivedItem = false;
                    state = States.Normal;
                }
                break;

            case States.Sign:
                PlayerFrozen(true);
                break;

        }
    }

    private void PlayerFliping() {
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
    private void PlayerFrozen(bool frozen) {
        characterMovementScript.enabled = !frozen;
        characterSwordScript.enabled = !frozen;
    }
    public void miniChestOpened() {

        //show Sprite of item
        anim.SetBool("RecivedItem", true);
        PlayerFrozen(true);
        StartCoroutine(stopRecivedItem());
        itemRecivedLocation.GetComponent<SpriteRenderer>().sprite = coin;
        
    }

    public void bigChestOpened() {
        //show Sprite of item
        anim.SetBool("RecivedItem", true);
        PlayerFrozen(true);
        StartCoroutine(stopRecivedItem());
        switch (ItemInchest) {
            case 0:
                itemRecivedLocation.GetComponent<SpriteRenderer>().sprite = coin;
                break;
            case 1:
                itemRecivedLocation.GetComponent<SpriteRenderer>().sprite = SwordSprite;
                break;
            case 2:
                itemRecivedLocation.GetComponent<SpriteRenderer>().sprite = LampSprite;
                break;
            case 3:
                itemRecivedLocation.GetComponent<SpriteRenderer>().sprite = RubberRingSprite;
                break;
            }

    }

    IEnumerator stopRecivedItem(){
        //deactivate sprite of item
        yield return new WaitForSeconds(1f);
        Debug.Log("ItemRecived");
        anim.SetBool("RecivedItem", false);
        PlayerFrozen(false);
        itemRecivedLocation.GetComponent<SpriteRenderer>().sprite = null;
        recivedItem = true;
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
                    anim.SetInteger("ItemInUse", (int)itemInUse);
                }
                else { if (sword) {
                        itemInUse = Trinckets.Sword;
                        anim.SetInteger("ItemInUse", (int)itemInUse);
                    } 
                }
                break;
            case Trinckets.Sword:
                if (Lamp)
                {
                    itemInUse = Trinckets.Lamp;
                    anim.SetInteger("ItemInUse", (int)itemInUse);
                }
                else
                {
                    if (RubberRing)
                    {
                        itemInUse = Trinckets.RubberRing;
                        anim.SetInteger("ItemInUse", (int)itemInUse);
                    }
                }
                break;
            case Trinckets.RubberRing:
                if (sword)
                {
                    itemInUse = Trinckets.Sword;
                    anim.SetInteger("ItemInUse", (int)itemInUse);
                }
                else
                {
                    if (Lamp)
                    {
                        itemInUse = Trinckets.Lamp;
                        anim.SetInteger("ItemInUse", (int)itemInUse);
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
    }

    //Getters and Setters

    public int GetCurrentHealth() {
        return health;
    }

    public void SetCurrentHealth(int health) {
        this.health = health;
    }

    public int GetCurrentMaxHealth()
    {
        return maxHealth;
    }

    public void SetCurrentMaxHealth(int health)
    {
        this.maxHealth = health;
    }

    public bool GetSword()
    {
        return sword;
    }

    public void SetSword(bool item)
    {
        this.sword = item;
    }

    public bool GetLamp()
    {
        return Lamp;
    }

    public void SetLamp(bool item)
    {
        this.Lamp = item;
    }

    public bool GetRubberRing()
    {
        return RubberRing;
    }

    public void SetRubberRing(bool item)
    {
        this.RubberRing = item;
    }

    public int GetCurrentCoins()
    {
        return coins;
    }

    public void SetCurrentCoins(int coins)
    {
        this.coins = coins;
    }

    public void SetMiniChestOpened(bool b) {
        MiniChestOpened = b;
    }

    public void SetState(int s) {
        state = (States)s;
    }

    public void SetItemInChest(int i) {
        ItemInchest = i;
    }

    public void setCoinsInCanvas()
    {
        CoinText.GetComponent<Text>().text = "X " + GetCurrentCoins();
    }
}
