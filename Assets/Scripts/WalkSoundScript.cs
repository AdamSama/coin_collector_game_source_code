using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSoundScript : MonoBehaviour
{

    public AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (GameManager.manager.paused|| GameManager.manager.win || PlayerController.player.dead || GameManager.manager.timer<0)
            audio.Stop();
        if(CameraController.cc.characterController.isGrounded && 
        CameraController.cc.characterController.velocity.magnitude > 2f
        && !audio.isPlaying)
        // Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.A) 
        // && Input.GetKeyDown(KeyCode.D) && audio.isPlaying == false)
        {
            // Debug.Log("play");
            audio.Play();
        }
    }
}
