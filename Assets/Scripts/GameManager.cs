using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _lives = 3;
    [SerializeField] private int _bricks = 20;
    [SerializeField] private float _resetDelay = 1f;
    [SerializeField] private TMP_Text _livesText;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _youWon;
    [SerializeField] private GameObject _bricksPrefab;
    [SerializeField] private GameObject _paddle;
    private GameObject _clonePaddle;

    private void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        _clonePaddle = Instantiate(_paddle, transform.position, Quaternion.identity);
        Instantiate(_bricksPrefab, transform.position, Quaternion.identity);
    }

    private void CheckGameOver()
    {
        if (_bricks < 1)
        {
            _youWon.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", _resetDelay);
        }

        if (_lives < 1)
        {
            _gameOver.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", _resetDelay);
        }
    }

    private void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoseLife()
    {
        _lives--;
        _livesText.text = "Lives: " + _lives;
        Destroy(_clonePaddle);
        Invoke("SetupPaddle", _resetDelay);
        CheckGameOver();
    }

    private void SetupPaddle()
    {
        _clonePaddle = Instantiate(_paddle, transform.position, Quaternion.identity);
    }

    public void DestroyBrick()
    {
        _bricks--;
        CheckGameOver();
    }
}
