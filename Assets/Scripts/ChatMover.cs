using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChatMover : MonoBehaviour
{
    public float maxAllowedDistance = 0.1f;  // tweak as needed
    private int offNavMeshCounter = 0; // Counter for off NavMesh checks
    public NavMeshAgent agent;
    public Transform target; // Assign the target Transform (e.g., player) in the Inspector
    public Vector3 spawn;

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
            offNavMeshCounter++;
            if (offNavMeshCounter > 5) // If off NavMesh for too long, reset position
            {
                Debug.LogWarning("Agent has been off NavMesh for too long, repositioning...");
                agent.Warp(spawn); // Warp to spawn position
                offNavMeshCounter = 0; // Reset counter after repositioning
            }
        }
    }
    // Check if the agent is on the NavMesh
    //if (!agent.isOnNavMesh)
    //{
    //    TryRepositionToNavMesh();
    //    offNavMeshCounter++;
    //    if(offNavMeshCounter > 5) // If off NavMesh for too long, reset position
    //    {
    //        Debug.LogWarning("Agent has been off NavMesh for too long, repositioning...");
    //        //agent.Warp(spawn); // Warp to spawn position
    //        CorrectIfOffNavMesh();
    //        offNavMeshCounter = 0; // Reset counter after repositioning
    //    }
    //}
    //else
    //{
    //    offNavMeshCounter = 0; // Reset counter if on NavMesh
    //}


    void TryRepositionToNavMesh()
    {
        // Try to find the closest point *on* the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2.50f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            agent.Warp(hit.position);  // Safe reposition
        }
        else
        {
            Debug.LogWarning("Could not find NavMesh nearby to reposition!");
        }
    }

    //public void CorrectIfOffNavMesh()
    //{
    //    NavMeshHit hit;
    //    float checkRadius = 5.0f;

    //    if (NavMesh.SamplePosition(transform.position, out hit, checkRadius, NavMesh.AllAreas))
    //    {
    //        float distance = Vector3.Distance(transform.position, hit.position);
    //        Debug.Log($"[NavMeshCorrector] Distance to NavMesh: {distance}");

    //        if (distance > maxAllowedDistance)
    //        {
    //            Debug.Log("[NavMeshCorrector] Too far — nudging back!");

    //            // Push slightly inward to keep it away from edge
    //            Vector3 directionToCenter = (hit.position - transform.position).normalized;
    //            Vector3 inwardOffset = directionToCenter * 0.1f;

    //            Vector3 correctedPosition = hit.position + inwardOffset;

    //            agent.Warp(correctedPosition + Vector3.up * 1.0f);
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("[NavMeshCorrector] Still no NavMesh found!");
    //    }
    //}
}
