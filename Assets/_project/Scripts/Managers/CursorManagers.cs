using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagers : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        _rectTransform.position = Input.mousePosition;
    }
}
