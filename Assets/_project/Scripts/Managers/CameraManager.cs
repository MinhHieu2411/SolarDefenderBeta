
using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    enum VirtualCameras
    {
        NoCamera = -1,
        CockpitCamera = 0,
        FollowCamera = 1,
        RearCamera = 2
    }

    [Header("Virtual Cameras")]
    [SerializeField] List<GameObject> _virtualCameras;

    VirtualCameras _currentCamera = VirtualCameras.CockpitCamera;

    bool _isLookingBack = false;

    VirtualCameras CameraKeyPressed
    {
        get
        {
            for (int i = 0; i < _virtualCameras.Count && i < 2; ++i)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                    return (VirtualCameras)i;
            }

            return VirtualCameras.NoCamera;
        }
    }

    void Start()
    {
        SetActiveCamera(_currentCamera);
    }

    void Update()
    {
        //main camera
        VirtualCameras selectedCamera = CameraKeyPressed;

        if (selectedCamera != VirtualCameras.NoCamera)
        {
            _currentCamera = selectedCamera;
        }

        //rear camera
        _isLookingBack = Input.GetKey(KeyCode.LeftControl);

        if (_isLookingBack)
        {
            //SetActiveCamera(VirtualCameras.FollowBackCamera);
            SetActiveCamera(VirtualCameras.RearCamera);
        }
        else
        {
            SetActiveCamera(_currentCamera);
        }
    }

    void SetActiveCamera(VirtualCameras activeCamera)
    {
        foreach (GameObject cam in _virtualCameras)
        {
            cam.SetActive(cam.CompareTag(activeCamera.ToString()));
        }
    }
}
