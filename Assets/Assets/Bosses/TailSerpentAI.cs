﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSerpentAI : MonoBehaviour
{
    enum TailOfSerpentStates
    {
        UnderWater,
        Transition,
        OutOfWater,
    }

    int FiringCount = 4;

    public GameObject fireBallPrefab;
    private GameObject Player;
    public Transform fireBallSpawn;

    bool CR_Transition, CR_OutWater = false;

    Animator anim;

    TailOfSerpentStates ActualState;
    // Start is called before the first frame update
    void Start()
    {
        ActualState = TailOfSerpentStates.UnderWater;
        anim = GetComponent<Animator>();
        Player = GameObject.Find("MainCharacter");
    }

    // Update is called once per frame
    void Update()
    {

        switch (ActualState)
        {
            case TailOfSerpentStates.UnderWater:
                //Wait Til Animation Finishes
                if (!CR_Transition) StartCoroutine(ChangeToTransition());
                break;
            case TailOfSerpentStates.Transition:
                //Wait Til Animation Finishes
                anim.SetBool("Transition", true);
                if (!CR_OutWater) StartCoroutine(ChangeToOutOfWater());
                break;
            case TailOfSerpentStates.OutOfWater:
                //Wait Till Fire
                anim.SetBool("Out", true);
                break;

        }
    }

    IEnumerator ChangeToTransition()
    {
        Debug.Log(ActualState);
        CR_Transition = true;
        yield return new WaitForSeconds(4f);
        ActualState = TailOfSerpentStates.Transition;
        CR_Transition = false;
        yield return null;
    }

    IEnumerator ChangeToOutOfWater()
    {
        Debug.Log(ActualState);
        CR_OutWater = true;
        yield return new WaitForSeconds(0.1f);
        ActualState = TailOfSerpentStates.OutOfWater;
        CR_OutWater = false;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SwordAtacks")
        {

        }
    }

}
