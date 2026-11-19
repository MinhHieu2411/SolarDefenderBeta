using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/*public class RadarScreen : MonoBehaviour
{
    [SerializeField] Image _blipPrefab;
    [SerializeField] LayerMask _layerMask;
    [SerializeField][Range(100, 5000)] float _radarRange = 500f;
    [SerializeField] int _maxTarget = 6;
    [SerializeField] float _refreshDelay = 0.25f;
    [SerializeField] Image _radarScreen;
    [SerializeField] Transform _player;

    Transform _transform;
    WaitForSeconds _waitForSeconds;
    List<Transform> _targetsInRange;
    float _radarWidth, _radarHeight;
    //Transform _radarTransform;
    RectTransform _radarTransform;
    //Renderer _radarRenderer;
    Vector3 _targetPosition = Vector3.zero;
    //blips
    Vector2 _blipPosition = Vector3.zero;
    Vector3 _localDirection;
    float _angleToTarget;
    float _playerAngle;
    float _radarAngle;
    float _normalozeDistance;
    float _angleRadians;
    float _blipX, _blipY;
    string[] _layerNames;
    float _pitch;
    Collider[] _targerColliders;

    private void Awake()
    {
        if (_radarScreen == null) return;
        _targetsInRange = new List<Transform>();
        _radarTransform = _radarScreen.rectTransform;
        //_radarRenderer = _radarScreen.GetComponent<Renderer>();
        _transform = transform;
    }
    private void OnValidate()
    {
        if (_radarScreen == null) return;
        _targerColliders = new Collider[_maxTarget];
        _waitForSeconds = new WaitForSeconds(_refreshDelay);
        _layerNames = _layerMask.ToString().Split(',');
    }
    private void Start()
    {
        if (_radarRenderer) return;
         
        var bounds = _radarRenderer.bounds;
        _radarWidth = bounds.size.x;
        _radarHeight = bounds.size.y;
        if (_radarTransform == null) return;
        _radarWidth = _radarTransform.rect.width;
        _radarHeight = _radarTransform.rect.height;

    }

    private void OnEnable()
    {
        StartCoroutine(nameof(RefreshTargetList));
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(RefreshTargetList));
    }

    private void LateUpdate()
    {
        DrawTargetBlips();
    }

    IEnumerator RefreshTargetList()
    {
        int size = 0;
        Transform target;
        while (true)
        {
            _targetsInRange.Clear();
            size = Physics.OverlapSphereNonAlloc(_transform.position, _radarRange, _targerColliders, _layerMask);
            for(int i = 0; i < size; ++i)
            {
                target = GetRootTransform(i);
                if (!_targetsInRange.Contains(target))
                {
                    _targetsInRange.Add(target);
                }
            }
            yield return _waitForSeconds;
        }
    }

    void DrawTargetBlips()
    {
        ClearDisplay();
        Vector3 scale;

        foreach (var target in _targetsInRange)
        {
            _targetPosition = (target.position - _player.position) / _radarRange;
            CalculateBlipPosition();

            var blip = Instantiate(_blipPrefab, _radarTransform);
            blip.transform.localScale = _blipPrefab.transform.localScale;
            blip.transform.localPosition = _blipPosition;
            RectTransform blipRect = blip.rectTransform;

            blipRect.anchoredPosition = _blipPosition;
            blipRect.localScale = Vector3.one;


            //if (Mathf.Approximately(0f, _pitch)) return;
            RadarBlip radarBlip = blip.GetComponent<RadarBlip>();
            radarBlip.AboveArrow.enabled = (_pitch < 0f);
            radarBlip.BelowArrow.enabled = (_pitch > 0f);

            /*if(_pitch < 0f)
            {
                scale = radarBlip.AboveArrow.transform.localScale;
                scale.y = Mathf.Clamp(Mathf.Abs(_pitch)/90f, 0f, 1f);
                radarBlip.AboveArrow.transform.localScale = scale;
                return;
            }
            if(_pitch > 0f)
            {
                scale = radarBlip.BelowArrow.transform.localScale;
                scale.y = Mathf.Clamp(Mathf.Abs(_pitch) / 90f, 0f, 1f);
                radarBlip.BelowArrow.transform.localScale = scale;
                return;
            }
        }
    }

    void CalculateBlipPosition()
    {
        _angleToTarget = Mathf.Atan2(_targetPosition.x, _targetPosition.z) * Mathf.Rad2Deg;
        //_localDirection = Quaternion.Inverse(_player.rotation) * (_targetPosition - _player.position);
        _localDirection = Quaternion.Inverse(_player.rotation) * _targetPosition;
        _pitch = Vector3.Angle(Vector3.down, _localDirection) - 90f;
        _playerAngle = _player.eulerAngles.y;
        _radarAngle = _angleToTarget - _playerAngle - 90f;
        _normalozeDistance = _targetPosition.magnitude;
        _angleRadians = _radarAngle * Mathf.Deg2Rad;
        _blipX = _normalozeDistance * Mathf.Cos(_angleRadians);
        _blipY = _normalozeDistance * Mathf.Sin(_angleRadians);
        _blipPosition.x = _blipX * (_radarWidth * 0.25f);
        _blipPosition.y = _blipY * (_radarHeight * 0.25f);
    }

    private void ClearDisplay()
    {
        while(_radarTransform.childCount > 0)
        {
            var child = _radarTransform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    private Transform GetRootTransform(int i)
    {
        Transform root = _targerColliders[i].transform;
        while (root.parent && _layerNames.Contains(LayerMask.LayerToName(root.parent.gameObject.layer)))
        {
            root = root.parent;
        }
        return root;
    }
}*/
public class RadarScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image _radarScreen;
    [SerializeField]  Image _blipPrefab;
    [SerializeField]  Transform _player;
    [SerializeField]  LayerMask _targetLayers;

    [Header("Radar Settings")]
    [SerializeField, Min(1f)]  float _radarRange = 500f;
    [SerializeField, Min(0.02f)]  float _refreshInterval = 0.25f;
    [SerializeField, Min(1)]  int _maxColliders = 64;
    [SerializeField, Min(0f)]  float _heightThreshold = 1f;
    [SerializeField, Min(0f)]  float _edgePadding = 8f;

     RectTransform _radarRect;
     Collider[] _hits;

    List<Transform> _targets = new List<Transform>();
    HashSet<Transform> _uniqueTargets = new HashSet<Transform>();
    List<RadarBlip> _blips = new List<RadarBlip>();

    private float _nextRefreshTime;

    private void Awake()
    {
        if (_radarScreen == null || _blipPrefab == null || _player == null)
        {
            Debug.LogError("RadarScreen: Missing references!");
            enabled = false;
            return;
        }

        _radarRect = _radarScreen.rectTransform;
        _hits = new Collider[Mathf.Max(1, _maxColliders)];

        RadarBlip template = _blipPrefab.GetComponent<RadarBlip>();

        if (template == null)
        {
            Debug.LogError("RadarBlip prefab is missing RadarBlip.cs!");
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

        int count = Physics.OverlapSphereNonAlloc(
            _player.position,
            _radarRange,
            _hits,
            _targetLayers,
            QueryTriggerInteraction.Collide
        );

        for (int i = 0; i < count; i++)
        {
            Collider hit = _hits[i];

            if (hit == null)
                continue;
            Transform target = hit.attachedRigidbody != null
                ? hit.attachedRigidbody.transform
                : hit.transform.root;

            if (target == _player || target.IsChildOf(_player))
                continue;

            if (_uniqueTargets.Add(target))
                _targets.Add(target);
        }
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

            Vector3 localDirection = _player.InverseTransformDirection(
                target.position - _player.position
            );

            Vector2 horizontalDirection = new Vector2(
                localDirection.x,
                localDirection.z
            );

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
                Debug.LogError("Created RadarBlip is missing RadarBlip.cs!");
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


