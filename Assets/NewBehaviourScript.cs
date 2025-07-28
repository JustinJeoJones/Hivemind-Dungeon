using System.Collections;
using System.Collections.Generic;
using TwitchLib.Api.Helix;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        Rigidbody target = other.GetComponentInChildren<Rigidbody>();
        if(target != null)
        {
            target.AddExplosionForce(1000.0f, transform.position, 1000.0f, 1000.0f);
        }
    }
}
