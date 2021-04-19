using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject EndScreen;
    private CharacterManagerScript managerScript;

    private GameObject MenuFirstSelected;
    private GameObject EndFirstSelected;
    private GameObject LizzyFirstSelected;
    private GameObject OzzyFirstSelected;

    EventSystem es;

    // Start is called before the first frame update
    void Start()
    {
        managerScript = GameObject.Find("SceneManager").GetComponent<CharacterManagerScript>();

        MenuFirstSelected = GameObject.Find("Resume");
        EndFirstSelected = GameObject.Find("Restart from CheckPoint");
        LizzyFirstSelected = GameObject.Find("Negative");
        OzzyFirstSelected = GameObject.Find("Negative");

        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        pauseMenu = GameObject.Find("Menu");

        EndScreen = GameObject.Find("EndSceen");
        EndScreen.SetActive(false);

        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start")) {
                Pause();
        }
        if (Input.GetButtonDown("Cancel") && GameIsPaused)
        {
            Resume();
        }
    }

    public void GameOverMenu()
    {
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(EndFirstSelected);
        EndScreen.SetActive(true);
        Time.timeScale = 0f;

    }

    private void Pause()
    {
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(MenuFirstSelected);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Quit() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitButton() {
        Time.timeScale = 1f;
        managerScript.QuitButtonPressed();
    }
}
