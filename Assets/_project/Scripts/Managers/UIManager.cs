using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TargetIndicator _targetIndicatorPrefab;
    [SerializeField] Canvas _mainCanvas;
    public static UIManager Instance;
    List<TargetIndicator> _targetIndicators;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _targetIndicators = new List<TargetIndicator>();

    }
    public void AddTarget(Transform target)
    {
        var targetIndicator = Instantiate(_targetIndicatorPrefab, _mainCanvas.transform);
        targetIndicator.Init(target, _mainCanvas);
        _targetIndicators.Add(targetIndicator);
    }
    public void RemoveTarget(Transform target)
    {
        if (target == null) return;

        var key = target.GetInstanceID();

        var indicator = _targetIndicators.FirstOrDefault(i => i != null && i.Key == key);
        if (indicator)
        {
            _targetIndicators.Remove(indicator);
            Destroy(indicator.gameObject);
        }
    }
    public void UpdateTargetIndicator(List<Transform> targets, int lockedOnTarget)
    {
        foreach (var indicator in _targetIndicators)
        {
            if (indicator == null) continue;

            bool targetExists = targets.Any(target => target != null && target.GetInstanceID() == indicator.Key);
            indicator.gameObject.SetActive(targetExists);
            indicator.LockedOn = targetExists && indicator.Key == lockedOnTarget;
        }
    }
    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
