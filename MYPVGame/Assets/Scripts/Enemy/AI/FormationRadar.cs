using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationRadar : MonoBehaviour
{
    private Formation _formation;
    private List<EnemyRadar> _memberRadars = new List<EnemyRadar>();
    [SerializeField] private Transform _sharedPlayerTarget;
    [SerializeField] private bool _isTargetVisibleToFormation;

    public Transform GetSharedPlayerTarget()
    {
        return _sharedPlayerTarget;
    }

    public bool IsTargetVisibleToFormation()
    {
        return _isTargetVisibleToFormation;
    }

    public void UpdateSharedTarget(Transform target, bool isVisible)
    {
        // Only update if this radar actually sees something
        if (isVisible && target != null)
        {
            _sharedPlayerTarget = target;
            _isTargetVisibleToFormation = true;
        }
        // Otherwise, let other radars in formation maintain the current shared target
    }
    
    private void Awake()
    {
        _formation = GetComponent<Formation>();
    }

    private void FixedUpdate()
    {
        // Update formation radar data every physics update
        UpdateFormationRadarData();
    }

    private void UpdateFormationRadarData()
    {
        // Reset shared visibility
        _isTargetVisibleToFormation = false;
        _sharedPlayerTarget = null;

        // Check if any member sees the target
        foreach (EnemyRadar radar in _memberRadars)
        {
            if (radar.isTargetVisible && radar.GetRadarPlayer() != null)
            {
                _isTargetVisibleToFormation = true;
                _sharedPlayerTarget = radar.GetRadarPlayer();
                break;
            }
        }

        // Sync data across all members
        foreach (EnemyRadar radar in _memberRadars)
        {
            radar.SyncWithFormation(_isTargetVisibleToFormation, _sharedPlayerTarget);
            
            if (!_isTargetVisibleToFormation) 
            {
                radar.ClearTarget();
            }
        }
    }

    public void RegisterRadar(EnemyRadar radar)
    {
        if (!_memberRadars.Contains(radar))
        {
            _memberRadars.Add(radar);
        }
    }

    public void UnregisterRadar(EnemyRadar radar)
    {
        if (_memberRadars.Contains(radar))
        {
            _memberRadars.Remove(radar);   
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying && _sharedPlayerTarget)
        {
            UnityEditor.Handles.Label(this.transform.position + Vector3.up * 2, _sharedPlayerTarget.position + " " + _isTargetVisibleToFormation);
            UnityEditor.Handles.Label(this.transform.position + Vector3.down * 2, _memberRadars.Count.ToString());
        }
#endif
    }
}
