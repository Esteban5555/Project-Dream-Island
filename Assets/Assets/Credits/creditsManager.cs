using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditsManager : MonoBehaviour
{
    public GameObject Made;
    public GameObject Thanks;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ChangeToThanks");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            //Change to menu
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator ChangeToThanks() {
        yield return new WaitForSeconds(3f);
        Made.SetActive(false);
        Thanks.SetActive(true);
        StartCoroutine("ChangeToMainMenu");
    }

    IEnumerator ChangeToMainMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}
