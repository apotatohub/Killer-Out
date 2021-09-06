using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActivate : MonoBehaviour
{
    Collider[] colliders;
    Rigidbody[] rigidbodies;
    Animator anim;
    public Collider mainCollider;
    private void Start()
    {
        anim = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        SetState(false);
    }
    public void SetState(bool isRagdoll)
    {
        anim.enabled = !isRagdoll;
        mainCollider.enabled = !isRagdoll;
        foreach (Rigidbody item in rigidbodies)
        {
            item.useGravity = isRagdoll;
        }
        foreach (Collider item in colliders)
        {
            item.enabled = isRagdoll;
        }
    }
}
