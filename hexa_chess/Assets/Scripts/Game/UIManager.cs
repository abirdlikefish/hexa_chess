using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [Header("Game Result UI")] 
    public Text WinText;
    public Text LoseText;

    [Header("Player Round UI")] 
    public Button EndRoundButton;
    
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShowPlayerRoundUI()
    {
        EndRoundButton.gameObject.SetActive(true);
    }
}