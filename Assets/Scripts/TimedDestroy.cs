using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    [SerializeField] private float _destroyTime = 1.0f;

    private void Start()
    {
        Destroy(gameObject, _destroyTime);
    }
}
