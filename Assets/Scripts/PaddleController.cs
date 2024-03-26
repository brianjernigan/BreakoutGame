using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private const float YPos = -9.5f;
    private const float HorizontalBoundary = 8f;

    [SerializeField] private float _paddleSpeed = 1.0f;
    private Vector3 _playerPos = new Vector3(0f, YPos, 0f);

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var xPos = transform.position.x + horizontalInput * _paddleSpeed;
        _playerPos = new Vector3(Mathf.Clamp(xPos, -HorizontalBoundary, HorizontalBoundary), YPos, 0f);
        transform.position = _playerPos;
    }
}
