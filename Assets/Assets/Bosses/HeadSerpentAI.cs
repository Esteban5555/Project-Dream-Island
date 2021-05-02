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

    Animator anim;

    HeadOfSerpentStates ActualState;
    // Start is called before the first frame update
    void Start()
    {
        ActualState = HeadOfSerpentStates.UnderWater;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (ActualState) {
            case HeadOfSerpentStates.UnderWater:
                //Wait Til Animation Finishes
                StartCoroutine(ChangeToTransition());
                break;
            case HeadOfSerpentStates.Transition:
                //Wait Til Animation Finishes
                anim.SetBool("ChangeToTransition", true);
                StartCoroutine(ChangeToOutOfWater());
                break;
            case HeadOfSerpentStates.OutOfWater:
                //Wait Till Fire
                anim.SetBool("ChangeToOutWater", true);
                StartCoroutine(ChangeToFiring());
                break;
            case HeadOfSerpentStates.Firing:
                //Invocar FireBalls
                anim.SetBool("ChangeToFiring", true);
                StartCoroutine(ChangeToWater());
                break;
            case HeadOfSerpentStates.Transition02:
                //Invocar FireBalls
                StartCoroutine(OutoDestruct());
                break;
        }
    }

    IEnumerator ChangeToTransition() {
        Debug.Log(ActualState);
        yield return new WaitForSeconds(2f);
        ActualState = HeadOfSerpentStates.Transition;
        yield return null;
    }

    IEnumerator ChangeToOutOfWater(){
        Debug.Log(ActualState);
        yield return new WaitForSeconds(0.5f);
        ActualState = HeadOfSerpentStates.OutOfWater;
        yield return null;
    }

    IEnumerator ChangeToFiring()
    {
        Debug.Log(ActualState);
        yield return new WaitForSeconds(4f);
        ActualState = HeadOfSerpentStates.Firing;
        yield return null;
    }

    IEnumerator ChangeToWater()
    {
        Debug.Log(ActualState);
        yield return new WaitForSeconds(5f);
        ActualState = HeadOfSerpentStates.OutOfWater;
        anim.SetBool("ChangeToFiring", false);
        yield return null;
    }

    IEnumerator OutoDestruct()
    {
        Debug.Log(ActualState);
        yield return new WaitForSeconds(5f);
        GameObject.Destroy(this.gameObject);
        yield return null;
    }
}
