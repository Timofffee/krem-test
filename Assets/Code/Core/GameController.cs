using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int MIN_PERCENT = 70;
    
    private LevelLoader ll;
    private UIController ui;
    private BuildingManager bm;

    private GameObject buildingObj;

    private void Start()
    {
        ll = GetComponent<LevelLoader>();
        ui = UIController.Instance;
    }

    public void StartGame()
    {
        int level = GetLevel();
        bm = ll.Load(level);
        buildingObj = bm.gameObject;
        bm.percentChanged.AddListener(SetPercent);
        bm.buildingStay.AddListener(() => { EndGame(false); } );
        ui.ShowInGameUI(level + 1);
    }

    public void EndGame(bool success = false)
    {
        Destroy(bm);
        if (success)
        {
            ui.ShowWinPopup();
        }
        else
        {
            ui.ShowLosePopup();
        }
    }

    private void SetPercent(int percent)
    {
        if (percent < MIN_PERCENT)
        {
            ui.SetPercent(percent);
        }
        else
        {
            EndGame(true);
        }
    }

    public void RestartLevel()
    {
        Destroy(buildingObj);
        StartGame();
    }

    private int GetLevel()
    {
        // Use PlayerPrefs?? no?
        int level = 0;
        return level;
    }
}
