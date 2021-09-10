using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    const String TAG_DESTRUCT = "can_destroyed";
    private Camera m_MainCamera;
    
    // ---
    public Transform prefab;

    private void Start()
    {
        m_MainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag(TAG_DESTRUCT))
                {
                    Instantiate(prefab, hit.transform.position, hit.transform.rotation * Quaternion.Euler(new Vector3(90, 0, 0)));
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
