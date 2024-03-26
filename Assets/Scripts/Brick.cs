using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject _brickParticles;
    private GameManager _gm;

    private void Start()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_brickParticles, transform.position, Quaternion.identity);
        _gm.DestroyBrick();
        Destroy(gameObject);
        Debug.Log("test");
    }
}
