using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class DestroyChecker : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Transform destroyedPrefab;
    [SerializeField] private float maxMagnitude = 4.0f;
    private bool alreadyDestroyed = false;
    private Quaternion angleFix = Quaternion.Euler(90, 0, 0);

    public UnityEvent onDestroyed = new UnityEvent();


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (alreadyDestroyed)
            return;
        
        // Calculate velocity
        var vel = (other.rigidbody ? other.rigidbody.velocity : Vector3.up*2) - rb.velocity;

        if (vel.magnitude >= maxMagnitude)
        {
            var obj = MakeDestroyed();
            ApplyStandartVelocity(obj, rb.velocity);
        }
    }

    private void OnDestroy()
    {
        onDestroyed.Invoke();
    }

    public void Explode(Vector3 point)
    {
        if (alreadyDestroyed)
            return;
        
        var obj = MakeDestroyed();
        ApplyExplodeVelocity(obj, rb.velocity, point);

    }

    private Transform MakeDestroyed()
    {
        alreadyDestroyed = true;
        Transform obj = Instantiate(destroyedPrefab, transform.position, transform.rotation * angleFix);
        obj.parent = transform.parent;
        Destroy(gameObject);
        return obj;
    }

    private void ApplyStandartVelocity(Transform obj, Vector3 velocity)
    {
        for (int idx = 0; idx < obj.childCount; idx++)
        {
            var r = obj.GetChild(idx).GetComponent<Rigidbody>();
            if (r != null)
            {
                r.velocity = rb.velocity;
            }
        }
    }
    
    private void ApplyExplodeVelocity(Transform obj, Vector3 velocity, Vector3 point)
    {
        for (int idx = 0; idx < obj.childCount; idx++)
        {
            var r = obj.GetChild(idx).GetComponent<Rigidbody>();
            if (r != null)
            {
                r.velocity = rb.velocity;
                r.AddExplosionForce(1f, point, 1f);
            }
        }
    }
}
