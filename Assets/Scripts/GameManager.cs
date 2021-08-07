using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [Header("GameInfo")]
    public int enemyNumber;
    public int coinNumber;
    public int coinGoal;
    public float timer;

    public bool paused;
    public bool win;
    public bool outside;

    public GameObject enemy;
    public static GameManager manager;
    public GameObject coins;

    void Awake()
    {
        manager = this;
        // Debug.Log("manager set");
    }

    void Start()
    {
        int number = Random.Range(enemyNumber - 5, enemyNumber + 5);
        SpawnEnemy();
        SpawnCoins();
    }

    void Update()
    {

        timer -= Time.deltaTime;
        //if we reach the game goal and we still have time, we win the game
        if (PlayerController.player.curScore >= coinGoal && timer >=0 )
        {
            //we win the game
            WinGame();

        }
        else if (timer < 0 || PlayerController.player.dead)
        {
            LostGame();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        paused = !paused;
        if(paused)
            Time.timeScale = 0.0f;
        else
        {
            Time.timeScale = 1.0f;
        }
        
        GameUI.instance.TogglePauseScreen(paused);
    }

    public void LostGame()
    {
        Time.timeScale = 0.0f;
        GameUI.instance.SetEndScreen(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        win = true;
        Time.timeScale = 0.0f;
        GameUI.instance.SetEndScreen(true);
    }

    public void Out()
    {
        outside = true;
        Time.timeScale = 0.0f;
        GameUI.instance.SetEndScreen(false);
    }

    void SpawnEnemy()
    {
        int count = 0;
        while(count < enemyNumber)
        {
            Instantiate(enemy, RandomNavmeshLocation(80), Quaternion.identity);
            count += 1;
        }
    }
    
    void SpawnCoins()
    {
        int count = 0;
        while(count < coinNumber)
        {
            Instantiate(coins, RandomNavmeshLocation(80), Quaternion.identity);
            count += 1;
        }
    }
    
    public Vector3 RandomNavmeshLocation(float radius) 
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        Vector3 finalPosition = hit.position;
        return finalPosition;
    }
    


}
