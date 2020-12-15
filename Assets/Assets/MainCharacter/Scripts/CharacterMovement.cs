using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    float speed = 3f;
    public Rigidbody2D myRigidbody;
    public Animator anim;

    public bool moving = false;

    Vector2 movement;
    public int lastFacingDirection = 1;

    GameObject player;
    public CharacterSwordAttack characterSwordAttackScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MainCharacter");
        characterSwordAttackScript = player.GetComponent<CharacterSwordAttack>();
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

    }

    void FixedUpdate()
    {
        //Movement
        if (!characterSwordAttackScript.Attacking) {
            myRigidbody.MovePosition(myRigidbody.position + movement * speed * Time.fixedDeltaTime);
        }        
    }
}
