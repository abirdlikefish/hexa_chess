using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitInformation SelfInformation;
    public UnitState currentState = null;
    public int CurrentAction;
    public int CurrentHp;

    public void InitInformation(MyEnum.UnitType _unitType, int _maxHp, int _action, int _attack, int _attackRadius,
        int _coin, int _occupation, bool _haveZOC)
    {
        SelfInformation.unitType = _unitType;
        SelfInformation.Action = _action;
        SelfInformation.MaxHp = _maxHp;
        SelfInformation.Attak = _attack;
        SelfInformation.Coin = _coin;
        SelfInformation.AttackRadius = _attackRadius;
        SelfInformation.Occupation = _occupation;
        SelfInformation.HaveZOC = _haveZOC;
        
        currentState = new UnitState(this, MyEnum.UnitStates.Action);//所有单位初始成等待操作的状态
        CurrentHp = SelfInformation.MaxHp;
        CurrentAction = 0;//刚出生的时候行动力为0
    }

    public void ChangeState(UnitState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}