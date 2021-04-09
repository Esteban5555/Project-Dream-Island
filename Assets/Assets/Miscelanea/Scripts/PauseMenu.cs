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
    private CharacterManagerScript managerScript;

    private GameObject MenuFirstSelected;
    private GameObject EndFirstSelected;
    private GameObject LizzyFirstSelected;
    private GameObject OzzyFirstSelected;

    // Start is called before the first frame update
    void Start()
    {
        MenuFirstSelected = GameObject.Find("Resume");
        EndFirstSelected = GameObject.Find("Restart from CheckPoint");
        LizzyFirstSelected = GameObject.Find("Negative");
        OzzyFirstSelected = GameObject.Find("Negative");

        pauseMenu = GameObject.Find("Menu");
        pauseMenu.SetActive(false);
        managerScript = GameObject.Find("SceneManager").GetComponent<CharacterManagerScript>();

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

    private void Pause()
    {
        EventSystem.current.firstSelectedGameObject = MenuFirstSelected;
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
