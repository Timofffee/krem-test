using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    [Header("Layers")]
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject inGameUI;
    [Header("Popups")]
    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject losePopup;
    [Header("InGame components")]
    [SerializeField] private Text percentText;
    [SerializeField] private Text levelText;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowStartUI();
    }

    public void ShowStartUI()
    {
        HideAll();
        startUI.SetActive(true);
    }

    public void ShowInGameUI(int level)
    {
        HideAll();
        inGameUI.SetActive(true);
        SetLevel(level);
        SetPercent(0);
    }

    public void ShowWinPopup()
    {
        HideAll();
        winPopup.SetActive(true);
    }

    public void ShowLosePopup()
    {
        HideAll();
        losePopup.SetActive(true);
    }

    private void HideAll()
    {
        winPopup.SetActive(false);
        losePopup.SetActive(false);
        startUI.SetActive(false);
        inGameUI.SetActive(false);
    }
    
    public void SetPercent(int percent)
    {
        percentText.text = percent.ToString() + "%";
    }

    public void SetLevel(int level)
    {
        levelText.text = "LEVEL " + level.ToString();
    }
    
}
