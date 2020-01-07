using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [HideInInspector]
    public PlayerData Data;

    [Header("Bullet")]
    public GameObject BulletPrefab;
    private int _bulletTypeIndex = 0;

    private List<GameObject> _turrets = new List<GameObject>();

    private Rigidbody _rb;

    #region Init
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        ExtractData();
    }

    private void Start()
    {
        for(int i = 0; i < Data.Turrets.Count; i++)
        {
            GameObject _turret = new GameObject("Turret_" + i);
            _turret.transform.parent = this.gameObject.transform.Find("Turrets");
            _turret.transform.position = transform.position + Data.Turrets[i];
            _turret.transform.rotation = transform.rotation;
            _turrets.Add(_turret);
        }
    }

    #region ExtractData
    private void ExtractData()
    {
        ExtractGraphicsData();
        ExtractColliderData();
        ExtractRigidbodyData();
    }

    private void ExtractGraphicsData()
    {
        Transform _graphics = transform.Find("Graphics");

        MeshFilter _meshFilter = _graphics.GetComponent<MeshFilter>();
        MeshRenderer _meshRenderer = _graphics.GetComponent<MeshRenderer>();

        _meshFilter.mesh = Data.Mesh;
        _meshRenderer.material = Data.Material;
    }

    private void ExtractColliderData()
    {
        BoxCollider _boxCollider = GetComponent<BoxCollider>();

        _boxCollider.center = Data.ColliderCenter;
        _boxCollider.size = Data.ColliderSize;
    }

    private void ExtractRigidbodyData()
    {
        _rb.mass = Data.Mass;
        _rb.drag = Data.Drag;
        _rb.angularDrag = Data.AngularDrag;
    }
    #endregion

    #endregion

    #region Update
    private void FixedUpdate()
    {
        float _vertical = Input.GetAxis("Vertical");
        float _horizontal = Input.GetAxis("Horizontal");

        Move(_vertical);
        Rotate(_horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }
    #endregion

    private void Move(float force)
    {
        _rb.AddRelativeForce(Vector3.forward * Data.MovementSpeed * force * Time.fixedDeltaTime);
    }

    private void Rotate(float force)
    {
        _rb.AddTorque(Vector3.up * Data.RotationSpeed * force * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        foreach (GameObject turret in _turrets) {
            GameObject _bullet = BulletPrefab;
            _bullet.GetComponent<Bullet>().Data = GameManager.instance.BulletDataList[_bulletTypeIndex];
            Instantiate(_bullet, turret.transform.position, turret.transform.rotation);
        }
    }
}
