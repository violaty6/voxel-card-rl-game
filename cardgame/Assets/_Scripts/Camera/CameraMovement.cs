using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed= 1f;
    [SerializeField] private float _smoothing = 5f;
    [SerializeField] private Vector2 _range = new Vector2(100, 100);
    private DefaultInputActions dplayerActions;
    public Vector3 _targetPosition;
    private Vector3 _input;
    void Start()
    {
        _targetPosition = transform.position; 
        dplayerActions = new DefaultInputActions();
        dplayerActions.Player.Move.Enable();
    }
    void Update()
    {
        HandleMovementInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 newTargetPosition = _targetPosition + _input * _speed;
        Debug.Log(IsInBounds(newTargetPosition));
        if (IsInBounds(newTargetPosition)) _targetPosition = newTargetPosition;
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothing);
    }

    bool IsInBounds(Vector3 position)
    {
        return position.x > -_range.x &&
               position.x < _range.x &&
               position.z > -_range.y &&
               position.z < _range.y;
    }
    void HandleMovementInput()
    {
        float x = dplayerActions.Player.Move.ReadValue<Vector2>().x;
        float z = dplayerActions.Player.Move.ReadValue<Vector2>().y;
        // Debug.Log(x+" "+ z);

        Vector3 right = transform.right * x;
        Vector3 forward = transform.forward * z;
        _input = (forward + right).normalized;
    }

    private void OnDisable()
    {
        dplayerActions.Player.Move.Disable();
    }
}
