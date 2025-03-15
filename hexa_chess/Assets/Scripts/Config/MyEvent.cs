using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyEvent
{
    // ui -> game manager
    public static Action<Vector2Int?> OnGridClick_left;
    public static Action<Vector2Int?> OnGridClick_right;
    public static Action OnClick_testBtn;
    public static Action OnClick_nextBtn;
    public static Action OnClick_attackBtn;
    public static Action OnClick_restBtn;
    public static Action OnClick_stationBtn;
    public static Action OnClick_dismissBtn;
    public static Action OnClick_skipBtn;
    
    // game manager -> ui
    public static Action<IUnit> OpenUnitUI;
    public static Action SetGlobalInfo;
    
    /// <summary>
    /// 当移动，攻击等的动画结束之后调用
    /// </summary>
    public static Action AnimaEnd;
}
