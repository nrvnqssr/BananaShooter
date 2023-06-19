using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator animator;

    private void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DeactivateRagdolls();
    }

    void ActivateRagdolls()
    {
        foreach(var RigidBody in rigidBodies) 
        { 
            RigidBody.isKinematic = false;
        }
        animator.enabled = false;
    }

    void DeactivateRagdolls()
    {
        foreach (var RigidBody in rigidBodies)
        {
            RigidBody.isKinematic = true;
        }
        animator.enabled = true;
    }
}
