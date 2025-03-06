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
        instance.playView = new PlayView();
        instance.playView.Init();
    }
    private IFguiView playView;

}
