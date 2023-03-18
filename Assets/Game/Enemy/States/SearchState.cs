using UnityEngine;

public class SearchState : EnemyState
{
    private const float SearchTime = 3.0f;
    private float timer ;

    public SearchState(EnemyManager manager) : base(manager)
    {
        timer = SearchTime;
    }

    public override void RunCurrentState()
    {
        if (_enemyManager.CheckPlayerInArea())
        {
            timer = SearchTime;
            _enemyManager.InitChase();
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = SearchTime;
                RunNextState();
            }
            else
            {
                _enemyManager.Search();
            }
        }
    }
}
