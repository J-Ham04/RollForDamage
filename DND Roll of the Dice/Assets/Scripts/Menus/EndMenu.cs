using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public GameObject endScreen;
    public TMP_Text scoreUI;
    public TMP_Text highscoreUI;
    public GameObject inGameUI;
    public GameObject healthBar;
    public GameObject highScoreParticles;

    private AudioManager am;
    private bool highScoreNew;
    private ScoreManager sm;
    private PlayerHealth playerHealth;

     public bool gameEnded = false;

    bool uploaded;

    private void Awake()
    {
        highScoreNew = false;
        uploaded = false;
        playerHealth = FindObjectOfType<PlayerHealth>();
        sm = FindObjectOfType<ScoreManager>();
        am = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        if(playerHealth.playerHealth <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        inGameUI.SetActive(false);
        healthBar.SetActive(false);

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
