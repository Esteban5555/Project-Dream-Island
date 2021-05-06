using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSerpentAI : MonoBehaviour
{
    enum HeadOfSerpentStates { 
        UnderWater,
        Transition,
        OutOfWater,
        Firing,
        Transition02
    }

    public GameObject fireBallPrefab;
    private GameObject Player;
    public Transform fireBallSpawn;

    bool CR_Transition, CR_OutWater, CR_Firing, CR_Water = false;

    Animator anim;

    HeadOfSerpentStates ActualState;
    // Start is called before the first frame update
    void Start()
    {
        ActualState = HeadOfSerpentStates.UnderWater;
        anim = GetComponent<Animator>();
        Player = GameObject.Find("MainCharacter");

        InvokeRepeating("Fire", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

        switch (ActualState) {
            case HeadOfSerpentStates.UnderWater:
                //Wait Til Animation Finishes
                if(!CR_Transition) StartCoroutine(ChangeToTransition());
                break;
            case HeadOfSerpentStates.Transition:
                //Wait Til Animation Finishes
                anim.SetBool("ChangeToTransition", true);
                if (!CR_OutWater) StartCoroutine(ChangeToOutOfWater());
                break;
            case HeadOfSerpentStates.OutOfWater:
                //Wait Till Fire
                anim.SetBool("ChangeToOutWater", true);
                if (!CR_Firing) StartCoroutine(ChangeToFiring());
                break;
            case HeadOfSerpentStates.Firing:
                //Invocar FireBalls
                anim.SetBool("ChangeToFiring", true);
                if (!CR_Water) StartCoroutine(ChangeToWater());
                break;
            case HeadOfSerpentStates.Transition02:
                //Invocar FireBalls
                StartCoroutine(OutoDestruct());
                break;
        }
    }

    IEnumerator ChangeToTransition() {
        CR_Transition = true;
        yield return new WaitForSeconds(4f);
        ActualState = HeadOfSerpentStates.Transition;
        CR_Transition = false;
        yield return null;
    }

    IEnumerator ChangeToOutOfWater(){
        CR_OutWater = true;
        yield return new WaitForSeconds(0.7f);
        ActualState = HeadOfSerpentStates.OutOfWater;
        CR_OutWater = false;
        yield return null;
    }

    IEnumerator ChangeToFiring()
    {
        CR_Firing = true;
        yield return new WaitForSeconds(2f);
        ActualState = HeadOfSerpentStates.Firing;
        CR_Firing = false;
        yield return null;
    }

    IEnumerator ChangeToWater()
    {
        CR_Water = true;
        yield return new WaitForSeconds(5f);
        ActualState = HeadOfSerpentStates.OutOfWater;
        anim.SetBool("ChangeToFiring", false);
        CR_Water = false;
        yield return null;
    }

    IEnumerator OutoDestruct()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Destroy(this.gameObject);
        yield return null;
    }

    private void Fire() {
        if (HeadOfSerpentStates.Firing == ActualState) {
            GameObject fireBall = Instantiate(fireBallPrefab, fireBallSpawn);
            fireBall.transform.position = fireBallSpawn.position;
            fireBall.transform.parent = this.transform.parent;
        }
    }
}
