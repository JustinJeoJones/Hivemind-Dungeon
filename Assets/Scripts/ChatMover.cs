using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChatMover : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target; // Assign the target Transform (e.g., player) in the Inspector

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target != null)
        {
            agent.SetDestination(target.position); // Set initial destination
        }
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        // Check if the agent is on the NavMesh
        if (!agent.isOnNavMesh)
        {
            TryRepositionToNavMesh();
        }
    }

    void TryRepositionToNavMesh()
    {
        // Try to find the closest point *on* the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            agent.Warp(hit.position);  // Safe reposition
        }
        else
        {
            Debug.LogWarning("Could not find NavMesh nearby to reposition!");
        }
    }
}
