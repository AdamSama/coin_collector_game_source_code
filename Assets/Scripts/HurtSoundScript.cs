using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtSoundScript : MonoBehaviour
{

    public AudioSource audio;
    public bool getHurt;

    public static HurtSoundScript instance;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        instance = this;

    }

    void Update()
    {
        if (GameManager.manager.paused|| GameManager.manager.win || PlayerController.player.dead || GameManager.manager.timer<0)
            audio.Stop();
        if(getHurt)
        {
            // Debug.Log("play");
            audio.Play();
            getHurt = false;
        }
    }
}