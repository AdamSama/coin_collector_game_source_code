using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public float timer;

    void Start()
    {
        timer = 0.0f;
    }
    

    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 20f)
            OnContinueEnter();
    }


    public void OnContinueEnter()
    {
        SceneManager.LoadScene(2);
    }
}
