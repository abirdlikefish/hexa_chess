public class GameLose : GameState
{
    public GameLose(GameStateMachine _gameStateMachine, MyEnum.GameState _whichState) : base(_gameStateMachine, _whichState)
    {
    }

    public override void Enter()
    {
        base.Enter();
        MyEvent.OnGameEnd?.Invoke(MyEnum.TheOperator.Enemy);
    }

    public override void Exit()
    {
        base.Exit();
    }
    
}