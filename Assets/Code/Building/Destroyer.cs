using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private const String TAG_DESTRUCT = "can_destroyed";
    private const int MAX_EXPLODE = 2;
    
    private int explodeCount = 0;
    private Camera m_MainCamera;

    private void Start()
    {
        m_MainCamera = Camera.main;
    }

    private void Update()
    {
        if (CanExplode() && Input.GetMouseButtonDown(0))
        {
            Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag(TAG_DESTRUCT))
                {
                    hit.transform.GetComponent<DestroyChecker>().Explode(hit.point);
                    explodeCount++;
                }
            }
        }
    }

    public bool CanExplode()
    {
        return explodeCount < MAX_EXPLODE;
    }
}
