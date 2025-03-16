using UnityEngine;
using UnityEngine.UIElements;

public class PlayerRound_IdleState : PlayerRoundState
{
    public PlayerRound_IdleState(PlayerStateMachine _playerStateMachine, MyEnum.PlayerRoundState _playerState):base(_playerStateMachine, _playerState)
    {
    }
    
    
    public override void Enter()
    {
        Debug.Log("Now in PlayerRound, State is: " + playerState);
        // MapManager.Instance.CloseMapUI(MyEnum.TheOperator.Player);
        playerStateMachine.selectedGrid = null;
        playerStateMachine.selectedUnit = null;
        MyEvent.OnGridClick_left += SelectGrid;
    }
    
    public override void Exit()
    {
        MyEvent.OnGridClick_left -= SelectGrid;
        // throw new System.NotImplementedException();
    }
    
    public void SelectGrid(Vector2Int? coord)
    {
        if(coord == null)   return;
        playerStateMachine.selectedUnit = MapManager.Instance.GetUnit(coord.Value);
        if(playerStateMachine.selectedUnit == null || playerStateMachine.selectedUnit.isFriendUnit() == false)
        {
            playerStateMachine.selectedUnit = null;
            return;
        }
        else
        {
            playerStateMachine.selectedGrid = coord;
            playerStateMachine.ChangeState(MyEnum.PlayerRoundState.WaitInput_WhichAction);
        }
    }
}