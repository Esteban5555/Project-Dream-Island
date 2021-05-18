using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEncountrL : MonoBehaviour
{
    public GameObject snakePrefab;
    public GameObject snakeBigMouth;
    public GameObject snakeBettyBat;

    public List<GameObject> spawnPoints;

    public List<GameObject> Enemies;

    public List<GameObject> doors;

    bool encounter = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (encounter && Enemies.Count == 0) {
            GameObject.Destroy(this.transform.parent);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MainCharacter")
        {
            StartEncounter();
        }
    }

    private void StartEncounter()
    {
        //Close doors

        doors[0].SetActive(true);
        doors[1].SetActive(true);

        //Spawn Enemies

        GameObject snake01 = Instantiate(snakePrefab);
        snake01.transform.position = spawnPoints[0].transform.position;

        Enemies.Add(snake01);

        GameObject snake02 = Instantiate(snakePrefab);
        snake02.transform.position = spawnPoints[1].transform.position;

        Enemies.Add(snake02);

        GameObject BigMouth = Instantiate(snakeBigMouth);
        BigMouth.transform.position = spawnPoints[2].transform.position;
        BigMouth.GetComponent<AIEnemyMouth>().target = GameObject.Find("MainCharacter").transform;

        Enemies.Add(BigMouth);

        GameObject Bat = Instantiate(snakeBettyBat);
        Bat.transform.position = spawnPoints[3].transform.position;

        Bat.GetComponent<BettyBatAI>().target = GameObject.Find("MainCharacter").transform;

        Enemies.Add(Bat);

        encounter = true;
    }
}
