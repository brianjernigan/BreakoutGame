using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject _brickParticles;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_brickParticles, transform.position, Quaternion.identity);
        GameManager.Instance.DestroyBrick();
        Destroy(gameObject);
    }
}
