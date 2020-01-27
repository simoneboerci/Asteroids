using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = PathsManager.SODataPath + "/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Setup")]
    public int Index = 0;

    [Header("Graphics")]
    public Mesh Mesh;
    public Material Material;

    [Header("Collider")]
    public Vector3 ColliderCenter = Vector3.zero;
    public Vector3 ColliderSize = Vector3.one;

    [Header("Physics")]
    public float Mass = 550000;
    public float Drag = 0;
    public float AngularDrag = 0;

    [Header("Variables")]
    public int Life = 3;
    public float MovementSpeed = 60;
    public float RotationSpeed = 20;
    public List<Vector3> Turrets;
}
