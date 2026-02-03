using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{

    /// <summary>
    /// 一度の移動に掛かる時間の定数
    /// </summary>
    private const float MOVE_TIME = 0.2f;

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
        action
    }

    /// <summary>
    /// 現在の状態
    /// </summary>
    [SerializeField] private status _status = status.none;

    /// <summary>
    /// ひとつ前の状態
    /// </summary>
    [SerializeField] private status _lostStatus = status.none;

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

    /// <summary>
    /// 現在つかまれているかどうか
    /// </summary>
    [SerializeField] private bool _isGrab = false;

    /// <summary>
    /// 現在つかむことが可能かどうか
    /// </summary>
    [SerializeField] private bool _grab = true;

    [SerializeField] private List<System.Action> actions = new List<System.Action>();


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Finish") return;

        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;

        tag = collision.transform.tag;

    }
    public void Awake()
    {
        initialize();
    }

    public void initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();

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

    /// <summary>
    /// カードのリセットに使う関数
    /// </summary>
    public void ResetCard()
    {
        SetStatus(CardObject.status.deck);
        ResetMoveTime();
        GravityStart();
        _isGrab = false;

    }

    public void GetCheckBuff(Card.Trump trump,System.Action<int> action,int id)
    {
        if (BuffUtility.CheckPlayBuffDeck(trump.deckBuff))
            actions.Add(()=>
            {
                TrumpBuff.target = gameObject;
                BuffUtility.GetActionPlayBuffDeck(trump.deckBuff)();
            });

        if (BuffUtility.CheckPlayBuffCard(trump.cardBuff))
            actions.Add(()=>
            {
                TrumpBuff.target = gameObject;

                BuffUtility.GetActionPlayBuffCard(trump.cardBuff)();
            });

        actions.Add(()=>action(id));
    }

    public int GetActionsCount() {  return actions.Count; }

    public void PlayAction() 
    {
        actions[0]();

        actions.RemoveAt(0);



    }

    public void SetStatus(status status) { _lostStatus = _status; _status = status; }

    public status GetStatus() { return _status; }

    public status GetLostStatus() { return _lostStatus; }

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

    /// <summary>
    /// 時間経過の関数
    /// </summary>
    public void CountDown()
    {
        //つかまれている間カウントしない
        if (_isGrab) return;
        _moveTime -= Time.deltaTime * GameConfig.GetGameSpeed();
        if (IsMovable()) return;
        _grab = true;
    }

    /// <summary>
    /// 移動可能かどうかの判定
    /// </summary>
    /// <returns></returns>
    public bool IsMovable() { return _moveTime > 0; }

    public float GetMoveTime() { return _moveTime; }
    public float GetMoveTimeRata() { return 1f - (_moveTime / MOVE_TIME); }

    public void StopMove() { _moveTime = 0f; }

    public Vector3 GetBeforePosition() { return _beforePosition; }
    public Vector3 GetBeforeAngle() { return _beforeAngle; }


    public void SetGrab(bool flag) { _isGrab = flag; }

    /// <summary>
    /// つかむことが可能かどうかを返す関数
    /// </summary>
    /// <returns></returns>
    public bool GetGrabFlag() { return _grab; }

    /// <summary>
    /// つかむことを出来なく変更
    /// カードが目的地に着いたら解除
    /// </summary>
    public void NotGrab() { _grab = false; }

    public bool IsGrab() { return _isGrab; }

    public System.Action AddScore(Card.number number)
    {
        return () =>
        {
            float score = (int)number;
            if (score <= 1 || 11 < score) score = 11;
            ScoreManager.instance.SetScoreViewTrans(gameObject.transform.position);
            ScoreManager.instance.SetScoreViewText("+" + score);


        };


    }


}
