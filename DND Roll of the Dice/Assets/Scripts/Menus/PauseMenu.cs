using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Linq;

public class PauseMenu : InputManager
{
    public GameObject resumeButton;
    public GameObject optionsControllerSelected;
    public GameObject optionsMenu;

    public static bool GameIsPaused = false;

    private EndMenu em;

    public GameObject pauseMenuUI;

    private void Awake()
    {
        base.Awake();
        controls.Gameplay.Pause.performed += ctx => PauseInput();
    }

    private void Start()
    {
        em = GetComponent<EndMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (em.gameEnded != false)
        {
            return;
        }
        if (!usingController)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void PauseInput()
    {

        if (usingController)
        {
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
        else EventSystem.current.SetSelectedGameObject(null);

        if (GameIsPaused)
        {
            Resume();
        }
        else 
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OpenOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(true);
        if (usingController)
        {
            EventSystem.current.SetSelectedGameObject(optionsControllerSelected);
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
