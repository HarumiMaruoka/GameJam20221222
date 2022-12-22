using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartAnimaiton : MonoBehaviour
{
    [SerializeField]
    private ItemGenerator _itemGenerator = default;
    [SerializeField]
    private float _animTime = 1f;
    [SerializeField]
    private Ease _animEase = default;
    [SerializeField]
    private int _startDelayTime = 1000;
    [SerializeField]
    private int _endDelayTime = 1000;
    private async void Awake()
    {
        _itemGenerator.enabled = false;
        await Task.Delay(_startDelayTime);
        transform.
            DORotate(new Vector3(0f, 0f, -360f), _animTime, RotateMode.FastBeyond360).
            SetEase(_animEase).
            OnComplete(async () =>
            {
                _itemGenerator.enabled = true;
                await Task.Delay(_endDelayTime);
                this.gameObject.SetActive(false);
            });
    }
}
