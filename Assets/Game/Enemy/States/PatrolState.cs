public class PatrolState : EnemyState
{
    public PatrolState(EnemyManager manager) : base(manager) { }

    public override void RunCurrentState()
    {
        if (_enemyManager.CheckPlayerInArea())
        {
            _enemyManager.InitChase();
            //RunNextState();
        }
        else
            _enemyManager.Patrol();
    }
}
