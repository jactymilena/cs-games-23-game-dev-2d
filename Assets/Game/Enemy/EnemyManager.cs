using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{
	public EnemyState State { get; set; }

	private PatrolState patrolState;
	private ChaseState chaseState;

	public abstract void Patrol();
	public abstract void Chase();
	public abstract bool CheckPlayerInArea();

	public void Start()
    {
		patrolState = new PatrolState(this);
		chaseState = new ChaseState(this);

		State = patrolState;
		patrolState.NextState = chaseState;
		chaseState.NextState = patrolState;
	}
}
