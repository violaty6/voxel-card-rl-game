using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _speed= 25f;
    [SerializeField] private float _smoothing = 5f;
    [SerializeField] private Vector2 _range = new Vector2(30, 70);
    [SerializeField] private Transform _cameraHolder;
    private DefaultInputActions dplayerActions;

    private Vector3 _cameraDirection => transform.InverseTransformDirection(_cameraHolder.forward);

    private Vector3 _targetPosition;
    private float _input;

    private void Awake()
    {
        _targetPosition = _cameraHolder.localPosition;
        dplayerActions = new DefaultInputActions();
        dplayerActions.UI.ScrollWheel.Enable();
    }


    private void Update()
    {
        HandleInput();
        Zoom();
    }

    private void HandleInput()
    {
        _input = dplayerActions.UI.ScrollWheel.ReadValue<Vector2>().y/1200;
    }

    private void Zoom()
    {
        Vector3 nextTargetPosition = _targetPosition + _cameraDirection * (_input * _speed);
        if (IsInBounds(nextTargetPosition))
        {
            _targetPosition = nextTargetPosition;
        }

        _cameraHolder.localPosition = Vector3.Lerp(_cameraHolder.localPosition, _targetPosition, Time.deltaTime * _smoothing);
    }

    private bool IsInBounds(Vector3 position)
    {
        return position.magnitude > _range.x && position.magnitude < _range.y;
    }

}
