using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject Credits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            EscapeButtonPressed();
        }

    }

    public void StartButtonPressed() {
        startMenu.SetActive(false);
        MainMenu.SetActive(true);
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
            return;
        }
        if (OptionsMenu.active) {
            MainMenu.SetActive(true);
            OptionsMenu.SetActive(false);
            return;
        }
        if (Credits.active) {
            MainMenu.SetActive(true);
            Credits.SetActive(false);
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
