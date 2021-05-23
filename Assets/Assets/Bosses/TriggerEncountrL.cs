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
    public GameObject EnemiesObject;

    public List<GameObject> doors;

    bool encounter = false;
    // Start is called before the first frame update
    void Start()
    {
        EnemiesObject = new GameObject();
        EnemiesObject.transform.parent = this.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (encounter && CheckEnemiesInChamber() <= 0) {
            doors[0].SetActive(false);
            doors[1].SetActive(false);
            Debug.Log("Encounter Finished");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MainCharacter" && !encounter)
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

        snake01.transform.parent = EnemiesObject.transform;

        GameObject snake02 = Instantiate(snakePrefab);
        snake02.transform.position = spawnPoints[1].transform.position;
        snake02.transform.parent = EnemiesObject.transform;
        Enemies.Add(snake02);

        GameObject BigMouth = Instantiate(snakeBigMouth);
        BigMouth.transform.position = spawnPoints[2].transform.position;
        BigMouth.GetComponent<AIEnemyMouth>().target = GameObject.Find("MainCharacter").transform;
        BigMouth.transform.parent = EnemiesObject.transform;
        Enemies.Add(BigMouth);

        GameObject Bat = Instantiate(snakeBettyBat);
        Bat.transform.position = spawnPoints[3].transform.position;

        Bat.GetComponent<BettyBatAI>().target = GameObject.Find("MainCharacter").transform;
        Bat.transform.parent = EnemiesObject.transform;
        Enemies.Add(Bat);

        encounter = true;
    }

    private int CheckEnemiesInChamber() {
        int count = 0;

        for (int i = 0; i < Enemies.Count; i++) {
            if (Enemies[i] != null && Enemies[i].tag == "Enemy") {
                count++;
            }
        }

        return count;

    }
}
