using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
	protected EnemyManager _enemyManager;
	protected EnemyState nextState;

	public EnemyState NextState 
	{
		set => this.nextState = value;
	}

	public EnemyState(EnemyManager manager) => this._enemyManager = manager;

	public abstract void RunCurrentState();
	public virtual void RunNextState() => _enemyManager.State = nextState;
}
