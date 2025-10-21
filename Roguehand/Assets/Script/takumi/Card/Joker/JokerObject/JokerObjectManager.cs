using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class JokerObjectManager : MonoBehaviour
{

    /// <summary>
    /// ジョーカーの状態
    /// </summary>
    public enum JokerStatus
    {
        wait,
        play,
        action,
        end,
    }

    /// <summary>
    /// ジョーカーのオブジェクトの基底オブジェクト
    /// </summary>
    [SerializeField] private GameObject _prefab;

    [SerializeField, Header("ジョーカーのオブジェクトの一番左側")] private Transform LeftPos;
    [SerializeField, Header("ジョーカーのオブジェクトの一番右側")] private Transform RightPos;

    /// <summary>
    ///現在のジョーカーの状況
    /// </summary>
    private JokerStatus _status = JokerStatus.wait;

    /// <summary>
    /// ひとつ前の状態
    /// </summary>
    private JokerStatus _lostStatus = JokerStatus.wait;

    /// <summary>
    /// ジョーカーの倍率などを描画する座標
    /// </summary>
    private Vector2 _numPos = Vector2.zero;

    /// <summary>
    /// ジョーカーのオブジェクトリスト
    /// </summary>
    [SerializeField] private List<JokerObject> _jokerObjects = new List<JokerObject>();

    /// <summary>
    /// ダミージョーカーのオブジェクトリスト
    /// </summary>
    [SerializeField] private List<JokerObject> _domyyJokerObjects = new List<JokerObject>();


    /// <summary>
    /// 現在つかまれているかどうか
    /// </summary>
    private bool _isGrab = false;
    /// <summary>
    /// 現在つかまれているインデックス番号
    /// /// </summary>
    private int _isGrabID = -1;


    public void Awake()
    {
        JokerObjectUtility.instance = this;
    }



    public void Update()
    {
        Play();
        ObjectMovePos();
        Action();
        //ジョーカーの処理が終わったかどうか
        if (_status != JokerStatus.end) return;
        TrunEnd();
        _lostStatus = _status;
        _status = JokerStatus.wait;

    }

    /// <summary>
    /// カードのプレイに反応してジョーカーの処理をする関数
    /// </summary>
    private void Play()
    {

        if (_status != JokerStatus.play) return;
        for (int i = 0; i < _jokerObjects.Count; i++) _jokerObjects[i].Play();

    }

    /// <summary>
    /// カードのアクションに応じた動きの関数
    /// </summary>
    private void Action()
    {
        if (_status != JokerStatus.action) return;


        for (int i = 0; i < _jokerObjects.Count; i++) _jokerObjects[i].Action();

        if(_jokerObjects.Find(joker=> joker.CheckAction())==null)_status = JokerStatus.wait;

    }

    /// <summary>
    /// ジョーカーがプレイされていない時にジョーカーの位置を修正する関数
    /// </summary>
    private void ObjectMovePos()
    {
        if (_status != JokerStatus.wait) return;

        //ジョーカー同士の距離を作成
        float renge = Vector3.Distance(LeftPos.transform.position, RightPos.transform.position) / (_jokerObjects.Count + 1);

        for (int i = 0; i < _jokerObjects.Count; i++) _jokerObjects[i].MovePos(LeftPos.transform.position + new Vector3(renge * (i + 1), 0, 0));

        //手動の移動によって順番が入れ替わる関数
        CheckOrder();


    }
    /// <summary>
    /// ターンの終了時に呼ぶ関数
    /// </summary>
    private void TrunEnd()
    {
        for (int i = 0; i < _jokerObjects.Count; i++) _jokerObjects[i].TrunEnd();

    }

    /// <summary>
    /// ジョーカーの順番が正しくなおす関数
    /// </summary>
    private void CheckOrder()
    {

        if (!_isGrab) return;


        //ジョーカー同士の距離
        float renge = Vector3.Distance(LeftPos.transform.position, RightPos.transform.position) / (_jokerObjects.Count + 1);


        float Cardrenge = (LeftPos.transform.position.x + renge * (_isGrabID + 1)) - _jokerObjects[_isGrabID].transform.position.x;


        //横方向への移動距離が小さかったら順番の変更を加えない
        if (Mathf.Abs(Cardrenge) + 30 < renge) return;

        //移動方向を調整
        int count = 1;
        if (Cardrenge > 1) count = -1;

        if (_isGrabID + count >= _jokerObjects.Count || _isGrabID + count < 0) return;

        //ジョーカーの順番を入れ替える関数を呼ぶ
        JokerUtility.ChengeOrder(_isGrabID, _isGrabID + count);

        _isGrabID = _isGrabID + count;





    }


    /// <summary>
    /// 次のジョーカーをプレイ状態に変更する
    /// </summary>
    public void NestJokerPlay(JokerObject jokerObject)
    {
        //引数のジョーカーの配列番号を取得
        int count = GetJokerIndex(jokerObject);

        //配列番号を一つ先にする
        count++;

        //次のジョーカーが存在しないとき
        
        if (_jokerObjects.Count <= count) { _lostStatus = _status; _status = JokerStatus.end; return; }

        //ジョーカーをプレイ状態に変更する
        _jokerObjects[count].SetStatus(JokerStatus.play);


    }

    /// <summary>
    /// ジョーカーのプレイを開始する
    /// </summary>
    public void StartJokerPlay()
    {
        //ジョーカーが一つもない時
        if (_jokerObjects.Count <= 0) return;

        //最初のジョーカーをプレイ状態に変更する
        _jokerObjects[0].SetStatus(JokerStatus.play);
        _lostStatus = _status;
        _status = JokerStatus.play;

    }
    /// <summary>
    /// ジョーカーの追加時に呼ばれるオブジェクトの追加
    /// </summary>
    /// <param name="jokerBase"></param>
    public void AddJoker(JokerBase jokerBase)
    {
        //オブジェクトの生成
        _jokerObjects.Add(GameObject.Instantiate(_prefab, transform).AddComponent<JokerObject>());

        //オブジェクトの物理演算を停止
        _jokerObjects[_jokerObjects.Count - 1].GetComponent<Rigidbody>().isKinematic = true;

        //オブジェクトの初期化処理
        _jokerObjects[_jokerObjects.Count - 1].Initializ(jokerBase);

        _jokerObjects[_jokerObjects.Count - 1].name = "JokerID" + (_jokerObjects.Count - 1).ToString();
        _jokerObjects[_jokerObjects.Count - 1].transform.eulerAngles = Vector3.zero;

    }
    public void AddDommyJoker(JokerBase jokerBase)
    {
        //オブジェクトの生成
        _domyyJokerObjects.Add(GameObject.Instantiate(_prefab, transform).AddComponent<JokerObject>());

        //オブジェクトの物理演算を停止
        _domyyJokerObjects[_jokerObjects.Count - 1].GetComponent<Rigidbody>().isKinematic = true;

        //オブジェクトの初期化処理
        _domyyJokerObjects[_jokerObjects.Count - 1].Initializ(jokerBase);

        _domyyJokerObjects[_jokerObjects.Count - 1].name = "DomyyJokerID" + (_jokerObjects.Count - 1).ToString();

    }

    public void DommyDestroy() 
    {

        for(int i = 0; i < _domyyJokerObjects.Count; i++) 
        {

            //ザ・エンドってね
            _domyyJokerObjects[i].THEEnd();


            _domyyJokerObjects.RemoveAt(i);




        }

    }

    /// <summary>
    /// ID指定のジョーカーのオブジェクトの削除
    /// </summary>
    /// <param name="ID"></param>
    public void RemoveJoker(int ID)
    {

        //ザ・エンドってね
        _jokerObjects[ID].THEEnd();


        _jokerObjects.RemoveAt(ID);

        ExplanationManager.instance.Remove();



    }

    public GameObject GetIDObject(int ID) { return _jokerObjects[ID].gameObject; }
    public JokerObject GetIDJokerObject(int ID) { return _jokerObjects[ID]; }

    /// <summary>
    /// ジョーカーオブジェクトからIDを取得
    /// </summary>
    /// <param name="jokerObject"></param>
    /// <returns></returns>
    public int GetJokerIndex(JokerObject jokerObject)
    {
        return _jokerObjects.FindIndex(joker => joker == jokerObject);
    }

    public void GrabChange(int ID, bool flag)
    {
        _isGrab = flag;
        _isGrabID = ID;
        _jokerObjects[ID].SetGrab(flag);

    }

    /// <summary>
    /// 順番を入れ替える関数
    /// </summary>
    /// <param name="lostID"></param>
    /// <param name="nextID"></param>
    public void ChengeOrder(int lostID, int nextID)
    {
        _jokerObjects = Extra.ChengeOrder(_jokerObjects, lostID, nextID);
    }

    public void CardAddPlay(int ID, int AddNum)
    {
        _lostStatus = _status;
        _status = JokerStatus.action;

        //決められたコマンド
        if (AddNum == -2) return;

        _jokerObjects[ID].CardAddPlay(AddNum);
    }

    /// <summary>
    /// UIの座標を返す関数
    /// </summary>
    /// <returns></returns>
    public Vector2 GetNumPos() { return _numPos; }
    public void SetNumPos(Vector2 vector) { _numPos = vector; }
    /// <summary>
    /// 現在ジョーカーがアクションを行っているかどうかを判定
    /// </summary>
    /// <returns></returns>
    public int ActionCount()
    {
        int count = 0;
        for (int i = 0; i < _jokerObjects.Count; i++)
        {
            if (!_jokerObjects[i].GetAction()) continue;

            count++;
        }

        return count;
    }

    public void NextAction(JokerObject jokerObject) 
    {
        //引数のジョーカーの配列番号を取得
        int count = GetJokerIndex(jokerObject);

        //配列番号を一つ先にする
        count++;

        //次のジョーカーが存在しないとき
        if (_jokerObjects.Count <= count) { _status = _lostStatus; return; }


        for(int i = count; i < _jokerObjects.Count; i++) 
        {

            if (!_jokerObjects[i].GetAction()) continue;
            //ジョーカーをアクション状態に変更する
            _jokerObjects[i].SetStatus(JokerStatus.action);

            //ひとつだけ起動する
            return;

        }
        //ひとつもないとき
        _status = _lostStatus; return;
    }

}
