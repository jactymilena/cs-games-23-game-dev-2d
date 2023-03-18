using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyManager : MonoBehaviour
{
	[SerializeField] private string enemiesManagerTag = "EnemiesManager";
	
	public EnemyState State { get; set; }
	public EnemiesManager EnemiesObserver { get; set; }

	private PatrolState patrolState;
	private ChaseState chaseState;
	private SearchState searchState;

	public abstract void Patrol();
	public abstract void Chase();
	public abstract void InitSearch();
	public abstract void Search();
	public abstract bool CheckPlayerInArea();

	public void InitChase() => EnemiesObserver.NotifyStartChase(GetInstanceID());
	public void NotifyObserverPlayerVisibility(bool visibility) => EnemiesObserver.UpdateVisibility(GetInstanceID(), visibility);

	public void RedirectToChaseState()
    {
		State = chaseState;
	}

	public void RedirectToSearchState()
	{
		InitSearch();
		State = searchState;
	}

	public void Start()
	{
		EnemiesObserver = GameObject.FindGameObjectWithTag(enemiesManagerTag).GetComponent<EnemiesManager>();
		patrolState = new PatrolState(this);
		chaseState = new ChaseState(this);
		searchState = new SearchState(this);

		State = patrolState;
		patrolState.NextState = chaseState;
		chaseState.NextState = searchState;
		searchState.NextState = patrolState;
	}
}
