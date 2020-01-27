using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextStyler : MonoBehaviour
{
    private TextMeshProUGUI _text;

    #region Init
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _text.color = ColorManager.instance.GetCurrentTheme().MainUiColor;
    }
    #endregion
}
