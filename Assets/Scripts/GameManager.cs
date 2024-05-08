using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas victoryCanvas;

    void Start(){
        Time.timeScale = 1;
    }

    void Update()
    {
    }

    public void GameOver()
    {
        gameOverCanvas.enabled = true; //show the Game Over Canvas
        Time.timeScale = 0; 
    }

    public void Victory()
    {
        victoryCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void OnRetry(){
        SceneManager.LoadScene("Forest Start");
    }

    public void OnQuit(){
        Application.Quit();
    }
}
