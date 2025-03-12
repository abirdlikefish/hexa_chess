using UnityEngine;


public class ActionState : UnitState
{
    public ActionState(Unit whichUnit, MyEnum.UnitStates state) : base(whichUnit, state)
    {
    }

    public override void Enter()
    {
        base.Enter();
        WhichUnit.CurrentAction = WhichUnit.SelfInformation.Action;//在每次到自己的回合都会回满行动力
    }

    public override void Exit()
    {
        base.Exit();
    }
}