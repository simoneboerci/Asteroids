using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    public int DefaultThemeIndex;
    private Theme _currentTheme;
    public List<Theme> Themes;

    #region Init
    private void Awake()
    {
        if (instance == null || instance != this)
            instance = this;

        _currentTheme = Themes[DefaultThemeIndex];
    }
    #endregion

    public void SetCurrentTheme(int index)
    {
        _currentTheme = Themes[index];
    }
    public Theme GetCurrentTheme()
    {
        return _currentTheme;
    }
}
