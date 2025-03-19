using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class UIManager
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                UIManager.Init();
            }
            return instance;
        }
    }
    public static void Init()
    {
        instance = new UIManager();
        UIPackage.AddPackage("fgui/Hexa_chess");
        GRoot.inst.SetContentScaleFactor(1920, 1080);

        instance.InitView();
        instance.InitWin();
         new GameObject("UIUpdate").AddComponent<UIUpdate>();
    }
    public void InitView()
    {
        uiViewList = new Dictionary<MyEnum.UIView, IFguiView>();
        uiViewList.Add(MyEnum.UIView.PlayView, new PlayView().Init());
        uiViewList.Add(MyEnum.UIView.MapEditView, new MapEditView().Init());
        currentView = MyEnum.UIView.Empty;
    }
    public void InitWin()
    {
        // Debug.LogWarning("InitWin");
        uiWinList = new Dictionary<MyEnum.UIWin, IFguiWin>();
        uiWinList.Add(MyEnum.UIWin.GridInfoWin , new GridInfoWin());
    }
    private Dictionary<MyEnum.UIView , IFguiView> uiViewList;
    private Dictionary<MyEnum.UIWin , IFguiWin> uiWinList;
    private MyEnum.UIView currentView;
    public void ShowView(MyEnum.UIView uiView)
    {
        if(currentView == uiView) return;
        if (currentView != MyEnum.UIView.Empty)
        {
            uiViewList[currentView].HideView();
        }
        uiViewList[uiView].ShowView();
    }
    public void ShowWin(MyEnum.UIWin uiWin , bool isShow)
    {
        if(isShow)
        {
            uiWinList[uiWin].ShowWin();
        }
        else
        {
            uiWinList[uiWin].HideWin();
        }
    }
    
}

public class UIUpdate:MonoBehaviour
{
    // public Action UpdateAction;
    void Update()
    {
        MyEvent.UIUpdate?.Invoke();
    }
}
