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

    //�ړ����J�n����O�̍��W
    private Vector3 _beforePosition = Vector3.zero;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Finish") return;

        GetComponent<Rigidbody>().useGravity=false;
        GetComponent<Rigidbody>().isKinematic=true;

        tag = collision.transform.tag;

    }

    public void initialize()
    {


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
        _moveTime = MOVE_TIME;
    }

    public void CountDown() { _moveTime -= Time.deltaTime* GameConfig.GetGameSpeed(); }

    public bool IsMovable() { return _moveTime > 0; }

    public float GetMoveTime() { return _moveTime; }
    public float GetMoveTimeRata() { return 1f - (_moveTime / MOVE_TIME); }

    public Vector3 GetBeforePosition() { return _beforePosition; }



}
