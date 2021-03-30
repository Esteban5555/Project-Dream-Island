using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D myRigidbody;
    public Animator anim;

    public ParticleSystem dust;

    public bool moving = false;

    Vector2 movement;
    public int lastFacingDirection = 1;

    GameObject player;
    public CharacterSwordAttack characterSwordAttackScript;
    public MainCharacter MainCharacterScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MainCharacter");
        characterSwordAttackScript = player.GetComponent<CharacterSwordAttack>();
        MainCharacterScript = player.GetComponent<MainCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (movement.x != 0) {
            lastFacingDirection = 0;
        }
        else {
            if (movement.y < 0)
            {
                lastFacingDirection = 2;
            }
            else {
                if (movement.y > 0)
                {
                    lastFacingDirection = 1;
                }
            }
        }

        anim.SetInteger("AttackDirection",lastFacingDirection);

        movement.Normalize();

        if (movement.x != 0 || movement.y != 0)
        {
            moving = true;
            anim.SetBool("Moving", true);
        }
        else {
            anim.SetBool("Moving", false);
            moving = false;
        }

        if (moving && !dust.isPlaying && !MainCharacterScript.swimming)
        {
            //FootsepsSound
            if (!FindObjectOfType<AudioManager>().IsPlayingAudio("Footsteps"))
            {
                FindObjectOfType<AudioManager>().Play("Footsteps");
            }
            createDust();
        }

        if (!moving) { FindObjectOfType<AudioManager>().Pause("Footsteps"); }

    }

    void FixedUpdate()
    {
        //Movement
        
        if (!characterSwordAttackScript.Attacking && moving) {
            myRigidbody.velocity = new Vector2(0,0);
            myRigidbody.MovePosition(myRigidbody.position + movement * speed * Time.fixedDeltaTime);
        } 
    }

    void createDust() {
        dust.Play();
    }

    void stopDust() {
        dust.Stop();
    }
}
