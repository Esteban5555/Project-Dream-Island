using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentBossAI : MonoBehaviour
{
    public GameObject HeadSerpentPrefab;
    public GameObject TailOfSerpentPrefab;

    enum Fase { 
        first,
        second
    }

    Fase fase = Fase.first;

    List<GameObject> currentHeads = new List<GameObject>();
    GameObject currentTail;

    public List<Transform> spawnPoints;

    //Stats
    int Helth = 20;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitTilNextRound());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTail != null && currentTail.GetComponent<TailSerpentAI>().countHits > 2) {
            DestroyCurrentHeadsTails();
            StartCoroutine(WaitTilNextRound());
        }
    }

    IEnumerator WaitTilNextRound()
    {

        yield return new WaitForSeconds(4f);

        //Spawn Serpent
        switch (fase) {
            case Fase.first:
                //Spawn 1 Head and 1 Tail

                int tailSpawnIndex = Random.Range(0, 3);
                int headSpawnIndex = Random.Range(0, 3);

                while (headSpawnIndex == tailSpawnIndex) {
                    headSpawnIndex = Random.Range(0, 3);
                }

                currentTail = Instantiate(TailOfSerpentPrefab);
                currentTail.transform.position = spawnPoints[tailSpawnIndex].transform.position;

                GameObject currentHead = Instantiate(HeadSerpentPrefab);
                currentHead.transform.position = spawnPoints[headSpawnIndex].transform.position;

                currentHeads.Add(currentHead);

                //currentTail = Instantiate(TailOfSerpentPrefab,spawn);

                break;
            case Fase.second:
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
}
