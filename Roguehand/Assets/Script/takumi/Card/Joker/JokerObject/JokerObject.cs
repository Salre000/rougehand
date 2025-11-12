using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
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
    /// ジョーカープレイ時の中身
    /// </summary>
    private System.Action _jokerActionProcess;

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
    /// ゲーム中に上昇する倍率の値
    /// </summary>
    private float AddNum = 0;

    /// <summary>
    /// このオブジェクトを破壊するかどうかのフラグ
    /// </summary>
    private bool _isEnd = false;

    /// <summary>
    /// 一ターンの行動の変数
    /// </summary>
    private List<System.Action> actions = new List<System.Action>();


    /// <summary>
    /// ジョーカーの生成時に動く初期化処理
    /// </summary>
    /// <param name="jokerBase"></param>
    public void Initializ(JokerBase jokerBase)
    {
        _base = jokerBase;
        _status = JokerStatus.wait;
        _jokerActionProcess = NormalJokerActionProcess;
        SetAction();

        transform.GetChild(0).AddComponent<JokerObjectAnime>();

    }

    /// <summary>
    /// ジョーカーのプレイ前にジョーカーのプレイ時の内容を記録
    /// </summary>
    public void PreparationPlay()
    {
        if (_base.Trun() >0) actions.Add(() => JokerUtility.AddMagnification(_base.Trun()));
        if (_base.GetCardBuff().BuffAction()) actions.Add(() => BuffUtility.PlayBuff(_base.GetCardBuff()));
        if (_base.GetJokerBuff().BuffAction()) actions.Add(() => BuffUtility.PlayBuff(_base.GetJokerBuff()));


    }

    /// <summary>
    /// カードのプレイに反応するジョーカーのプレイ
    /// </summary>
    public void Play()
    {
        //このジョーカーがプレイ状態じゃない時
        if (_status != JokerStatus.play) return;

        //ジョーカーが何もできない時
        if (actions.Count < 1) { _status = JokerStatus.wait; JokerObjectUtility.NestJokerPlay(this); return; }



        _jokerPlayAction();
    }

    public void Action()
    {
        if (_status != JokerStatus.action) return;

        //　アクションを開始時に角度を記憶する
        LostAngle = transform.eulerAngles;


        _jokerPlayAction();



    }


    /// <summary>
    /// このジョーカーのオブジェクトをID依存の位置に移動させる
    /// </summary>
    public void MovePos(Vector3 nextpos)
    {
        //掴む移動中は特殊な移動に変更
        if (_isGrab) { GrabMove(); return; }

        if (_status != JokerStatus.wait) return;

        if (Vector3.Distance(transform.position, nextpos) < EPSILON) { _time = 0; _lostpos = nextpos; _lostAngle = Vector3.zero; return; }

        _time += Time.deltaTime * GameConfig.GetGameSpeed() * 2.5f;


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
        _isGrab = flag;

        _lostpos = transform.position;
    }

    /// <summary>
    /// ジョーカーが自分のターン以外に起こす挙動の開始時
    /// </summary>
    public void CardAddPlay(float AddNum)
    {
        this.AddNum = AddNum;
        _jokerActionProcess = NeverAddJokerActionProcess;

        if (JokerObjectUtility.GetActionCount() >= 2) return;
        _status = JokerStatus.action;
    }

    /// <summary>
    /// アクションの待機が存在するのかどうか
    /// </summary>
    /// <returns></returns>
    public bool GetAction() { return AddNum != 0; }

    public bool CheckAction() { return _status == JokerStatus.action; }

    public void THEEnd() { _isEnd = true; }
    public bool IsEnd() { return _isEnd; }

    public void StartChenge()
    {
        _status = JokerStatus.action;


        _jokerPlayAction = ChengeAction;
        reta = 1;
        _time = 0;
    }

    public int GetJokerID() { return _base.GetID(); }

    private readonly float CHENGE_SPEED=4;

    private Vector3 LostAngle = Vector3.zero;
    private void ChengeAction()
    {
        _time += Time.deltaTime * GameConfig.GetGameSpeed() * reta* CHENGE_SPEED;

        transform.eulerAngles = Vector3.Lerp(Vector3.zero, LostAngle+new Vector3(0, 180, 0), _time);

        if (_time > 1 && reta == 1)
        {

            //マテリアルを変更
            JokerUtility.SetMaterial(JokerObjectUtility.GetJokerIndex(this));

            reta = -1;

            LostAngle = transform.eulerAngles;
        }

        if (_time > 0f) return;




        _status = JokerStatus.wait;
        reta = 1;
        SetAction();
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

        if (!JokerAction()) return;

        _jokerActionProcess();


    }

    /// <summary>
    /// ジョーカーの動き
    /// </summary>
    private bool JokerAction()
    {

        _time += Time.deltaTime * GameConfig.GetGameSpeed() * 10;

        transform.eulerAngles = Vector3.Lerp(_lostAngle, new Vector3(0, 0, 45 * reta), _time);

        if (_time < 1) return false;

        _time = 0;
        if (reta == 0) reta = -1;
        if (reta == 1) reta = 0;
        _lostAngle = transform.eulerAngles;

        //一ターンに一度に制限
        if (!_isPlay || reta != -1) return false;



        return true;

    }


    private void NormalJokerActionProcess()
    {


        reta = 1;
        //プレイの瞬間のアクション
        //倍率に追加
        actions[0]();

        actions.RemoveAt(0);

        if (actions.Count > 0) return;

        JokerObjectUtility.NestJokerPlay(this);
        _status = JokerStatus.wait;
        _isPlay = false;



    }
    private void NeverAddJokerActionProcess()
    {
        //カードの倍率の上昇したっていうアニメーションを入れる

        reta = 1;
        AddNum = 0;

        _status = JokerStatus.wait;

        _jokerActionProcess = NormalJokerActionProcess;

        JokerObjectUtility.NextAction(this);



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
