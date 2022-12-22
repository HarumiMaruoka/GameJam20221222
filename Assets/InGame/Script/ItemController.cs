using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class ItemController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D = null;

    [SerializeField, Range(-0.1f, -20f)]
    private float _fallSpeed = -1f;

    private void Start()
    {
        var pos = transform.position;
        pos.z = 0;
        transform.position = pos;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        // ���܂ꂽ�Ƃ��̏����B
        // RigidBody��gravityScale�̒l��0�ɕύX����B
        _rigidbody2D.gravityScale = 0f;
        // �ŏ��̗����X�s�[�h��velocity��y�ɗ^����B
        _rigidbody2D.velocity = new Vector2(0f, _fallSpeed);
    }
    [SerializeField]
    private float _sideMoveSpeed = 3f;
    [InputName, SerializeField]
    private string _sideMoveInputName = default;
    [SerializeField]
    private float _rotateSpeed = 3f;
    [InputName, SerializeField]
    private string _rotateInputName = default;
    private void Update()
    {
        // ���͂ɉ����č��E�Ɉړ�����B
        var h = Input.GetAxisRaw(_sideMoveInputName);
        _rigidbody2D.velocity = new Vector2(h * _sideMoveSpeed, _rigidbody2D.velocity.y);
        // ���͂ɉ����ĉ�]����B
        var v = Input.GetAxisRaw(_rotateInputName);
        transform.Rotate(new Vector3(0f, 0f, v * _rotateSpeed));
    }
    [SceneName, SerializeField]
    private string _gameOverSceneName = default;
    [TagName, SerializeField]
    private string _gameZone = default;
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �Q�[���]�[������o����GameOver�V�[����ǂݍ��ށB
        if (collision.tag == _gameZone)
        {
            ScoreManager.SetScore(Camera.main.transform.position.y);
            // SceneManager.LoadScene(_gameOverSceneName);
            FindObjectOfType<SceneChange>().FadeInAndChangeScene();
        }
    }
    [SerializeField]
    private float _gravity = 1f;
    bool _isFirstCollision = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ƏՓ˂�����ȉ������s����B
        // RigidBody��gravityScale���w��̒l�ɕύX����B
        // ���̃A�C�e���𐶐�����B
        // �J�����̈ʒu���X�V����B
        // ���̃R���|�[�l���g���A�N�e�B�u�ɂ���B
        if (_isFirstCollision)
        {
            _isFirstCollision = false;
            _rigidbody2D.gravityScale = _gravity;
            FindObjectOfType<ItemGenerator>().Generate();
            FindObjectOfType<CameraController>().ChangeTarget(transform.position);
            this.enabled = false;
            GetComponent<AudioSource>().Play();
        }
    }
}
