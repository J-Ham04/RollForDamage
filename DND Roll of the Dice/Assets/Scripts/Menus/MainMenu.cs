using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private NameStorage ns;

    private void Start()
    {
        ns = FindObjectOfType<NameStorage>();
        ns.nameInput.text = ns.name;

        Time.timeScale = 1f;
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
