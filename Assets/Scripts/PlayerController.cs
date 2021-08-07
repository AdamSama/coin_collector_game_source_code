using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    // public int maxHp;
    // public int kills;
    public bool dead;
    public int curScore;
    public float timer;

    public Rigidbody rig;
    public AudioSource audio;
    public AudioClip coinAudio;


    public static PlayerController player;

    void Awake()
    {
        player = this;
        // Debug.Log("Set Player instance");
        audio = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
        timer = 0.0f;
        // rig = GetComponent<Rigidbody>();
    }

    public void IncrementScore()
    {
        curScore += 1;
        GameUI.instance.UpdateScoreText();
    }

    public void GetHurt(int damage)
    {
        HurtSoundScript.instance.getHurt = true;
        curHp -= damage;
        // Debug.Log("Cur HP: " + curHp);
        GameUI.instance.UpdateHPText();
        if (curHp <= 0)
            Die();
        
    }

    void Die()
    {
        dead = true;
        GameManager.manager.LostGame();
        // Debug.Log("You Died");
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            IncrementScore();
            Destroy(other.gameObject);
            audio.clip = coinAudio;
            audio.Play();
        }
        // Debug.Log("Your Score is"+ curScore); 
        // Debug.Log(other.tag);
    }

    // public void MakeSound(AudioClip curClip)
    // {
    //     audio.clip = curClip;
    //     audio.Play();
    // }


    
}
