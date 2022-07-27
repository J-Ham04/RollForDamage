using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : InputManager
{
    private NameStorage ns;

    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject optionsFirstSelected;

    private void Start()
    {
        ns = FindObjectOfType<NameStorage>();
        ns.nameInput.text = ns.name;

        Time.timeScale = 1f;
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
        if (usingController)
        {
            EventSystem.current.SetSelectedGameObject(optionsFirstSelected);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
