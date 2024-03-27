using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int _lives = 3;
    [SerializeField] private int _bricks = 20;
    [SerializeField] private float _resetDelay = 1f;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text _livesText;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _youWonScreen;

    [Header("Game Objects")]
    [SerializeField] private GameObject _bricksPrefab;
    [SerializeField] private GameObject _paddle;

    private GameObject _clonePaddle;

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoad;
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        DebugControls();
    }

    private void DebugControls()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();
        }
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        InitialSetup();
        ResetBricks();
        _youWonScreen.SetActive(false);
        _gameOverScreen.SetActive(false);
    }

    private void InitialSetup()
    {
        _clonePaddle = Instantiate(_paddle, transform.position, Quaternion.identity);
        Instantiate(_bricksPrefab, transform.position, Quaternion.identity);
    }

    private void ResetBricks()
    {
        _bricks = 20;
    }

    private void ResetLives()
    {
        _lives = 3;
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        _livesText.text = $"Lives: {_lives}";
    }

    private void CheckGameOver()
    {
        if (_bricks < 1)
        {
            _youWonScreen.SetActive(true);
            Time.timeScale = .25f;
            GameObject.FindWithTag("Ball").GetComponent<SphereCollider>().enabled = false;
            Invoke("LoadNextScene", _resetDelay);
        }

        if (_lives < 1)
        {
            _gameOverScreen.SetActive(true);
            Time.timeScale = .25f;
            Invoke("RestartGame", _resetDelay);
        }
    }

    private void Reset()
    {
        Time.timeScale = 1f;
        ResetLives();
        ReloadScene();
    }

    private void AddLife()
    {
        _lives++;
        UpdateLivesText();
    }

    public void LoseLife()
    {
        _lives--;
        UpdateLivesText();
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

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            AddLife();
        } else
        {
            RestartGame();
        }

        Time.timeScale = 1f;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
        ResetLives();
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
