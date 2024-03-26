using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float _paddleSpeed = 1.0f;
    private Vector3 _playerPos = new Vector3(0f, -9.5f, 0f);

    private void Update()
    {
        var xPos = transform.position.x + (Input.GetAxis("Horizontal") * _paddleSpeed);
        _playerPos = new Vector3(Mathf.Clamp(xPos, -8f, 8f), -9f, 0f);
        transform.position = _playerPos;
    }
}
