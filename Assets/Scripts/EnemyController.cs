using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Basic Info")]
    public float timeBetweenAttack;
    float timer;

    public Transform target;
    public float walkingDistance;
    public float minDistance;
    public bool isWandering;
    public bool reachDestination;
    // public float wanderTimer;
    // public float wanderRadius;
    

    private NavMeshAgent nav;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        // timer = 0f;
        // timeBetweenAttack = 0.5f;

    }

    void Start()
    {
        timeBetweenAttack = 1.5f;
        timer = 0;
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, target.position);
        //when the enemy sees player within a range of distance, they go to the position of player
        if(distance > minDistance)
        {
            transform.LookAt(target);
            nav.SetDestination(target.position);
        }
        else      
        {     
            nav.SetDestination(transform.position);
            if (timer >= timeBetweenAttack)
            {
                TakeDamage();
                timer = 0f;
            }
                
                
        }   
    }
            
    

    // public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    // {
    //     Vector3 randDirection = Random.insideUnitSphere * dist;
    //     randDirection += origin;
    //     NavMeshHit navHit;
    //     NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
    //     return navHit.position;
    // }

    // public static Vector3 RandomPosition()
    // {
    //     float xp = Random.Range(80,-80);
    //     float zp = Random.Range(80,-80);
    //     return new Vector3(xp, 1.1f, zp);
    // }



    void TakeDamage()
    {
        timer = 0;
        PlayerController.player.GetHurt(20);
    }

  

}



