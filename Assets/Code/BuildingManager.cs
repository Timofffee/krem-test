using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    private int partCount = 0;
    private int totalCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int idx = 0; idx < transform.childCount; idx++)
        {
            var part = transform.GetChild(idx).GetComponent<DestroyChecker>();
            if (part != null)
            {
                RegisterPart(part);
            }
        }
    }

    public void RegisterPart(DestroyChecker part)
    {
        partCount++;
        totalCount++;
        part.onDestroyed.AddListener(() => {PartDestroyed(part);});
    }

    public void PartDestroyed(DestroyChecker part)
    {
        partCount--;
        Debug.Log((float)partCount/totalCount);
    }
}
