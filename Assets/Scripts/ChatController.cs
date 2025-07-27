using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target != null)
        {
            agent.SetDestination(target.transform.position); // Set initial destination
        }
    }

    void Update()
    {
        if(chatInfo.Status == ChatFightStatus.walk)
        {
            Move();
        }
        else if(chatInfo.Status == ChatFightStatus.attack)
        {
            attack();
        }
        else if(chatInfo.Status == ChatFightStatus.die)
        {
            // Handle death logic here
            Debug.Log($"{chatInfo.Name} has died!");
            // You can add death logic, animations, etc. here
            Destroy(gameObject); // Destroy the game object when dead
        }
    }
    void attack()
    {
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
    void Move()
    {
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
                Debug.LogWarning("Agent has been off NavMesh for too long, repositioning...");
                agent.Warp(spawn); // Warp to spawn position
                offNavMeshCounter = 0; // Reset counter after repositioning
            }
        }
    }

    
}
