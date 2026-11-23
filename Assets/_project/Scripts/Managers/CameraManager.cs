
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraManager : MonoBehaviour
{
    [SerializeField] Canvas _hud;
    enum VirtualCameras
    {
        NoCamera = -1,
        CockpitCamera = 0,
        FollowCamera = 1,
        RearCamera = 2
    }

    [Header("Virtual Cameras")] [SerializeField] List<GameObject> _virtualCameras;
    //[Header("Virtual Cameras")][SerializeField] List<CinemachineVirtualCamera> _virtualCameras;
    public Transform ActiveCamera {  get; private set; }
    public UnityEvent ActiveCameraChanged;
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
     void Awake()
    {
        ActiveCameraChanged = new UnityEvent();
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
            //_hud.enabled = false;
        }
        else
        {
            SetActiveCamera(_currentCamera);
            _hud.enabled = true;
        }

    }

    void SetActiveCamera(VirtualCameras activeCamera)
    {
        VirtualCameras camIndex = VirtualCameras.CockpitCamera;
        foreach (var cam in _virtualCameras)
        {
            if(camIndex++ == activeCamera)
            {
                cam.gameObject.SetActive(true);
                ActiveCamera = cam.transform;
                ActiveCameraChanged.Invoke();
            }
            else
            {
                cam.gameObject.SetActive(false);
            }
        }
    }
}
