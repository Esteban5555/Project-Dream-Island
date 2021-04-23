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

    Animator anim;

    public GameObject fireBallPrefab;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("MainCharacter");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state) {
            case snakeStates.firing:
                //
                anim.SetBool("firing", true);
                break;
            case snakeStates.resting:
                //
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
}
