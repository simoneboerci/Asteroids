using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("PostProcessing")]
    public PostProcessProfile DefaultPostProcess;
    public PostProcessProfile PausePostProcess;
    private PostProcessVolume _postProcessVolume;

    [HideInInspector]
    public bool Pause;
    [HideInInspector]
    public bool GameOverBool;

    private int _score = 0;

    [Header("Borders")]
    public List<Transform> Borders;

    [Header("Pool")]
    public Transform AsteroidPool;

    [Header("Player")]
    public int SelectedPlayerIndex = 0;
    public List<PlayerData> PlayerDataList;
    private Player _player;

    [Header("Asteroids")]
    public GameObject AsteroidPrefab;
    public float AsteroidSpawnRate;
    public float BorderOffset = 2;
    private float _asteroidSpawnAngleMin = -1.5f;
    private float _asteroidSpawnAngleMax = 1.5f;
    public List<AsteroidData> AsteroidDataList;

    [Header("Bullet")]
    public List<BulletData> BulletDataList;
    
    private float _time = 0;

    #region Init
    private void Awake()
    {
        if (instance == null || instance != this)
            instance = this;

        _postProcessVolume = FindObjectOfType<PostProcessVolume>();

        _postProcessVolume.profile = DefaultPostProcess;

        _player = FindObjectOfType<Player>();
        _player.Data = PlayerDataList[SelectedPlayerIndex];

        UIManager.instance.CreatingLives(_player.Data.Life);

        GameOverBool = false;
    }
    #endregion

    #region Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (Pause)
                ResumeGame();
            else
                PauseGame();
        }

        if (Pause || GameOverBool)
            return;

        _time += Time.deltaTime;

        if(_time >= AsteroidSpawnRate)
        {
            SpawnAsteroid();
            _time = 0;
        }
    }
    #endregion

    #region SpawnAsteroid
    private void SpawnAsteroid()
    {
        GameObject _asteroid = AsteroidPrefab;
        Asteroid _asteroidScript = _asteroid.GetComponent<Asteroid>();

        _asteroidScript.Data = GetAsteroidData();

        Transform _selectedBorder = GetRandomBorder();

        Vector3 _spawnPosition = CalculateSpawnPosition(_selectedBorder, _asteroidScript);

        InstantiateAsteroid(_asteroid, _spawnPosition);
    }

    public AsteroidData GetAsteroidData()
    {
        return AsteroidDataList[Random.Range(0, AsteroidDataList.Count)];
    }

    private Transform GetRandomBorder()
    {
        int _borderPositionIndex = Random.Range(0, Borders.Count);

        return Borders[_borderPositionIndex];
    }

    private Vector3 CalculateSpawnPosition(Transform selectedBorder, Asteroid asteroid)
    {
        Vector3 _spawnPositionOffset = Vector3.zero;
        Vector3 _direction = Vector3.zero;
        Vector3 _directionOffset = Vector3.zero;
        float _angleOffset = Random.Range(_asteroidSpawnAngleMin, _asteroidSpawnAngleMax);

        switch (selectedBorder.name)
        {
            case "TopBorder":
                _spawnPositionOffset = new Vector3(0, 0, -BorderOffset);
                _direction = Vector3.back;
                _directionOffset = new Vector3(_angleOffset, 0, 0);
                break;
            case "BottomBorder":
                _spawnPositionOffset = new Vector3(0, 0, BorderOffset);
                _direction = Vector3.forward;
                _directionOffset = new Vector3(_angleOffset, 0, 0);
                break;
            case "LeftBorder":
                _spawnPositionOffset = new Vector3(BorderOffset, 0, 0);
                _direction = Vector3.right;
                _directionOffset = new Vector3(0, 0, _angleOffset);
                break;
            case "RightBorder":
                _spawnPositionOffset = new Vector3(-BorderOffset, 0, 0);
                _direction = Vector3.left;
                _directionOffset = new Vector3(0, 0, _angleOffset);
                break;
        }
        asteroid.InitialDirection = Vector3.Normalize(_direction + _directionOffset);

        return selectedBorder.position + _spawnPositionOffset;
    }

    private void InstantiateAsteroid(GameObject asteroid, Vector3 position)
    {
        Instantiate(asteroid, position, Quaternion.identity, AsteroidPool);
    }
    #endregion

    public void AddScore(Asteroid asteroid)
    {
        _score += asteroid.Data.Score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        UIManager.instance.UpdateScoreText(_score);
    }

    public void PauseGame()
    {
        Pause = true;
        Time.timeScale = 0;
        _postProcessVolume.profile = PausePostProcess;
        UIManager.instance.SetPause(Pause);
    }

    public void ResumeGame()
    {
        Pause = false;
        Time.timeScale = 1;
        _postProcessVolume.profile = DefaultPostProcess;
        UIManager.instance.SetPause(Pause);
    }

    public void GameOver()
    {
        GameOverBool = true;
        SoundManager.instance.Stop();
        SceneManager.instance.LoadMainMenu();
    }

    #region Utils
    public Transform GetBorderByName(string name)
    {
        foreach(Transform border in Borders)
        {
            if (border.name == name)
                return border;
        }

        return null;
    }
    #endregion
}