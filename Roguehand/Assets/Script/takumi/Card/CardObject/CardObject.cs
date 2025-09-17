using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{

    /// <summary>
    /// ��x�̈ړ��Ɋ|���鎞�Ԃ̒萔
    /// </summary>
    private const float MOVE_TIME = 1f;

    /// <summary>
    /// ���̃J�[�h�̏��
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
    /// �ړ����J�n����O�̍��W
    /// </summary>
    private Vector3 _beforePosition = Vector3.zero;
    /// <summary>
    /// �ړ����J�n����O�̊p�x
    /// </summary>
    private Vector3 _beforeAngle = Vector3.zero;

    /// <summary>
    /// ���̃I�u�W�F�N�g�̃��M�b�h�{�f�B
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
    /// �d�͂𑀍�\��ԂɕύX
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
    /// �ړ��\���Ԃ����Z�b�g
    /// �ړ����\�ɕύX
    /// </summary>
    public void ResetMoveTime()
    {
        _beforePosition = transform.position;
        _beforeAngle = transform.eulerAngles;
        _moveTime = MOVE_TIME;
    }

    public void CountDown() { _moveTime -= Time.deltaTime* GameConfig.GetGameSpeed(); }

    /// <summary>
    /// �ړ��\���ǂ����̔���
    /// </summary>
    /// <returns></returns>
    public bool IsMovable() { return _moveTime > 0; }

    public float GetMoveTime() { return _moveTime; }
    public float GetMoveTimeRata() { return 1f - (_moveTime / MOVE_TIME); }

    public Vector3 GetBeforePosition() { return _beforePosition; }
    public Vector3 GetBeforeAngle() { return _beforeAngle; }



}
