using UnityEngine;
public class HeadingIndicator : MonoBehaviour
{
    [SerializeField]  Transform _playerShip;
    [SerializeField]  Camera _mainCamera;
    [SerializeField]  RectTransform _headingUI;

    [SerializeField]  float _indicatorDistance = 200f;

    private void LateUpdate()
    {
        if (_playerShip == null || _mainCamera == null || _headingUI == null)
            return;

        Vector3 targetPosition =
            _playerShip.position +
            _playerShip.forward * _indicatorDistance;

        Vector3 screenPosition =
            _mainCamera.WorldToScreenPoint(targetPosition);

        if (screenPosition.z <= 0)
        {
            _headingUI.gameObject.SetActive(false);
            return;
        }

        _headingUI.gameObject.SetActive(true);
        _headingUI.position = screenPosition;
    }
}
