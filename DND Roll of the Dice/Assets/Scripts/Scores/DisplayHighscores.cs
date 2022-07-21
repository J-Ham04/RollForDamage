using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour
{
    public TMPro.TextMeshProUGUI[] rScores;
    public TMPro.TextMeshProUGUI myScore;
    HighScores myScores;

    void Start() //Fetches the Data at the beginning
    {
        for (int i = 0; i < rScores.Length; i++)
        {
            rScores[i].text = i + 1 + ". FETCHING...";
        }
        myScores = FindObjectOfType<HighScores>();
        StartCoroutine("RefreshHighscores");

        myScore.text = "SCORE - " + PlayerPrefs.GetInt("High Score").ToString();
    }

    public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
    {
        for (int i = 0; i < rScores.Length; i++)
        {
            rScores[i].text = i + 1 + ". ";
            if (highscoreList.Length > i)
            {
                rScores[i].text = i + 1 + ". " + highscoreList[i].username + " - " + highscoreList[i].score.ToString();
            }
        }
    }
    IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
    {
        while (true)
        {
            myScores.DownloadScores();
            yield return new WaitForSeconds(5);
        }
    }
}
