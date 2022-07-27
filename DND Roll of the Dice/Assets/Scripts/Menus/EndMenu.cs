using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EndMenu : InputManager
{
    public GameObject endScreen;
    public TMP_Text scoreUI;
    public TMP_Text highscoreUI;
    public GameObject inGameUI;
    public GameObject highScoreParticles;
    public GameObject replayButton;

    private AudioManager am;
    private bool highScoreNew;
    private ScoreManager sm;
    private PlayerHealth playerHealth;

     public bool gameEnded = false;

    bool uploaded;

    private void Awake()
    {
        base.Awake();
        highScoreNew = false;
        uploaded = false;
        playerHealth = FindObjectOfType<PlayerHealth>();
        sm = FindObjectOfType<ScoreManager>();
        am = FindObjectOfType<AudioManager>();
        controls = new PlayerControls();
    }
    private void Update()
    {
        base.Update();
        if(playerHealth.health <= 0)
        {
            EndGame();
        }
        if (!usingController)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void EndGame()
    {
        if (usingController == true && gameEnded == false)
        {
            EventSystem.current.SetSelectedGameObject(replayButton);
        }
        else if (gameEnded == false) EventSystem.current.SetSelectedGameObject(null);

        gameEnded = true;
        inGameUI.SetActive(false);

        if(uploaded == false)
        {
            uploaded = true;
            if (NameStorage.playerName != null)
            {
                HighScores.UploadScore(NameStorage.playerName, sm.score);
            }
        }

        if (sm.score == sm.highScore && highScoreNew == false)
        {
            highScoreNew = true;
            am.Play("New Highscore");
            Instantiate(highScoreParticles, highScoreParticles.transform.position, Quaternion.identity);
        }

        PlayerPrefs.SetInt("High Score", sm.highScore);

        highscoreUI.text = PlayerPrefs.GetInt("High Score").ToString();

        Time.timeScale = 0f;
        scoreUI.text = sm.score.ToString();
        endScreen.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
