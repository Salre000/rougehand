using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static JokerObjectManager;
public class JokerObject : MonoBehaviour
{


    /// <summary>
    /// ジョーカーの移動に使う移動量
    /// </summary>
    private readonly Vector3 _MOVE_VEC = new Vector3(0, 0, 100);

    /// <summary>
    /// ジョーカーの移動前の座標
    /// </summary>
    private Vector3 _lostpos = new Vector3(0, 0, 0);

    /// <summary>
    /// 前の角度
    /// </summary>
    private Vector3 _lostAngle = new Vector3(0, 0, 0);

    /// <summary>
    /// このジョーカーのステータス
    /// </summary>
    [SerializeField] private JokerStatus _status = JokerStatus.wait;

    /// <summary>
    /// ジョーカーの内部処理
    /// </summary>
    private JokerBase _base;

    /// <summary>
    /// ジョーカープレイ時の動き方
    /// </summary>
    private System.Action _jokerPlayAction;

    /// <summary>
    /// ジョーカーの経過時間
    /// </summary>
    private float _time;

    /// <summary>
    /// ジョーカーのオブジェクトが掴む移動中かどうか
    /// </summary>
    private bool _isGrab = false;

    /// <summary>
    /// 一ターンに一度だけにする為の変数
    /// </summary>
    private bool _isPlay = true;

    /// <summary>
    /// ジョーカーのオブジェクと移動で使用する誤差
    /// </summary>
    private readonly float EPSILON = 0.1f;

    /// <summary>
    /// ジョーカーの生成時に動く初期化処理
    /// </summary>
    /// <param name="jokerBase"></param>
    public void Initializ(JokerBase jokerBase)
    {
        _base = jokerBase;
        _status = JokerStatus.wait;

        SetAction();
    }

    /// <summary>
    /// カードのプレイに反応するジョーカーのプレイ
    /// </summary>
    public void Play()
    {
        //このジョーカーがプレイ状態じゃない時
        if (_status != JokerStatus.play) return;

        //ジョーカーが何もできない時
        if (_base.Trun() < 1) { _status = JokerStatus.wait; JokerObjectUtility.NestJokerPlay(this); return; }


        _jokerPlayAction();
    }

    /// <summary>
    /// このジョーカーのオブジェクトをID依存の位置に移動させる
    /// </summary>
    public void MovePos(Vector3 nextpos)
    {
        //掴む移動中は特殊な移動に変更
        if (_isGrab) { GrabMove(); return; }

        if (Vector3.Distance(transform.position, nextpos) < EPSILON) { _time = 0; _lostpos = nextpos; _lostAngle = Vector3.zero; return; }

        _time += Time.deltaTime * GameConfig.GetGameSpeed();


        transform.position = Vector3.Lerp(_lostpos, nextpos, _time);

    }

    /// <summary>
    /// ターンの終了時に呼ばれる
    /// </summary>
    public void TrunEnd()
    {

        _isPlay = true;
        _base.TrunReset();
    }

    /// <summary>
    /// ラウンドの終了時に呼ばれる
    /// </summary>
    public void RoundEnd()
    {
        _base.RoundEnd();
    }





    /// <summary>
    /// 自身のステータスの上書き
    /// </summary>
    /// <param name="status"></param>
    public void SetStatus(JokerStatus status) { _status = status; }

    /// <summary>
    /// 掴み状態の切り替え関数
    /// </summary>
    /// <param name="flag"></param>
    public void SetGrab(bool flag)
    {
        _isGrab=flag;

        _lostpos=transform.position;
    }

    private void SetAction()
    {

        switch (_base.GetJokerObjectType())
        {
            case 0: _jokerPlayAction = JokerCardAction; break;

            default:
                break;
        }
    }

    /// <summary>
    /// 一つ下の関数内でしか使わない変数
    /// </summary>
    private int reta = 1;
    /// <summary>
    /// カード状のジョーカーのプレイ時の挙動
    /// </summary>
    private void JokerCardAction()
    {
        _time += Time.deltaTime * GameConfig.GetGameSpeed() * 10;

        transform.eulerAngles = Vector3.Lerp(_lostAngle, new Vector3(0, 0, 45 * reta), _time);

        if (_time < 1) return;

        _time = 0;
        if (reta == 0) reta = -1;
        if (reta == 1) reta = 0;
        _lostAngle = transform.eulerAngles;

        //一ターンに一度に制限
        if (!_isPlay || reta != -1) return;
        reta = 1;
        _isPlay = false;

        _status = JokerStatus.wait;
        JokerObjectUtility.NestJokerPlay(this);

        //プレイの瞬間のアクション

        //倍率に追加
        JokerUtility.AddMagnification(_base.Trun());
        Debug.Log("倍率に追加");


    }

    /// <summary>
    /// 掴んでいるジョーカーの移動関数
    /// </summary>
    private void GrabMove() 
    {
        //マウスポイント依存で座標を決定する
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    





    
    }


}
