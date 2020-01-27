using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum LifeDecreaseMode
{
    LeftToRight,
    RightToLeft,
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Lives")]
    private List<GameObject> _lives = new List<GameObject>();
    public Transform LifePanel;
    public GameObject LifePrefab;

    [Space(10)]
    public LifeDecreaseMode LifeDecreaseMode;

    [Space(10)]
    public Vector2 LifePanelAnchorMin;
    public Vector2 LifePanelAnchorMax = new Vector2(1, 1);

    [Header("UI")]
    public TextMeshProUGUI ScoreText;
    public GameObject PausePanel;

    #region Init
    private void Awake()
    {
        if (instance == null || instance != this)
            instance = this;

        if(PausePanel.activeSelf)
            PausePanel.SetActive(false);

        SetLifePanelOffset();
    }

    private void SetLifePanelOffset()
    {
        RectTransform _lifePanelOffset = LifePanel.gameObject.GetComponent<RectTransform>();

        SetAnchorPoints(_lifePanelOffset, LifePanelAnchorMin, LifePanelAnchorMax);

        ClearOffset(_lifePanelOffset);
    }

    public void CreatingLives(int lives)
    {
        Vector3 _startingPosition = LifePanel.transform.position;

        for(int i = 0; i < lives; i++)
        {
            GameObject _life = InstantiateUIObject(LifePrefab, _startingPosition, Quaternion.identity, LifePanel);

            RectTransform _transform = GetTransformFromUIObject(_life);

            _transform.localRotation = Quaternion.identity;

            Vector2 _achorMin = new Vector2(1f / lives * i, 0.2f);
            Vector2 _anchorMax = new Vector2(1f / lives * (i+1), 0.8f);

            SetAnchorPoints(_transform, _achorMin, _anchorMax);

            _lives.Add(_life);
        }
    }
    #endregion

    public void UpdateScoreText(int score)
    {
        UpdateText(ScoreText, score.ToString());
    }

    public void SetPause(bool pause)
    {
        PausePanel.SetActive(pause);
    }

    public void DecreaseLife(int damage)
    {
        if(_lives.Count - damage > 0)
        {
            switch (LifeDecreaseMode)
            {
                case LifeDecreaseMode.LeftToRight:
                    for (int i = 0; i < _lives.Count; i++)
                    {
                        GameObject _currentLife = _lives[i];

                        if (_currentLife.activeSelf)
                        {
                            _currentLife.SetActive(false);
                            break;
                        }
                    }
                    break;
                case LifeDecreaseMode.RightToLeft:
                    for (int i = _lives.Count; i > 0; i--)
                    {
                        GameObject _currentLife = _lives[i - 1];

                        if (_currentLife.activeSelf)
                        {
                            _currentLife.SetActive(false);
                            break;
                        }
                    }
                    break;
            }
        }
    }

    #region Utils
    private GameObject InstantiateUIObject(GameObject gameObject, Vector3 position, Quaternion rotation, Transform parent)
    {
        return Instantiate(gameObject, position, rotation, parent);
    }

    private RectTransform GetTransformFromUIObject(GameObject gameObject)
    {
        return gameObject.GetComponent<RectTransform>();
    }

    private void SetAnchorPoints(RectTransform transform, Vector2 min, Vector2 max)
    {
        transform.anchorMin = min;
        transform.anchorMax = max;

        ClearOffset(transform);
    }

    private void ClearOffset(RectTransform transform)
    {
        transform.offsetMin = Vector2.zero;
        transform.offsetMax = Vector2.zero;
    }

    private void UpdateText(TextMeshProUGUI textMeshPro, string text)
    {
        textMeshPro.text = text;
    }
    #endregion
}
