using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private string enemyTag = "Enemy";
    private Dictionary<int, bool> _visibilities = new Dictionary<int, bool>();

    private List<EnemyManager> _managers;
   
    void Start()
    {
        _managers = GameObject.FindGameObjectsWithTag(enemyTag).Select(x => x.GetComponent<EnemyManager>()).ToList();
        foreach (var manager in _managers)
        {
            _visibilities.Add(manager.GetInstanceID(), true);
        }
    }

    public void UpdateVisibility(int instanceID, bool visibility)
    {
        _visibilities[instanceID] = visibility;
        if (!_visibilities.Values.Contains(true))
        {
            NotifyStopChase();
        }
    }
    
    
    public void NotifyStartChase(int instanceID) 
    {
        _visibilities[instanceID] = true;
        foreach (var manager in _managers)
        {
            manager.RedirectToChaseState();
        }
    }

    public void NotifyStopChase()
    {
        foreach (var manager in _managers)
        {
            manager.RedirectToSearchState();
        }
    }
}
