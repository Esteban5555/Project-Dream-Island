using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossIntroScript : MonoBehaviour
{
    CharacterMovement player;
    MainCharacter player2;
    PlayableDirector direc;

    public GameObject serpentBossFight;

    public GameObject serpentHead;

    CharacterManagerScript manager;

    bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MainCharacter").GetComponent<CharacterMovement>();
        player2 = GameObject.Find("MainCharacter").GetComponent<MainCharacter>();
        direc = GameObject.Find("Director").GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerInRange && direc.state != PlayState.Playing) {
            player.enabled = true;
            player2.enabled = true;
            serpentBossFight.SetActive(true);
            GameObject.Destroy(serpentHead);
            GameObject.Destroy(this);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCharacter")
        {
            playerInRange = true;
            player.enabled = false;
            player2.enabled = false;
            direc.Play();
        }
    }
}
