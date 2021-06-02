using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SerpentBossAI : MonoBehaviour
{
    public GameObject HeadSerpentPrefab;
    public GameObject TailOfSerpentPrefab;

    private CharacterManagerScript manager;

    enum Fase { 
        first,
        second
    }

    Fase fase = Fase.first;

    List<GameObject> currentHeads = new List<GameObject>();
    GameObject currentTail;

    public List<Transform> spawnPoints;

    //Stats
    float Health = 20;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("SceneManager").GetComponent<CharacterManagerScript>();
        StartCoroutine(WaitTilNextRound());
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(Health);

        if (currentTail != null && currentTail.GetComponent<TailSerpentAI>().countHits > 2) {
            DestroyCurrentHeadsTails();
            StartCoroutine(WaitTilNextRound());

            Health = Health - (manager.GetSwordAttackDamage() * 2f);
        }

        if (Health <= 10)
        {
            //Second Fase
            fase = Fase.second;

        }


        if (Health <= 0) {
            //Serpent Defeated
            StartCoroutine("Ending");
            
        }
    }

    IEnumerator WaitTilNextRound()
    {

        yield return new WaitForSeconds(4f);

        int tailSpawnIndex, headSpawnIndex, heaadSpawnIndex2;

        GameObject currentHead;

        //Spawn Serpent
        switch (fase) {
            case Fase.first:
                //Spawn 1 Head and 1 Tail

                //Sonido Mountstro

                tailSpawnIndex = Random.Range(0, 3);
                headSpawnIndex = Random.Range(0, 3);

                while (headSpawnIndex == tailSpawnIndex) {
                    headSpawnIndex = Random.Range(0, 3);
                }

                currentTail = Instantiate(TailOfSerpentPrefab);
                currentTail.transform.position = spawnPoints[tailSpawnIndex].transform.position;

                currentHead = Instantiate(HeadSerpentPrefab);
                currentHead.transform.position = spawnPoints[headSpawnIndex].transform.position;

                currentHeads.Add(currentHead);

                //currentTail = Instantiate(TailOfSerpentPrefab,spawn);

                break;
            case Fase.second:

                //Sonido Monstruo

                tailSpawnIndex = Random.Range(0, 3);
                headSpawnIndex = Random.Range(0, 3);


                while (headSpawnIndex == tailSpawnIndex)
                {
                    headSpawnIndex = Random.Range(0, 3);
                }

                int headSpawnIndex2 = Random.Range(0, 3);

                while (headSpawnIndex2 == tailSpawnIndex || headSpawnIndex2 == headSpawnIndex)
                {
                    headSpawnIndex2 = Random.Range(0, 3);
                }

                currentTail = Instantiate(TailOfSerpentPrefab);
                currentTail.transform.position = spawnPoints[tailSpawnIndex].transform.position;

                currentHead = Instantiate(HeadSerpentPrefab);
                currentHead.transform.position = spawnPoints[headSpawnIndex].transform.position;

                currentHeads.Add(currentHead);

                currentHead = Instantiate(HeadSerpentPrefab);
                currentHead.transform.position = spawnPoints[headSpawnIndex2].transform.position;

                currentHeads.Add(currentHead);
                break;
        }

        yield return null;
    }

    private void DestroyCurrentHeadsTails() {
        GameObject.Destroy(currentTail);

        for (int i = 0; i < currentHeads.Count; i++) {
            GameObject.Destroy(currentHeads[i]); 
        }
        currentHeads = new List<GameObject>();
    }

    IEnumerator Ending() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Credits");
    }
}
