using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : EnemyManager
{
    [SerializeField] private string waypointsTag = "Waypoint";
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float speed = 3f;

    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float farViewRange = 5f;
    [Range(0f, 360f)] [SerializeField] private float farViewAngle = 70f;

    private List<Transform> _waypoints;
    private int _currentWaypointsIndex = 0;
    private Transform _playerTarget;
    private Vector3 _target;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastDetectPosition;

    public new void Start()
    {
        base.Start();

        _spriteRenderer = GetComponentInParent<SpriteRenderer>();

        _waypoints = GameObject.FindGameObjectsWithTag(waypointsTag).Select(x => x.transform).ToList();
        _target = new Vector3(0, 0, 0);
        _currentWaypointsIndex = GetRandomWaypointIndex();
        _playerTarget = GameObject.FindGameObjectWithTag(playerTag).transform;
    }

    void Update()
    {
        State.RunCurrentState();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, _target - transform.position, Color.red);
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }

    public override void Patrol()
    {
        CheckWaypoints();
    }

    public override void Chase()
    {
        ChangeTarget(_playerTarget.position);
    }

    public override bool CheckPlayerInArea()
    {
        if (Physics2D.OverlapCircle(transform.position, farViewRange, detectionLayer.value))
        {
            Vector2 directionToTarget = (_playerTarget.position - transform.position).normalized;
            return Vector2.Angle(transform.up, directionToTarget) <= farViewAngle / 2;
        }

        return false;
    }

    public override void InitSearch()
    {
        _lastDetectPosition = _playerTarget.position; ;
    }

    public override void Search()
    {
        const int radius = 2;
        Vector3 randomPosInCircle = GetRandomPositionInCircle(_lastDetectPosition, radius); 
        const float tolerance = 10e-6f;
        if (Math.Abs(transform.position.x - _target.x) < tolerance
            && Math.Abs(transform.position.y - _target.y) < tolerance)
        {
            ChangeTarget(randomPosInCircle);
        }
    }

    private void CheckWaypoints()
    {
        if (_waypoints.Count <= 0) return;

        ChangeTarget(_waypoints[_currentWaypointsIndex].position);

        const float tolerance = 10e-6f;
        if (Math.Abs(transform.position.x - _target.x) < tolerance 
            && Math.Abs(transform.position.y - _target.y) < tolerance)
        {
            _currentWaypointsIndex = GetRandomWaypointIndex();
        }
    }

    private void ChangeTarget(Vector3 newTarget)
    {
        _target = newTarget;
        Vector2 lookDirection = (_target - transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }

    // ======= fonctions utilitaires ==================

    private int GetRandomWaypointIndex()
    {
        List<int> indexes = Enumerable.Range(0, _waypoints.Count).ToList();
        indexes = indexes.OrderBy(_ => Guid.NewGuid()).ToList();

        foreach(int index in indexes)
        {
            if (index != _currentWaypointsIndex && !ObjectDetected(_waypoints[index].position))
            {
                return index;
            }
        }

        return _currentWaypointsIndex;
    }

    private Vector2 GetRandomPositionInCircle(Vector2 center, int radius)
    {
        return center + (UnityEngine.Random.insideUnitCircle * radius);
    }

    bool ObjectDetected(Vector2 targetPosition)
    {
        Vector2 direction = ((Vector2)transform.position - targetPosition).normalized;
        RaycastHit2D hit = Physics2D.CircleCast(targetPosition, _spriteRenderer.bounds.size.x / 2, direction);

        return !(hit.collider == null || hit.collider.CompareTag("Enemy"));
    }

    // ======= fonctions de debug ==================

    void OnDrawGizmos()
    {

        
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, farViewRange);

        Vector3 angl1 = DirFromAngl(-transform.eulerAngles.z, -farViewAngle * farViewRange);
        Vector3 angl2 = DirFromAngl(-transform.eulerAngles.z, farViewAngle * farViewRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + -angl1 * farViewRange);
        Gizmos.DrawLine(transform.position, transform.position + -angl2 * farViewRange);
    }

    private Vector2 DirFromAngl(float eulerYm, float angleDegrees)
    {
        angleDegrees += eulerYm;
        return new Vector2(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }
}
