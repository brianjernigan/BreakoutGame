using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private const float YPos = -9.5f;
    private const float HorizontalBoundary = 8.0f;
    
    private const float SmoothFactor = 7.0f;
    private const float PaddleSpeed = 27.5f;

    private float _targetXPos;

    private void Start()
    {
        _targetXPos = transform.position.x;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        _targetXPos += horizontalInput * PaddleSpeed * Time.deltaTime;
        
        var smoothedPos = Mathf.Lerp(transform.position.x, _targetXPos, SmoothFactor * Time.deltaTime);
        smoothedPos = Mathf.Clamp(smoothedPos, -HorizontalBoundary, HorizontalBoundary);

        if (Math.Abs(smoothedPos - (-HorizontalBoundary)) < Mathf.Epsilon || Math.Abs(smoothedPos - HorizontalBoundary) < Mathf.Epsilon)
        {
            _targetXPos = smoothedPos;
        }
        
        transform.position = new Vector3(smoothedPos, YPos, 0f);
    }
}
