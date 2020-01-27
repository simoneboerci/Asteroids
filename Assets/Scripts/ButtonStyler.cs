using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class ButtonStyler : MonoBehaviour
{
    private Button _button;
    private TextMeshProUGUI _text;

    #region Init

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        ApplyStyle();
    }

    private void ApplyStyle()
    {
        Theme _theme = ColorManager.instance.GetCurrentTheme();

        ColorBlock _colorBlock = new ColorBlock
        {
            normalColor = _theme.NormalButtonColor,
            highlightedColor = _theme.HighlightedButtonColor,
            pressedColor = _theme.PressedButtonColor,
            selectedColor = _theme.SelectedButtonColor,
            disabledColor = _theme.DisabledButtonColor,

            colorMultiplier = 1,
            fadeDuration = _theme.ColorTransitionSpeed
        };

        _button.colors = _colorBlock;

        _text.color = _theme.MainUiColor;

        if (!_button.interactable)
        {
            Color _alphaColor = _text.color;
            _alphaColor.a = _theme.DisabledButtonTextOpacity;
            _text.color = _alphaColor;
        }
    }
    #endregion
}
