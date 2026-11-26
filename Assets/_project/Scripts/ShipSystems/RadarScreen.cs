using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class RadarScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image _radarScreen;
    [SerializeField]  Image _blipPrefab;
    [SerializeField]  Transform _player;
    [SerializeField]  LayerMask _targetLayers;
    //for lock target
    [SerializeField]
    [Range(0f, 2000f)] float _lockOnRange = 800f;
    [SerializeField]
    [Range(0f, 45f)] float _lockOnRadius = 15f;

    [Header("Radar Settings")]
    [SerializeField, Min(1f)]  float _radarRange = 1000f;
    [SerializeField, Min(0.02f)]  float _refreshInterval = 0.5f;
    [SerializeField, Min(1)]  int _maxColliders = 200;
    [SerializeField, Min(0f)]  float _heightThreshold = 1f;
    [SerializeField, Min(0f)]  float _edgePadding = 8f;

     RectTransform _radarRect;
     Collider[] _hits;

    List<Transform> _targets = new List<Transform>();
    HashSet<Transform> _uniqueTargets = new HashSet<Transform>();
    List<RadarBlip> _blips = new List<RadarBlip>();

    private float _nextRefreshTime;

    public Transform LockOnTarget { get; set; }


    private void Awake()
    {
        if (_radarScreen == null || _blipPrefab == null || _player == null)
        {
            enabled = false;
            return;
        }

        _radarRect = _radarScreen.rectTransform;
        _hits = new Collider[Mathf.Max(1, _maxColliders)];

        RadarBlip template = _blipPrefab.GetComponent<RadarBlip>();

        if (template == null)
        {
            enabled = false;
            return;
        }

        if (_blipPrefab.gameObject.scene.IsValid())
        {
            template.transform.SetParent(_radarRect, false);
            ConfigureBlip(template);
            template.gameObject.SetActive(false);

            _blips.Add(template);
        }
        LockOnTarget = null;
    }

    private void Start()
    {
        RefreshTargets();
        _nextRefreshTime = Time.time + _refreshInterval;
    }

    private void Update()
    {
        if (Time.time >= _nextRefreshTime)
        {
            RefreshTargets();
            _nextRefreshTime = Time.time + _refreshInterval;
        }
    }

    private void LateUpdate()
    {
        DrawBlips();
        UIManager.Instance.UpdateTargetIndicator(_targets, LockOnTarget ? LockOnTarget.GetInstanceID() : -1);
    }

    private void OnDisable()
    {
        foreach (RadarBlip blip in _blips)
        {
            if (blip != null)
                blip.gameObject.SetActive(false);
        }
    }

    private void RefreshTargets()
    {
        _targets.Clear();
        _uniqueTargets.Clear();

        LockOnTarget = null;
        float closest = _lockOnRange;
        Vector3 playerPosition = _player.position;

        int count = Physics.OverlapSphereNonAlloc( _player.position, _radarRange, _hits, _targetLayers, QueryTriggerInteraction.Collide
        );

        for (int i = 0; i < count; i++)
        {
            Collider hit = _hits[i];

            if (hit == null) continue;
            Transform target = hit.attachedRigidbody != null? hit.attachedRigidbody.transform : hit.transform.root;

            if (target == _player || target.IsChildOf(_player))  continue;

            if (!target.gameObject.activeSelf) continue;

            closest = TryLockOnTarget(target, playerPosition, closest);
            if (_uniqueTargets.Add(target)) _targets.Add(target);
        }
    }

    float TryLockOnTarget(Transform target, Vector3 playerPosition, float closest)
    {
        var targetPosition = target.position;
        var distance = Vector3.Distance(target.position, playerPosition);
        var direction = targetPosition - playerPosition;
        var angle = Vector3.Angle(direction, _player.forward);
        if(distance < closest && angle < _lockOnRadius)
        {
            closest = distance;
            LockOnTarget = target;
        }
        return closest;
    }

    private void DrawBlips()
    {
        EnsureBlipCount(_targets.Count);

        for (int i = 0; i < _blips.Count; i++)
        {
            RadarBlip blip = _blips[i];

            if (i >= _targets.Count || _targets[i] == null)
            {
                blip.gameObject.SetActive(false);
                continue;
            }
            Transform target = _targets[i];
            blip.gameObject.SetActive(true);
            Vector3 localDirection = _player.InverseTransformDirection(target.position - _player.position);
            Vector2 horizontalDirection = new Vector2( localDirection.x, localDirection.z);
            Vector2 normalizedPosition = horizontalDirection / _radarRange;
            normalizedPosition = Vector2.ClampMagnitude(normalizedPosition, 1f);
            float halfWidth = Mathf.Max(0f, _radarRect.rect.width * 0.5f - _edgePadding);
            float halfHeight = Mathf.Max(0f, _radarRect.rect.height * 0.5f - _edgePadding);

            Vector2 blipPosition = new Vector2(
                normalizedPosition.x * halfWidth,
                normalizedPosition.y * halfHeight
            );

            RectTransform blipRect = blip.GetComponent<RectTransform>();
            blipRect.anchoredPosition = blipPosition;

            bool isAbove = localDirection.y > _heightThreshold;
            bool isBelow = localDirection.y < -_heightThreshold;

            blip.AboveArrow.gameObject.SetActive(isAbove);
            blip.BelowArrow.gameObject.SetActive(isBelow);
        }
    }

    private void EnsureBlipCount(int requiredCount)
    {
        while (_blips.Count < requiredCount)
        {
            Image newImage = Instantiate(_blipPrefab, _radarRect, false);
            RadarBlip newBlip = newImage.GetComponent<RadarBlip>();

            if (newBlip == null)
            {
                Destroy(newImage.gameObject);
                return;
            }

            ConfigureBlip(newBlip);
            newBlip.gameObject.SetActive(false);

            _blips.Add(newBlip);
        }
    }

    private void ConfigureBlip(RadarBlip blip)
    {
        RectTransform rect = blip.GetComponent<RectTransform>();

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = Vector2.zero;
    }
}


