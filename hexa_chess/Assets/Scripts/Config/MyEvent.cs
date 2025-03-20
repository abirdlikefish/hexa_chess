using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyEvent
{
    public static Action UIUpdate;
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
    public static Action<MyEnum.ArmyType> OnClick_GenerateBtn;//生成单元的按钮

    public static Action<Vector2Int> OnPress;
    public static Action OnPressEnd;
    public static Action<MyEnum.ArmyType> SelectArmyToCreate;
    
    // game manager -> ui
    public static Action<IUnit> OpenUnitUI;
    public static Action<Vector2, Vector2, Vector2> SetGlobalInfo;
    public static Action<int , int , float> ShowGridInfoWin;
    public static Action HideGridInfoWin;
    public static Action<bool> EnterPlayerRound;

    public static Action<Vector3> CameraMove;
    public static Action<Vector3> DragScreen;

    public static Func<MyEnum.ArmyType> GetSelectedArmy;
    
    /// <summary>
    /// 当移动，攻击等的动画结束之后调用
    /// </summary>
    public static Action AnimaEnd;

    /// <summary>
    /// AI回合开始
    /// </summary>
    public static Action<int> OnEnemyRoundBegin;
    /// <summary>
    /// AI回合结束
    /// </summary>
    public static Action EnemyRoundEnd;
}
