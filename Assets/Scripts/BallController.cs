using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _initialVelocity = 600f;
    private Rigidbody _rb;
    private bool _ballInPlay;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !_ballInPlay)
        {
            transform.parent = null;
            _ballInPlay = true;
            _rb.isKinematic = false;
            _rb.AddForce(new Vector3(_initialVelocity, _initialVelocity, 0f));
        }
    }

}
