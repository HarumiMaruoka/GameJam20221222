using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // �J�����̓A�C�e���̒��_�ʒu���^�[�Q�b�g����B
    // NowPos
    [SerializeField]
    private float _animTime = 1f;
    [SerializeField]
    private Ease _animEase = default;
    public void ChangeTarget(Vector2 Pos)
    {
        if (transform.position.y < Pos.y)
        {
            transform.
                DOMoveY(Pos.y, _animTime).
                SetEase(_animEase);
        }
    }
}
