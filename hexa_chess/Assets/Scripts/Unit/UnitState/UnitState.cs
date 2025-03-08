using UnityEngine;

public class UnitState
{
    /// <summary>
    /// 这是哪个单位
    /// </summary>
    public Unit WhichUnit;
    /// <summary>
    /// 枚举状态
    /// </summary>
    public Enum.UnitStates state;

    public UnitState(Unit whichUnit, Enum.UnitStates state)
    {
        WhichUnit = whichUnit;
        this.state = state;
    }

    public virtual void Enter()
    {
        Debug.Log("Now " + WhichUnit + " is in " + state + " state");
    }

    public virtual void Exit()
    {
        Debug.Log("Now " + WhichUnit + " exit from " + state + " state");
    }
    
}