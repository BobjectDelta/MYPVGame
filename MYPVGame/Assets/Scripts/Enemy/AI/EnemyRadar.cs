using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadar : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 10)] private float _radarRadius;
    [SerializeField] private float _radarCheckInterval = 0.3f;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _visionLayer;
    [SerializeField] private Transform _target;

    [field: SerializeField] public bool isTargetVisible { get; private set; }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if (_target != null)
            isTargetVisible = CheckTargetVisibility();
    }

    private bool CheckTargetVisibility()
    {
        var raycastHitInfo = Physics2D.Raycast(transform.position, _target.position - transform.position, _radarRadius, _visionLayer);
        if (raycastHitInfo.collider != null)
            return (_playerLayer & (1 << raycastHitInfo.collider.gameObject.layer)) != 0;
        return false;

    }

    private IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(_radarCheckInterval);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    private void DetectTarget()
    {
        if (_target == null)
            CheckIfPlayerInRange();
        else if (_target != null)
            DetectIfOutOfRange();
    }

    private void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, _radarRadius, _playerLayer);
        if (collision != null)
            this.SetRadarTarget(collision.transform);
    }

    private void DetectIfOutOfRange()
    {
        if (_target == null || _target.gameObject.activeSelf == false || Vector2.Distance(transform.position, _target.position) > _radarRadius + 1)
            this.SetRadarTarget(null);
    }

    public Transform GetRadarTarget()
    {
        return _target;
    }

    public void SetRadarTarget(Transform target)
    {
        _target = target;
        isTargetVisible = false;
    }
}
