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

    // Singleton
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

    // Press N-key to go to next level
    private void DebugControls()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();
        }
    }

    // Whenever any scene loads
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        InstantiatePaddleAndBricks();
        ResetNumBricks();
        _youWonScreen.SetActive(false);
        _gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    private void InstantiatePaddleAndBricks()
    {
        _clonePaddle = Instantiate(_paddle, transform.position, Quaternion.identity);
        Instantiate(_bricksPrefab, transform.position, Quaternion.identity);
    }
    
    // Load level one and set lives to 3
    private void RestartGame()
    {
        SceneManager.LoadScene(0);
        ResetLives();
    }
    
    private void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // Add bonus life on level up
            AddLife();
        } else
        {
            // If on level 3, go back to level 1
            RestartGame();
        }
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
        Invoke(nameof(InstantiatePaddleClone), _resetDelay);
        CheckGameOver();
    }
    
    public void DestroyBrick()
    {
        _bricks--;
        CheckGameOver();
    }
    
    private void CheckGameOver()
    {
        if (_bricks < 1)
        {
            _youWonScreen.SetActive(true);
            Time.timeScale = .25f;
            // Deactivate Ball's collider to avoid losing and winning at same time
            GameObject.FindWithTag("Ball").GetComponent<SphereCollider>().enabled = false;
            Invoke(nameof(LoadNextScene), _resetDelay);
        }

        if (_lives < 1)
        {
            _gameOverScreen.SetActive(true);
            Time.timeScale = .25f;
            Invoke(nameof(RestartGame), _resetDelay);
        }
    }
    
    private void InstantiatePaddleClone()
    {
        _clonePaddle = Instantiate(_paddle, transform.position, Quaternion.identity);
    }
    
    private void ResetNumBricks()
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
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
