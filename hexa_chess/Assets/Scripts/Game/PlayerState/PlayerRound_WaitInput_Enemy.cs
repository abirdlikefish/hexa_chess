using UnityEngine;

public class PlayerRound_WaitInput_Enemy : PlayerRoundState
{
    public PlayerRound_WaitInput_Enemy(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        MapManager.Instance.SearchAttackArea(MyEnum.TheOperator.Player, playerStateMachine.selectedGrid.Value , playerStateMachine.selectedUnit.AttackRadius);
        MyEvent.OnGridClick_left += SelectTarget;
    }
    

    public override void Exit()
    {
        base.Exit();
        MapManager.Instance.CloseMapUI(MyEnum.TheOperator.Player);
        MyEvent.OnGridClick_left -= SelectTarget;
    }
    
    //选择要攻击的敌人
    public void SelectTarget(Vector2Int? targetcoord)
    {
        if (targetcoord == null)
        {
            Cancel();
            return;
        }
        IUnit selectedUnit = MapManager.Instance.GetAttackedUnit(targetcoord.Value);
        if (selectedUnit == null)
        {
            Cancel();
            return;
        }
        bool flag = playerStateMachine.selectedUnit.Attack(selectedUnit);
        if(flag)
            playerStateMachine.ChangeState(MyEnum.PlayerRoundState.PlayingAnimation);
        else
            Cancel();
    }

    public override void Cancel()
    {
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_WhichAction);
    }
}