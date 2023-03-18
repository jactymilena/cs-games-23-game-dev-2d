using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : EnemyState
{
    [SerializeField] private float searchTime = 3.0f;
    private float timer ;

    public SearchState(EnemyManager manager) : base(manager)
    {
        timer = searchTime;
    }

    public override void RunCurrentState()
    {
        if (_enemyManager.CheckPlayerInArea())
        {
            timer = searchTime;
            _enemyManager.InitChase();
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = searchTime;
                RunNextState();
            }
            else
            {
                _enemyManager.Search();
            }
        }
    }
}
