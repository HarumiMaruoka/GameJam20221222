using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScoreAnimation : MonoBehaviour
{
    [SerializeField]
    private float _animTime = 3f;
    [SerializeField]
    private Ease _animEase = default;
    private float _scoreValue = 0f;
    private Text _scoreText = null;
    private void Start()
    {
        DOTween.To(() => _scoreValue, (x) => _scoreValue = x, ScoreManager.Score, _animTime)
            .SetEase(_animEase);
        _scoreText = GetComponent<Text>();
    }
    private void Update()
    {
        _scoreText.text = _scoreValue.ToString("F");
    }
}
