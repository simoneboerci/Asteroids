using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public BulletData Data;

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
    }

    private void ExtractGraphicsData()
    {
        Transform _graphics = transform.Find("Graphics");

        MeshFilter _meshFilter = _graphics.GetComponent<MeshFilter>();
        MeshRenderer _meshRenderer = _graphics.GetComponent<MeshRenderer>();

        _meshFilter.mesh = Data.Mesh;
        _meshRenderer.material = Data.Material;

        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); //TEMP
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
        _rb.AddForce(transform.forward * Data.MovementSpeed);
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
            Die();
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
