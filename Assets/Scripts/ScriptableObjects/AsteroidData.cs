using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = PathsManager.SODataPath + "/AsteroidData")]
public class AsteroidData : ScriptableObject
{
    [Header("Setup")]
    public int Index;

    [Header("Graphics")]
    public Mesh Mesh;
    public Material Material;
    [Range(0.1f, 1)]
    public float MinSize = 0.3f;
    [Range(0.1f, 1)]
    public float MaxSize = 1;

    [Header("Collider")]
    public Vector3 ColliderCenter = Vector3.zero;
    public float ColliderRadius = 1;
    public float ColliderHeight = 1;
    public int ColliderDirection = 1;

    [Header("Physics")]
    public float Mass = 100000;

    [Header("Variables")]
    public int Life = 100;
    public int Damage = 1;
    public int Score = 10;
    public float MaxMovementSpeed = 10000000;
    public float MaxRotationSpeed = 1000000;
}