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
        // 生まれたときの処理。
        // RigidBodyのgravityScaleの値を0に変更する。
        _rigidbody2D.gravityScale = 0f;
        // 最初の落下スピードをvelocityのyに与える。
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
        // 入力に応じて左右に移動する。
        var h = Input.GetAxisRaw(_sideMoveInputName);
        _rigidbody2D.velocity = new Vector2(h * _sideMoveSpeed, _rigidbody2D.velocity.y);
        // 入力に応じて回転する。
        var v = Input.GetAxisRaw(_rotateInputName);
        transform.Rotate(new Vector3(0f, 0f, v * _rotateSpeed));
    }
    [SceneName, SerializeField]
    private string _gameOverSceneName = default;
    [TagName, SerializeField]
    private string _gameZone = default;
    private void OnTriggerExit2D(Collider2D collision)
    {
        // ゲームゾーンから出たらGameOverシーンを読み込む。
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
        // 何かと衝突したら以下を実行する。
        // RigidBodyのgravityScaleを指定の値に変更する。
        // 次のアイテムを生成する。
        // カメラの位置を更新する。
        // このコンポーネントを非アクティブにする。
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
