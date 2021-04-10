using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject startButton;
    public GameObject MainMenu;
    public GameObject firstMainButton;
    public GameObject OptionsMenu;
    public GameObject Credits;

    EventSystem es;

    // Start is called before the first frame update
    void Start()
    {
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            EscapeButtonPressed();
        }

    }

    public void StartButtonPressed() {
        startMenu.SetActive(false);
        MainMenu.SetActive(true);
        es.SetSelectedGameObject(firstMainButton);
    }

    public void EscapeButtonPressed() {
        if (startMenu.active)
        {
            //ExitGame
            Debug.Log("Exiting Game");
            return;
        }
        if (MainMenu.active) {
            startMenu.SetActive(true);
            MainMenu.SetActive(false);
            es.SetSelectedGameObject(startButton);
            return;
        }
        if (OptionsMenu.active) {
            MainMenu.SetActive(true);
            OptionsMenu.SetActive(false);
            es.SetSelectedGameObject(firstMainButton);
            return;
        }
        if (Credits.active) {
            MainMenu.SetActive(true);
            Credits.SetActive(false);
            es.SetSelectedGameObject(firstMainButton);
        }

    }

    public void ContinueButtonPressed() {
        PlayerData playerData = SaveSystem.LoadPlayerSystem();
        if (playerData != null) {
            SceneManager.LoadScene(playerData.scene);
        }
        Debug.Log("No Save Data");
    }

    public void NewGameButtonPressed() {
        SaveSystem.ResetPlayerSystem();
        PlayerData playerData = SaveSystem.LoadPlayerSystem();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(playerData.scene);
    }
}
