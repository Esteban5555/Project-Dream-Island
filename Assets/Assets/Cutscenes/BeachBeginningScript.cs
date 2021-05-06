using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BeachBeginningScript : MonoBehaviour
{

    CharacterMovement player;
    MainCharacter player2;
    PlayableDirector direc;

    CharacterManagerScript manager;
    
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
        Debug.Log(direc.state);
        if (direc.state == PlayState.Playing)
        {
            player.enabled = false;
            player2.enabled = false;
        }
        else {
            player.enabled = true;
            player2.enabled = true;
            GameObject.Destroy(this.gameObject);
        }
    }
}
