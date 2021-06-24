using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : MonoBehaviour
{

    enum snakeStates { 
        resting, 
        firing
    }

    public float reach = 5;

    snakeStates state = snakeStates.resting;

    int Health = 1;
    int MaxHealth = 1;

    public float fireInterval = 2f;

    Animator anim;

    public GameObject fireBallPrefab;
    private GameObject Player;
    public Transform fireBallSpawn;
    private GameObject Manager;

    bool firing = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("MainCharacter");
        Manager = GameObject.Find("SceneManager");
        anim = GetComponent<Animator>();
        InvokeRepeating("fireFireBall", fireInterval, fireInterval);
    }

    private void Update()
    {
        switch (state) {
            case snakeStates.firing:
                //
                firing = true;
                anim.SetBool("firing", true);
                break;
            case snakeStates.resting:
                //
                firing = false;
                anim.SetBool("firing", false);
                break;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < reach)
        {
            //firing
            state = snakeStates.firing;
            //Instantiate Fireball

        }
        else {
            //resting
            state = snakeStates.resting;
        }
    }

    private void fireFireBall() {
        if (firing)
        {
            FindObjectOfType<AudioManager>().Play("Snake");
            firing = true;
            GameObject fireBall = Instantiate(fireBallPrefab, fireBallSpawn);
            fireBall.transform.position = fireBallSpawn.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SwordAtacks")
        {
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
            else {
                Health = Health - Manager.GetComponent<CharacterManagerScript>().GetSwordAttackDamage();
            }
        }
    }
}
