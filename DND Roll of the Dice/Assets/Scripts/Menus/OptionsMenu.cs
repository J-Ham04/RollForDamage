using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : InputManager
{
    public GameObject optionsExitSelected;
    public GameObject optionsMenu;
    public GameObject menuToReactivate;

    public float musicVolume;
    public float sfxVolume;

    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume");
        sfxSlider.value = PlayerPrefs.GetFloat("Sfx Volume");
    }

    private void Update()
    {
        base.Update();
        musicVolume = musicSlider.value;
        sfxVolume = sfxSlider.value;
    }

    public void ExitOptions()
    {
        menuToReactivate.SetActive(true);
        optionsMenu.SetActive(false);
        if (usingController)
        {
            EventSystem.current.SetSelectedGameObject(optionsExitSelected);
        }
    }

    public void SaveChanges()
    {
        PlayerPrefs.SetFloat("Music Volume", musicVolume);
        PlayerPrefs.SetFloat("Sfx Volume", sfxVolume);
    }
}
