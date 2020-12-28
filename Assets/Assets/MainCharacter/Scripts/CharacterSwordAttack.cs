using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwordAttack : MonoBehaviour
{
    public bool Attacking = false;

    float minCoolDownAttack = 0.5f;
    float coolDownAttack = 0f;

    GameObject player;
    GameObject AttackColliders;
    public CharacterMovement characterMovementScript;
    public MainCharacter MainCharacterScript;

    BoxCollider2D[] colliders;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MainCharacter");
        AttackColliders = GameObject.Find("AttackColliders");
        characterMovementScript = player.GetComponent<CharacterMovement>();
        MainCharacterScript = player.GetComponent<MainCharacter>();
        colliders = AttackColliders.GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && coolDownAttack >= minCoolDownAttack && !characterMovementScript.moving && MainCharacterScript.sword)
        {
            Attacking = true;
            coolDownAttack = 0;
        }
        anim.SetBool("Attacking", Attacking);
        coolDownAttack += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Attacking)
        {
            colliders[characterMovementScript.lastFacingDirection].enabled = true;
        }
    }

    public void finishAttack() {
        Attacking = false;
        colliders[characterMovementScript.lastFacingDirection].enabled = false;
    }
}
