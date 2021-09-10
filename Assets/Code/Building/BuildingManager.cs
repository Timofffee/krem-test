using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PercentEvent : UnityEvent<int> { }

public class BuildingManager : MonoBehaviour
{
    private List<Rigidbody> parts = new List<Rigidbody>();
    private int totalCount = 0;
    private int ticks = 0;

    public PercentEvent percentChanged = new PercentEvent();
    public UnityEvent buildingStay = new UnityEvent();

    private Destroyer ds;

    private void Start()
    {
        RegisterParts();
        totalCount = parts.Count;
        StartCoroutine("CheckVelocity");
        ds = gameObject.AddComponent<Destroyer>();
    }

    private void RegisterParts()
    {
        var parts = gameObject.GetComponentsInChildren<DestroyChecker>();
        for (int idx = 0; idx < parts.Length; idx++)
        {
            RegisterPart(parts[idx]);
        }
    }

    private void RegisterPart(DestroyChecker part)
    {
        Rigidbody partRb = part.GetComponent<Rigidbody>();
        parts.Add(partRb);
        part.onDestroyed.AddListener(() => { PartDestroyed(partRb); } );
    }

    private void PartDestroyed(Rigidbody part)
    {
        parts.Remove(part);
        percentChanged.Invoke(CalculatePercent());
        ticks = 0;
    }

    private int CalculatePercent()
    {
        float percent = 1.0f - (float)parts.Count/totalCount;
        return (int) (percent * 100);
    }

    private IEnumerator CheckVelocity()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (parts.Count == totalCount || ds.CanExplode()) continue;

            for (int idx = 0; idx < parts.Count; idx++)
            {
                if (parts[idx].velocity.magnitude >= 0.5f)
                {
                    ticks = 0;
                    break;
                }
            }

            if (ticks >= 30)
            {
                buildingStay.Invoke();
                break;
            }

            ticks++;
        }
    }

    private void OnDestroy()
    {
        for (int idx = 0; idx < parts.Count; idx++)
        {
            parts[idx].GetComponent<DestroyChecker>().onDestroyed.RemoveAllListeners();
        }
    }
}
