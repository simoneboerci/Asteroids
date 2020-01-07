using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Borders")]
    public List<Transform> Borders;

    [Header("Pool")]
    public Transform AsteroidPool;

    [Header("Player")]
    private Player _player;
    public int SelectedPlayerIndex = 0;
    public List<PlayerData> PlayerDataList;

    [Header("Asteroids")]
    public GameObject AsteroidPrefab;
    public float AsteroidSpawnRate;
    private float _asteroidSpawnPositionOffset = 2;
    private float _asteroidSpawnPositionMargin = 5;
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

        _player = FindObjectOfType<Player>();
        _player.Data = PlayerDataList[SelectedPlayerIndex];
    }
    #endregion

    #region Update
    private void Update()
    {
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

        SetAsteroidData(_asteroidScript);

        Transform _selectedBorder = GetRandomBorder();

        Vector3 _spawnPosition = CalculateSpawnPosition(_selectedBorder, _asteroidScript);

        InstantiateAsteroid(_asteroid, _spawnPosition);
    }

    private void SetAsteroidData(Asteroid asteroid)
    {
        asteroid.Data = AsteroidDataList[Random.Range(0, AsteroidDataList.Count)];
    }

    private Transform GetRandomBorder()
    {
        int _borderPositionIndex = Random.Range(0, Borders.Count);

        return Borders[_borderPositionIndex];
    }

    private Vector3 CalculateSpawnPosition(Transform selectedBorder, Asteroid asteroid)
    {
        Vector3 _spwanPositionOffset = Vector3.zero;
        Vector3 _direction = Vector3.zero;
        float _angleOffset = Random.Range(_asteroidSpawnAngleMin, _asteroidSpawnAngleMax);
        Vector3 _directionOffset = Vector3.zero;

        switch (selectedBorder.name)
        {
            case "TopBorder":
                _spwanPositionOffset = new Vector3(0, 0, -_asteroidSpawnPositionOffset);
                _direction = Vector3.back;
                _directionOffset = new Vector3(_angleOffset, 0, 0);
                break;
            case "BottomBorder":
                _spwanPositionOffset = new Vector3(0, 0, _asteroidSpawnPositionOffset);
                _direction = Vector3.forward;
                _directionOffset = new Vector3(_angleOffset, 0, 0);
                break;
            case "LeftBorder":
                _spwanPositionOffset = new Vector3(_asteroidSpawnPositionOffset, 0, 0);
                _direction = Vector3.right;
                _directionOffset = new Vector3(0, 0, _angleOffset);
                break;
            case "RightBorder":
                _spwanPositionOffset = new Vector3(-_asteroidSpawnPositionOffset, 0, 0);
                _direction = Vector3.left;
                _directionOffset = new Vector3(0, 0, _angleOffset);
                break;
        }
        asteroid.InitialDirection = Vector3.Normalize(_direction + _directionOffset);

        return selectedBorder.position + _spwanPositionOffset;
    }

    private void InstantiateAsteroid(GameObject asteroid, Vector3 position)
    {
        Instantiate(asteroid, position, Quaternion.identity, AsteroidPool);
    }
    #endregion
}