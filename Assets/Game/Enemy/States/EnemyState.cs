using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
	protected EnemyManager _enemyManager;
	protected EnemyState _nextState;

	public EnemyState NextState 
	{
		set => this._nextState = value;
	}

	public EnemyState(EnemyManager manager) => this._enemyManager = manager;

	public abstract void RunCurrentState();
	public virtual void RunNextState() => _enemyManager.State = _nextState;
	
}
