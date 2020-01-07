using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = PathsManager.SODataPath + "/BulletData")]
public class BulletData : ScriptableObject
{
    [Header("Setup")]
    public int Index = 0;

    [Header("Graphics")]
    public Mesh Mesh;
    public Material Material;

    [Header("Collider")]
    public Vector3 ColliderCenter = Vector3.zero;
    public float ColliderRadius = 1;
    public float ColliderHeight = 1;
    public int ColliderDirection = 1;

    [Header("Physics")]
    public float Mass = 15;

    [Header("Variables")]
    public int Damage = 20;
    public float MovementSpeed = 2000;
}
