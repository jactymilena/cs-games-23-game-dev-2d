using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : EnemyState
{
    private float timer = 10;

    public SearchState(EnemyManager manager) : base(manager) { }

    public override void RunCurrentState()
    {
        if (_enemyManager.CheckPlayerInArea())
        {
            timer = 10;
            _enemyManager.RedirectToChaseState();
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 10;
                RunNextState();
            }
            else
            {
                _enemyManager.Search();
            }
        }
    }
}
