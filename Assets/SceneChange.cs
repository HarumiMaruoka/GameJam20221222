using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SceneName, SerializeField]
    private string _nextScene = default;
    [SerializeField]
    private Image _fadeInImage = default;
    [SerializeField]
    private float _fadeInTime = default;
    [SerializeField]
    private Image _fadeOutImage = default;
    [SerializeField]
    private float _fadeOutTime = default;
    // ボタン押下時に呼び出す
    public void FadeInAndChangeScene()
    {
        _fadeInImage.gameObject.SetActive(true);
        _fadeInImage?.DOFade(1f, _fadeInTime).
            OnComplete(() => SceneManager.LoadScene(_nextScene));
    }
    // シーン開始時に実行する
    private void Awake()
    {
        var tmp = _fadeInImage.color;
        tmp.a = 0f;
        _fadeInImage.color = tmp;
        _fadeInImage.gameObject.SetActive(false);
        FadeOut();
    }
    [SerializeField]
    private UnityEvent _fadeOutEvent;
    private void FadeOut()
    {
        _fadeOutImage.gameObject.SetActive(true);
        _fadeOutImage?.DOFade(0f, _fadeOutTime).
            OnComplete(() => 
            {
                _fadeOutImage.gameObject.SetActive(false);
                _fadeOutEvent?.Invoke();
            }
            );
    }
}
