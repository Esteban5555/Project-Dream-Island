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
        if (Input.GetKeyDown(KeyCode.E) && coolDownAttack >= minCoolDownAttack && !characterMovementScript.moving && MainCharacterScript.itemInUse == MainCharacter.Trinckets.Sword && MainCharacterScript.GetSword() == true)
        {
            Attacking = true;
            colliders[characterMovementScript.lastFacingDirection].enabled = true;
            anim.SetBool("Attacking", true);
            coolDownAttack = 0;
            StartCoroutine(stopAttacking());
        }
        coolDownAttack += Time.deltaTime;
    }

    private void FixedUpdate()
    {
    }

    IEnumerator stopAttacking()
    {
        //deactivate sprite of item
        yield return new WaitForSeconds(0.16f);
        Attacking = false;
        anim.SetBool("Attacking", false);
        colliders[0].enabled = false;
        colliders[1].enabled = false;
        colliders[2].enabled = false;
        yield return null;
    }

    public void finishAttack() {
        Attacking = false;
        anim.SetBool("Attacking", false);
        colliders[0].enabled = false;
        colliders[1].enabled = false;
        colliders[2].enabled = false;
    }
}
