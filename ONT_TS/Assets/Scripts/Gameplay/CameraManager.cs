using System;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public InputReader _inputReader;

    public Camera _mainCam;

    public CinemachineFreeLook _freeCam;

    public ObjectPositionSO _playerPos;

    [SerializeField, Range(.5f, 3f)]
    private float _speedMultiplier = 1f; //Use to modify in game setting

    public void SetupVirtualCamera(Transform target){
        _freeCam.Follow = target;
        _freeCam.LookAt = target;
    }

    private void OnEnable() {
        _inputReader.RotateCameraEvent += OnCameraMove;
    }
    private void OnDisable() {
        _inputReader.RotateCameraEvent -= OnCameraMove;
    }

    private void Start() {
        SetupVirtualCamera(_playerPos.Transform);
    }

    private void OnCameraMove(Vector2 cameraMovement, bool isDeviceMouse)
    {
        if(!isDeviceMouse) return;
        _freeCam.m_XAxis.m_InputAxisValue = cameraMovement.x * _speedMultiplier * Time.deltaTime;
        _freeCam.m_YAxis.m_InputAxisValue = cameraMovement.y * _speedMultiplier * Time.deltaTime;
    }
}
