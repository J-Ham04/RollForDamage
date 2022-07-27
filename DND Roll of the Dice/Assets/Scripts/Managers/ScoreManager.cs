using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // FIELDS
    private int _score;
    private int _highScore;

    private int displayScore;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    // PROPERTIES
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;

            if (value >= _highScore)
            {
                _highScore = score;
            }
        }
    }
    public int highScore
    {
        get { return _highScore; }
    }

    //METHODS
    void Start()
    {
        _highScore = PlayerPrefs.GetInt("High Score");
    }

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
