using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] Image _targetBox, _lockOnTarget, _offScreenTarget;
    [SerializeField] float _offScreenMargin = 45f;

    Transform _target;
    Canvas _mainCanvas;
    Camera _mainCamera;
    RectTransform _rectTransform, _canvasRect;
    Vector3 _screenCenter = Vector3.zero;
    Vector3 ScreenCentert
    {
        get
        {
            var rect = _canvasRect.rect;
            _screenCenter.x = rect.width * 0.5f ;
            _screenCenter.y = rect.height * 0.5f;
            _screenCenter.z = 0f;
            return _screenCenter * _canvasRect.localScale.x;
        }
    }
    Camera CanvasCamera
    {
        get
        {
            return _mainCanvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : _mainCanvas.worldCamera;
        }
    }
    public int Key { get;  set;  }
    public bool LockedOn {  get;  set; }
     void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    private void LateUpdate()
    {
        if (!_target || !_mainCamera)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 targetViewportPos = _mainCamera.WorldToViewportPoint(_target.position);

        if (TargetIsVisible(targetViewportPos))
        {
            DisplayOnScreenRecticle(targetViewportPos);
            return;
        }

        DisplayOffScreenRecticle(targetViewportPos);
    }
    public void Init(Transform target, Canvas mainCanvas)
    {
        if (target == null || mainCanvas == null)
        {
            gameObject.SetActive(false);
            return;
        }

        _target = target;
        Key = target.GetInstanceID();

        _mainCanvas = mainCanvas;
        _canvasRect = mainCanvas.GetComponent<RectTransform>();
        _mainCamera = Camera.main;

        if (_canvasRect == null || _mainCamera == null)
        {
            gameObject.SetActive(false);
            return;
        }
    }
    bool TargetIsVisible(Vector3 position)
    {
        return (position.x is >= 0f and <= 1f && position.y is >= 0f and <= 1f && position.z >= 0f);
    }
     void DisplayOnScreenRecticle(Vector3 targetViewportPos)
    {
        /*Vector3 position = _mainCamera.ViewportToScreenPoint(targetViewportPos);
        position.z = 0;
        _rectTransform.position = position;
        _targetBox.enabled = !LockedOn;
        _lockOnTarget.enabled = LockedOn;
        _offScreenTarget.enabled = false;*/

        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(_target.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, screenPosition, CanvasCamera, out Vector2 localPosition);
        _rectTransform.anchoredPosition = localPosition;
        _targetBox.enabled = !LockedOn;
        _lockOnTarget.enabled = LockedOn;
        _offScreenTarget.enabled = false;
    }
     void DisplayOffScreenRecticle(Vector3 targetViewportPos)
    {
        Vector2 direction = new Vector2(targetViewportPos.x - 0.5f,targetViewportPos.y - 0.5f );
        if (targetViewportPos.z < 0f)
        {
            direction = -direction;
        }
        if (direction.sqrMagnitude < 0.0001f)
        {
            direction = Vector2.up;
        }
        direction.Normalize();
        float halfWidth = _canvasRect.rect.width * 0.5f - _offScreenMargin;
        float halfHeight = _canvasRect.rect.height * 0.5f - _offScreenMargin;
        float scaleX = Mathf.Abs(direction.x) > 0.0001f ? halfWidth / Mathf.Abs(direction.x) : float.PositiveInfinity;
        float scaleY = Mathf.Abs(direction.y) > 0.0001f ? halfHeight / Mathf.Abs(direction.y) : float.PositiveInfinity;
        float scale = Mathf.Min(scaleX, scaleY);
        Vector2 localPosition = direction * scale;
        _rectTransform.anchoredPosition = localPosition;
        _targetBox.enabled = false;
        _lockOnTarget.enabled = false;
        _offScreenTarget.enabled = true;
        _offScreenTarget.rectTransform.rotation =Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
    }

}
