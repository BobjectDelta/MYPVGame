using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [SerializeField][Range(1, 15)] private float _viewRadius;
    [SerializeField] private float _detectionCheckDelay = 0.3f;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private LayerMask _visibilityLayer;
    [SerializeField] private Transform _target;

    [field: SerializeField]
    public bool isTargetVisible { get; private set; }

    public Transform Target
    {
        get => _target;
        set { _target = value; isTargetVisible = false; }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if (Target != null)
            isTargetVisible = CheckIfTargetVisible();
    }

    private bool CheckIfTargetVisible()
    {
        var raycastHitInfo = Physics2D.Raycast(transform.position, Target.position - transform.position, _viewRadius, _visibilityLayer);
        if (raycastHitInfo.collider  != null)
            return (_playerLayerMask & (1 << raycastHitInfo.collider.gameObject.layer)) != 0;
        return false;
        
    }

    private void DetectTarget()
    {
        if (Target == null)
            CheckIfPlayerInRange();
        else if (Target != null)
            DetectIfOutOfRange();
    }

    private void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, _viewRadius, _playerLayerMask);
        if (collision != null)
            Target = collision.transform;        
    }

    private void DetectIfOutOfRange()
    {
        if (Target == null || Target.gameObject.activeSelf == false || Vector2.Distance(transform.position, Target.position) > _viewRadius+1)
            Target = null;
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(_detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}
