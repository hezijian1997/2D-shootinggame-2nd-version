using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using scenemanagement
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

	public void LoadGameOver()
    {

        // use Coroutine to delay game
        StartCoroutine(GameOverDelay());
 
    }

    // Delay game for 2s
    IEnumerator GameOverDelay()
    {

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Game Over");

    }

    public void GameScene()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentSceneIndex);
        SceneManager.LoadScene("Game");
    }

    public void WinScene()
    {
        SceneManager.LoadScene("YOU WIN");
        //FindObjectOfType<GameSession>().ResetGame();
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
