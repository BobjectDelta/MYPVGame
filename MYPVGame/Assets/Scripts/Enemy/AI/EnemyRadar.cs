using System.Collections;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 10)] private float _radarRadius;
    [SerializeField] private float _radarCheckInterval = 0.3f;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _visionLayer;
    [SerializeField] private Transform _playerTarget;
    [SerializeField] private Transform _enemyTarget;

    [field: SerializeField] public bool isTargetVisible { get; private set; }
    [field: SerializeField] public bool isAllyVisible { get; private set; }
    private bool _hasPreviousPosition;

    private Vector3 _previousTargetPosition = Vector3.zero;

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        UpdateTargetVisibility(_playerTarget);
    }

    private void UpdateTargetVisibility(Transform target)
    {
        if (target != null)
        {
            var currentVisibility = CheckTargetVisibility(target);

            if (!currentVisibility && _hasPreviousPosition)
            {
                var distanceToPreviousPosition = Vector2.Distance(transform.position, _previousTargetPosition);
                if (distanceToPreviousPosition <= 2 * 0.8f)
                {
                    isTargetVisible = true;
                    return;
                }

                _hasPreviousPosition = false;
            }

            isTargetVisible = currentVisibility;

            if (isTargetVisible)
            {
                _previousTargetPosition = target.position;
                _hasPreviousPosition = true;
            }
        }

        //if (_enemyTarget != null) 
        //{ 
        //    isAllyVisible = CheckTargetVisibility(_enemyTarget);
        //}
    }

    private bool CheckTargetVisibility(Transform target)
    {
        var raycastHitInfo = Physics2D.Raycast(transform.position, target.position - transform.position, _radarRadius,
            _visionLayer);
        if (raycastHitInfo.collider != null)
            return (_playerLayer & (1 << raycastHitInfo.collider.gameObject.layer)) != 0;
        return false;
    }

    private IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(_radarCheckInterval);
        DetectTargets();
        StartCoroutine(DetectionCoroutine());
    }

    private void DetectTargets()
    {
        DetectTarget(_playerTarget, _playerLayer);
        DetectTarget(_enemyTarget, _enemyLayer);    //TODO: Detect a new enemy ally after killing the old one
    }

    private void DetectPlayerTarget()
    {
        if (_playerTarget == null)
            CheckIfPlayerInRange();
        else if (_playerTarget != null)
            DetectIfPlayerOutOfRange();
    }

    private void DetectEnemyTarget()
    {
        if (_enemyTarget == null)
            CheckIfEnemyInRange();
        else if (_enemyTarget != null)
            DetectIfEnemyOutOfRange();
    }

    private void DetectTarget(Transform target, LayerMask targetLayerMask)
    {
        if (target == null)
            CheckIfTargetInRange(targetLayerMask);
        else
            DetectIfTargetOutOfRange(target);
    }

    private void CheckIfPlayerInRange()
    {
        var collision = Physics2D.OverlapCircle(transform.position, _radarRadius, _playerLayer);
        if (collision != null)
            SetRadarPlayer(collision.transform);
    }

    private void CheckIfEnemyInRange()
    {
        var collision = Physics2D.OverlapCircle(transform.position, _radarRadius, _enemyLayer);
        if (collision != null)
            SetRadarEnemy(collision.transform);
    }

    private void CheckIfTargetInRange(LayerMask targetLayerMask)
    {
        var collision = Physics2D.OverlapCircle(transform.position, _radarRadius, targetLayerMask);
        if (collision != null)
            SetRadarTarget(collision.transform);
    }

    private void DetectIfPlayerOutOfRange()
    {
        if (_playerTarget == null || _playerTarget.gameObject.activeSelf == false ||
            Vector2.Distance(transform.position, _playerTarget.position) > _radarRadius + 1)
            SetRadarPlayer(null);
    }

    private void DetectIfEnemyOutOfRange()
    {
        if (_enemyTarget == null || _enemyTarget.gameObject.activeSelf == false ||
            Vector2.Distance(transform.position, _enemyTarget.position) > _radarRadius + 1)
            SetRadarEnemy(null);
    }

    private void DetectIfTargetOutOfRange(Transform target)
    {
        if (target == null || target.gameObject.activeSelf == false ||
            Vector2.Distance(transform.position, target.position) > _radarRadius + 1)
            SetRadarTarget(null);

    }

    // Old API for backwards compatibility, TODO: REMOVE
    public Transform GetRadarTarget()
    {
        return GetRadarPlayer();
    }

    public Transform GetRadarPlayer()
    {
        return _playerTarget;
    }

    public Transform GetRadarEnemy()
    {
        return _enemyTarget;
    }

    private void SetRadarTarget(Transform target)
    {
        if (target.gameObject.GetComponent<Enemy>() == null)
        {
            _playerTarget = target;
            isTargetVisible = false;

            _hasPreviousPosition = false;
            _previousTargetPosition = Vector3.zero;
        }
        else
            _enemyTarget = target;
    }

    private void SetRadarPlayer(Transform player)
    {
        _playerTarget = player;
        isTargetVisible = false;

        _hasPreviousPosition = false;
        _previousTargetPosition = Vector3.zero;
    }

    private void SetRadarEnemy(Transform enemy)
    {
        _enemyTarget = enemy;
    }
}