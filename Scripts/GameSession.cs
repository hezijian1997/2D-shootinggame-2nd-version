using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameSession : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int currentScore;
    [SerializeField] int currentHealth;
    //[Range(1f,10f)] [SerializeField] float GameSpeed = 1f;

    private void Awake()
    {
        SetUpSingleton();
    }
    
    // in order to keep the score for the next scene
    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    public int GetScore()
    {
        return currentScore;
    }
    // Use this for initialization
 //   void Start ()
 //   {
 //       scoreText.text = "Score: " + currentScore.ToString();
		
	//}
	
	// Update is called once per frame
	//void Update ()
 //   {
 //       Time.timeScale= GameSpeed;
	//}

    public void AddScore(int ScoreValue)
    {
        currentScore += ScoreValue;
        //scoreText.text = "Score: " + currentScore.ToString();
    }

    public void DecreaseScore(int DamageValue)
    {
    }

    //public int GetHealth()
    //{
    //    return currentHealth;
    //}

    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
