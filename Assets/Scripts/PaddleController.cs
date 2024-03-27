using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private const float YPos = -9.5f;
    private const float HorizontalBoundary = 8.0f;
    private const float SmoothFactor = 5.0f;

    [SerializeField] private float _paddleSpeed = 100.0f;

    private float targetXPos;

    private void Start()
    {
        targetXPos = transform.position.x;
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        targetXPos += horizontalInput * _paddleSpeed * Time.deltaTime;
        targetXPos = Mathf.Clamp(targetXPos, -HorizontalBoundary, HorizontalBoundary);

        var smoothedPos = Mathf.Lerp(transform.position.x, targetXPos, SmoothFactor * Time.deltaTime);
        transform.position = new Vector3(smoothedPos, YPos, 0f);
    }
}
