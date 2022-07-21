using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int _score;
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;

            if (value >= highScore)
            {
                highScore = score;
            }
        }
    }
    public int highScore;
    private int displayScore;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("High Score");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (score > displayScore)
        {
            displayScore += 1;
        }
        SetScoreTexts();
    }

    private void SetScoreTexts()
    {
        scoreText.text = displayScore.ToString();
        highScoreText.text = PlayerPrefs.GetInt("High Score").ToString();
    }
}
