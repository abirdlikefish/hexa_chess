using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCom : IFguiCom
{
    private GComponent parent;
    private GComponent gameOverCom;
    private bool isShow = false;
    
    GButton restartBtn;
    GButton exitBtn;
    
    public IFguiCom Create(GComponent parent)
    {
        this.parent = parent;
        gameOverCom =parent.GetChild("GameOverCom").asCom;

        restartBtn = gameOverCom.GetChild("RestartBtn").asButton;
        exitBtn = gameOverCom.GetChild("ExitBtn").asButton;

        InitEvent();
        return this;
    }
    public void Remove()
    {
        if(isShow)
        {
            Hide();
        }
    }

    private void InitEvent()
    {
        restartBtn.onClick.Add(() => {MyEvent.ClearEvent();SceneManager.LoadScene(SceneManager.GetActiveScene().name);});
    #if UNITY_EDITOR
        exitBtn.onClick.Add(() => {MyEvent.ClearEvent();UnityEditor.EditorApplication.isPlaying = false;});
    #else
        exitBtn.onClick.Add(() => {MyEvent.ClearEvent();Application.Quit();});
    #endif
    }
    public void Show()
    {
        isShow = true;
    }

    public void Hide()
    {
        if(!isShow) return;
        isShow = false;
    }

}
