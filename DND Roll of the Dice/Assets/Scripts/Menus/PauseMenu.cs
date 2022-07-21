using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    PlayerControls controls;

    public static bool GameIsPaused = false;

    private EndMenu em;

    public GameObject pauseMenuUI;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Pause.performed += ctx => PauseInput();
    }

    private void Start()
    {
        em = GetComponent<EndMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (em.gameEnded != false)
        {
            return;
        }
    }

    void PauseInput()
    {

        if (GameIsPaused)
        {
            Resume();
        }
        else Pause();
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

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
