using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChatController : MonoBehaviour
{
    public float maxAllowedDistance = 0.1f;  // tweak as needed
    private int offNavMeshCounter = 0; // Counter for off NavMesh checks
    public NavMeshAgent agent;
    public GameObject target; // Assign the target Transform (e.g., player) in the Inspector
    public Vector3 spawn;
    public ChatCharacter chatInfo;
    public int attackTimer = 0; // Timer for attack cooldown
    public Animator animatior;
    private ChatFightStatus currentStatus;
    public GameObject model;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        animatior = GetComponentInChildren<Animator>();
        animatior.animatePhysics = true;
        if (target != null)
        {
            agent.SetDestination(target.transform.position); // Set initial destination
        }
        animatior.CrossFade("walk", 0.01f);
    }

    void Update()
    {
        if (target != null)
        {
            //model.transform.LookAt(new Vector3(transform.position.x, 180, transform.position.z));
            
        }
        if (chatInfo.Status == ChatFightStatus.walk)
        {
            Move();
        }
        else if (chatInfo.Status == ChatFightStatus.attack)
        {
            attack();
        }
        else if (chatInfo.Status == ChatFightStatus.die)
        {
            // Handle death logic here
            UnityEngine.Debug.Log($"{chatInfo.Name} has died!");
            // You can add death logic, animations, etc. here
            Destroy(gameObject); // Destroy the game object when dead
        }
        else if (chatInfo.Status == ChatFightStatus.win)
        {
            win();
        }

    }

    private void ChangeAnimation(ChatFightStatus status, float crossFase = 0.2f)
    {
        if (currentStatus != status)
        {
            string animation = "";
            switch (status)
            {
                case ChatFightStatus.walk:
                    animation = "walk";
                    break;
                case ChatFightStatus.attack:
                    animation = "fight";
                    break;
                case ChatFightStatus.die:
                    animation = "die";
                    break;
                case ChatFightStatus.win:
                    animation = "win";
                    //model.transform.Translate(Vector3.down, Space.World);
                    break;
                default:
                    animation = "idle";
                    break;
            }
            currentStatus = status;
            UnityEngine.Debug.LogWarning(animation);
            animatior.CrossFade(animation, crossFase);
        }
    }

    private void win()
    {
        agent.isStopped = true;
        ChangeAnimation(ChatFightStatus.win);
        //animation.Play();
    }

    void attack()
    {
        agent.isStopped = true;
        ChangeAnimation(ChatFightStatus.attack);
        if(attackTimer <= 0)
        {
            target.GetComponent<BossController>().TakeDamage(chatInfo.Damage);
            attackTimer = 100;
        }
        else
        {
            {
                // Decrease the attack timer
                attackTimer--;
            }
        }
    }
    public void TakeDamage(int dmg)
    {
        chatInfo.Health -= dmg;
        if(chatInfo.Health <= 0)
        {
            chatInfo.IsAlive = false;
            chatInfo.Health = chatInfo.MaxHealth;
            Destroy(gameObject);
        }
    }
    void Move()
    {
        UnityEngine.Debug.Log($"{chatInfo.Name} has died!");
        agent.isStopped = false;
        ChangeAnimation(ChatFightStatus.walk);
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
        // Check if the agent is on the NavMesh
        if (!agent.isOnNavMesh)
        {
            offNavMeshCounter++;
            if (offNavMeshCounter > 5) // If off NavMesh for too long, reset position
            {
                UnityEngine.Debug.LogWarning("Agent has been off NavMesh for too long, repositioning...");
                agent.Warp(spawn); // Warp to spawn position
                offNavMeshCounter = 0; // Reset counter after repositioning
            }
        }
    }

    
}
