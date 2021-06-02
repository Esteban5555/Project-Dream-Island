using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Text dialoguetext;
    public GameObject FadeBackground;

    private List<string> sentences = new List<string>();
    int index = 0;

    bool fade = false;

    // Start is called before the first frame update
    void Start()
    {
        AddingSentences();
        StartCoroutine("sentenceSwap");
    }

    // Update is called once per frame
    void Update()
    {
        dialoguetext.text = sentences[index];

        if (fade) {
            FadeBackgroundEffect();
        }

    }

    private void AddingSentences() {
        sentences.Add("");// 0
        sentences.Add("In Times of legend, ages ago...");// 1
        sentences.Add("It existed an Isle where civilization blossomed");// 2
        sentences.Add("People enjoyed a simple life growing the land and it's culture");// 3
        sentences.Add("All this was possible thanks to the protection of the DIVINE TURTLE");// 4
        sentences.Add("However as it is written in nature...");// 5
        sentences.Add("A great light will make shadows appear.");// 6
        sentences.Add("And so Testudo Isle ended up shrouded in darkness");// 7
        sentences.Add("Little by little the light that once batheed the isle dwindled");// 8
        sentences.Add("And the civilization that once existed crumbled");// 9
        sentences.Add("But like two sides of the same coin");// 10
        sentences.Add("Darkness canot exist without light...");// 11
    }

    IEnumerator sentenceSwap() {
        yield return new WaitForSeconds(10f);
        ChangeSentences();
    }

    private void ChangeSentences() {
        if (index >= sentences.Count -1)
        {
            fade = true;
        }
        else {
            index++;
            StartCoroutine("sentenceSwap");
        }
    }

    private void FadeBackgroundEffect() {

        if (FadeBackground.GetComponent<Image>().color.a >= 1f) {
            StartGame();
        }

        Color c = FadeBackground.GetComponent<Image>().color;

        c.a = c.a + 0.5f * Time.deltaTime;

        FadeBackground.GetComponent<Image>().color = c;
    }

    private void StartGame() {
        SaveSystem.ResetPlayerSystem();
        SaveSystem.ResetChestsInGame();
        PlayerData playerData = SaveSystem.LoadPlayerSystem();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("BeachBeginning");
    }
}
