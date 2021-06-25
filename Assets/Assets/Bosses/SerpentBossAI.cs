using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SerpentBossAI : MonoBehaviour
{
    public GameObject HeadSerpentPrefab;
    public GameObject TailOfSerpentPrefab;

    private CharacterManagerScript manager;

    public enum Fase { 
        first,
        second
    }

    public List<Image> Hearts;
    public GameObject HertsGameObject;

    public Fase fase = Fase.first;

    List<GameObject> currentHeads = new List<GameObject>();
    GameObject currentTail;

    public List<Transform> spawnPoints;

    //Stats
    int Health = 10;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("SceneManager").GetComponent<CharacterManagerScript>();
        StartCoroutine(WaitTilNextRound());

        HertsGameObject.SetActive(true);

        FindObjectOfType<AudioManager>().Play("BossFight_ost");
        FindObjectOfType<AudioManager>().Pause("Temple_ost");
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(Health);

        if (currentTail != null && currentTail.GetComponent<TailSerpentAI>().countHits >= 2) {
            DestroyCurrentHeadsTails();
            StartCoroutine(WaitTilNextRound());
        }

        if (Health <= 5)
        {
            //Second Fase
            fase = Fase.second;

        }


        if (Health <= 0) {
            //Serpent Defeated
            StartCoroutine("Ending");
            
        }

        UpdateHearts();


    }

    public void UpdateHearts() {

        for (int i = 0; i < Hearts.Count; i++) {
            if (Health <= i)
            {
                Hearts[i].transform.gameObject.SetActive(false);
            }

        }
    }

    IEnumerator WaitTilNextRound()
    {

        yield return new WaitForSeconds(4f);

        int tailSpawnIndex, headSpawnIndex, heaadSpawnIndex2;

        GameObject currentHead;
        if (Health <= 0) yield return null;

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

    public void HitTaken() {
        Debug.Log("Damage dealt: " + manager.GetSwordAttackDamage());
        Health = Health - (manager.GetSwordAttackDamage());
    }
}
