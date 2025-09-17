using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{

    /// <summary>
    /// 一度の移動に掛かる時間の定数
    /// </summary>
    private const float MOVE_TIME = 1f;

    /// <summary>
    /// このカードの状態
    /// </summary>
    public enum status
    {
        none = -1,
        deck,
        change,
        hand,
        play,
        playWait,
        discard,
        trash
    }

   [SerializeField]private status _status = status.none;

    private float _moveTime = 0;

    /// <summary>
    /// 移動を開始する前の座標
    /// </summary>
    private Vector3 _beforePosition = Vector3.zero;
    /// <summary>
    /// 移動を開始する前の角度
    /// </summary>
    private Vector3 _beforeAngle = Vector3.zero;

    /// <summary>
    /// このオブジェクトのリギッドボディ
    /// </summary>
    private Rigidbody _rigidbody;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Finish") return;

        _rigidbody.useGravity=false;
        _rigidbody.isKinematic=true;

        tag = collision.transform.tag;

    }
    public void Awake()
    {
        initialize();
    }

    public void initialize()
    {
        _rigidbody=GetComponent<Rigidbody>();

    }

    /// <summary>
    /// 重力を操作可能状態に変更
    /// </summary>
    public void GravityStart() 
    {
        tag = "Untagged";
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;

    }

    public void SetStatus(status status) { _status = status; }

    public status GetStatus() { return _status; }

    /// <summary>
    /// 移動可能時間をリセット
    /// 移動を可能に変更
    /// </summary>
    public void ResetMoveTime()
    {
        _beforePosition = transform.position;
        _beforeAngle = transform.eulerAngles;
        _moveTime = MOVE_TIME;
    }

    public void CountDown() { _moveTime -= Time.deltaTime* GameConfig.GetGameSpeed(); }

    /// <summary>
    /// 移動可能かどうかの判定
    /// </summary>
    /// <returns></returns>
    public bool IsMovable() { return _moveTime > 0; }

    public float GetMoveTime() { return _moveTime; }
    public float GetMoveTimeRata() { return 1f - (_moveTime / MOVE_TIME); }

    public Vector3 GetBeforePosition() { return _beforePosition; }
    public Vector3 GetBeforeAngle() { return _beforeAngle; }



}
