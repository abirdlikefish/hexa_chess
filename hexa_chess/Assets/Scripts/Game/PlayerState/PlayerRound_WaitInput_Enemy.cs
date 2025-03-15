using UnityEngine;

public class PlayerRound_WaitInput_Enemy : PlayerRoundState
{
    public PlayerRound_WaitInput_Enemy(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
MapManager.Instance.SearchAttackArea(MyEnum.TheOperator.Player, playerStateMachine.selectedGrid.Value , 5);
        MyEvent.OnGridClick_left += SelectTarget;
        MyEvent.AnimaEnd += EndAttack;
    }

    public override void ShowUI()
    {
        base.ShowUI();
    }

    public override void Exit()
    {
        base.Exit();
        MapManager.Instance.CloseMapUI(MyEnum.TheOperator.Player);
        MyEvent.OnGridClick_left -= SelectTarget;
        MyEvent.AnimaEnd -= EndAttack;
    }

    public override void Cancel()
    {
        base.Cancel();
    }

    public override void Update()
    {
        base.Update();
    }

    //选择要攻击的敌人
    public void SelectTarget(Vector2Int? targetcoord)
    {
        if (targetcoord == null) return;
        IUnit selectedUnit = MapManager.Instance.GetAttackedUnit(targetcoord.Value);
        if (selectedUnit == null) return;//没选中东西或者选中了友军
        playerStateMachine.selectedUnit.Attack(selectedUnit);
    }

    public void EndAttack()
    {
        if (GameManager.instance.JudgeShouldEndGame())
        {
            return;
        }
        playerStateMachine.ChangeState(MyEnum.PlayerRoundState.Idle);
    }
    
}