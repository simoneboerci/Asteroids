using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    [HideInInspector]
    public AsteroidData Data;

    private int _life;
    private bool _child;

    [HideInInspector]
    public Vector3 InitialDirection;

    private Rigidbody _rb;

    #region Init
    private void Awake()
    {
        GetComponents();
        ExtractData();
    }

    private void GetComponents()
    {
        _rb = GetComponent<Rigidbody>();
    }

    #region ExtractData
    private void ExtractData()
    {
        ExtractGraphicsData();
        ExtractColliderData();
        ExtractPhysicsData();

        if(!_child)
            _life = Data.Life;
    }

    private void ExtractGraphicsData()
    {
        Transform _graphics = transform.Find("Graphics");

        MeshFilter _meshFilter = _graphics.GetComponent<MeshFilter>();
        MeshRenderer _meshRenderer = _graphics.GetComponent<MeshRenderer>();

        _meshFilter.mesh = Data.Mesh;
        _meshRenderer.material = Data.Material;  

        if (!_child)
        {
            float _scale = Random.Range(Data.MinSize, Data.MaxSize);
            transform.localScale = new Vector3(_scale, _scale, _scale);
        }
    }

    private void ExtractColliderData()
    {
        CapsuleCollider _capsuleCollider = GetComponent<CapsuleCollider>();

        _capsuleCollider.center = Data.ColliderCenter;
        _capsuleCollider.direction = Data.ColliderDirection;
        _capsuleCollider.radius = Data.ColliderRadius;
        _capsuleCollider.height = Data.ColliderHeight;
    }

    private void ExtractPhysicsData()
    {
        _rb.mass = Data.Mass;
    }
    #endregion

    private void Start()
    {
        Vector3 _rotationSpeed = Vector3.up * Random.Range(-Data.MaxRotationSpeed, Data.MaxRotationSpeed);

        float _movementSpeed = Random.Range(Data.MaxMovementSpeed / 100 * 50, Data.MaxMovementSpeed);
        Vector3 _force = InitialDirection * _movementSpeed;

        _rb.AddForce(_force);
        _rb.AddTorque(_rotationSpeed);
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            int _damage = collision.gameObject.GetComponent<Bullet>().Data.Damage;
            if (_life - _damage > 0)
            {
                _life -= _damage;
                transform.localScale /= 2f;
                SpawnMiniAsteroid(_damage);
            }
            else
            {
                GameManager.instance.AddScore(this);
                Die();
            }

        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void SpawnMiniAsteroid(int damage)
    {
        GameObject _asteroid = GameManager.instance.AsteroidPrefab;
        Asteroid _asteroidScript = _asteroid.GetComponent<Asteroid>();

        _asteroidScript.Data = GameManager.instance.GetAsteroidData();

        _asteroidScript.InitialDirection = transform.forward;

        GameObject _child = Instantiate(_asteroid, transform.position, transform.rotation, GameManager.instance.AsteroidPool);

        _child.GetComponent<Asteroid>()._child = true;
        _child.transform.localScale = transform.localScale;
        _child.GetComponent<Asteroid>()._life = _life;
    }
}