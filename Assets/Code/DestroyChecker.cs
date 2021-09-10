using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChecker : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Transform destroyedPrefab;
    [SerializeField] private float maxMagnitude = 4.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        var vel = (other.rigidbody ? other.rigidbody.velocity : Vector3.up*2) - rb.velocity;

        if (vel.magnitude >= maxMagnitude)
        {
            Instantiate(destroyedPrefab, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
            Destroy(gameObject);
            return;
        }
    }
}
