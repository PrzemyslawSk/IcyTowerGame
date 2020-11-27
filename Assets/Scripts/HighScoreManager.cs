using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    public int currentScore;
    public int highScore;
    [SerializeField] Transform playerTransform;
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        currentScore = 0;

        //Update score on screen
        scoreText.text = "Score: " + currentScore.ToString();
        highScoreText.text = "HighScore: " + highScore.ToString();
    }

    private void Update()
    {
        //Update score by checking player position
        if (playerTransform.position.y > currentScore)
        {
            currentScore = (int)playerTransform.position.y;
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public void SaveHighScore() 
    {
        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
