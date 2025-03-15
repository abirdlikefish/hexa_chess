using UnityEngine;

public class PlayerRound_WaitInput_Enemy : PlayerRoundState
{
    public PlayerRound_WaitInput_Enemy(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState) : base(_playerStateMachine, _playerState)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        MyEvent.OnGridClick_left += SelectTarget;
    }

    public override void ShowUI()
    {
        base.ShowUI();
    }

    public override void Exit()
    {
        base.Exit();
        MyEvent.OnGridClick_left -= SelectTarget;
    }

    public override void Cansel()
    {
        base.Cansel();
    }

    public override void Update()
    {
        base.Update();
    }

    //选择要攻击的敌人
    public void SelectTarget(Vector2Int? targetcoord)
    {
        if (targetcoord == null) return;
        IUnit selectedUnit =  MapManager.Instance.GetUnit(targetcoord.Value);
        if (selectedUnit == null) return;//没选中东西
        if (selectedUnit.isFriendUnit()) return;//选中友军
        
    }
}