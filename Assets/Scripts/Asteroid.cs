using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Variables")]
    public float movementSpeed;
    public float rotationSpeed;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(Vector3.left * movementSpeed * Time.fixedDeltaTime);
        _rb.AddTorque(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
    }
}
