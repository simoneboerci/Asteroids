using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = PathsManager.SOPath + "/Audio")]
public class Audio : ScriptableObject
{
    public AudioClip Clip;
    public float Volume = 1f;
    public bool Loop = false;
}
