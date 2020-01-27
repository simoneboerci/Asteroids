using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = PathsManager.SOPath + "/Theme")]
public class Theme : ScriptableObject
{
    [Header("UI")]
    public Color MainUiColor = Color.white;
    public Color NormalButtonColor;
    public Color HighlightedButtonColor;
    public Color PressedButtonColor;
    public Color SelectedButtonColor;
    public Color DisabledButtonColor;
    public float ColorTransitionSpeed = 0.1f;
    public float DisabledButtonTextOpacity = 0.3f;
}
