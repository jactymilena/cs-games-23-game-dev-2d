using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(EnemyManager manager) : base(manager) { }

    public override void RunCurrentState()
    {
        _enemyManager.NotifyObserverPlayerVisibility(_enemyManager.CheckPlayerInArea());
        _enemyManager.Chase();
    }
}