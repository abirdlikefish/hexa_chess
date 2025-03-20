public class GameWin : GameState
{
    public GameWin(GameStateMachine _gameStateMachine, MyEnum.GameState _whichState) : base(_gameStateMachine, _whichState)
    {
    }

    public override void Enter()
    {
        base.Enter();
        MyEvent.OnGameEnd?.Invoke(MyEnum.TheOperator.Player);
    }

    public override void Exit()
    {
        base.Exit();
    }
}