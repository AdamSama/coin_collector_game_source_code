using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI hpText;
    
    public GameObject endScreen;
    public TextMeshProUGUI endScreenHeader;
    public TextMeshProUGUI endScreenScoreText;

    //instance
    public static GameUI instance;
    public GameObject pauseScreen;

    void Awake()
    {
        instance = this;
        // Debug.Log("set UI instance");
    }

    void Update()
    {

        timeText.text = "Time Left: " + (int)GameManager.manager.timer;
    }

    public void UpdateScoreText()
    {
        // Debug.Log(PlayerController.player.curScore);
        scoreText.text = "Score: " + PlayerController.player.curScore;
    }

    public void UpdateHPText()
    {
        hpText.text = "HP: " + PlayerController.player.curHp;
    }

    public void SetEndScreen(bool hasWon)
    {
        endScreen.SetActive(true);
        endScreenScoreText.text = "<b> YOUR SCORE IS: </b>" + PlayerController.player.curScore;

        if(hasWon)
        {
            endScreenHeader.text = "YOU WIN!";
            endScreenHeader.color = Color.green;
        }
        else if (GameManager.manager.outside == true)
        {
            endScreenHeader.text = "YOU ARE OUT OF BOUNDS!";
            endScreenHeader.color = Color.red;

        }
        else
        {
            endScreenHeader.text = "YOU LOSS!";
            endScreenHeader.color = Color.red;
        }
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OnMenuButton()
    {
        if(GameManager.manager.paused)
            GameManager.manager.TogglePauseGame();
        SceneManager.LoadScene(0);
    }

    public void TogglePauseScreen(bool paused)
    {
        //activate and disactive the pause screen 
        pauseScreen.SetActive(paused);
    }

    public void OnResumeButton()
    {
        GameManager.manager.TogglePauseGame();
    }

}
