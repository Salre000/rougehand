using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Scripting;

public class JokerManager : MonoBehaviour
{
    /// <summary>
    /// ジョーカーをまとめたリスト
    /// </summary>
    private List<JokerBase> _jokers = new List<JokerBase>(5);

    /// <summary>
    /// 一時的にもっているジョーカー
    /// </summary>
    private List<JokerBase> _dommyJoker = new List<JokerBase>(5);



    /// <summary>
    /// ジョーカーのターゲットになり得る物をキャッシュする
    /// 順番に処理をする為にリストにした
    /// </summary>
    private List<JokerActionUseEnum.JokerActionTarget> _target = new List<JokerActionUseEnum.JokerActionTarget>();


    /// <summary>
    /// ジョーカーのターゲットになり得るスート
    /// </summary>
    private Card.suit _targetSuit;


    /// <summary>
    /// ジョーカーのターゲットになり得るナンバー
    /// </summary>
    private Card.number _targetNumer;

    /// <summary>
    /// ジョーカーのターゲットになり得る役
    /// </summary>
    private RoleManager.Role _targetRole;

    /// <summary>
    /// 現在のループ中のインデックス番号
    /// </summary>
    private int useIndex = -1;

    /// <summary>
    /// ジョーカーの最大数
    /// </summary>
    private readonly int JOKER_MAX_COUNT = 5;


    public void Awake()
    {
        JokerUtility.instance = this;
        SetTarget(JokerActionUseEnum.JokerActionTarget.None);
    }
    public void Start()
    {
        RoundObserver.Instance.AddRoundEndAction(RoundEnd);
        RoundObserver.Instance.AddRoundStartAction(RoundStart);
    }

    private void Update()
    {
        JokerUpData();
    }




    /// <summary>
    /// ジョーカーを破棄する関数
    /// </summary>
    /// <param name="joker"></param>
    /// <returns></returns>
    public bool Remove(JokerBase joker)
    {

        bool flag = _jokers.Contains(joker);

        int index = _jokers.FindIndex(jokerBase => joker == jokerBase);

        _jokers.Remove(joker);


        //ジョーカーのオブジェクトの削除処理
        JokerObjectUtility.RemoveJoker(index);

        return flag;
    }
    public bool Remove(int ID)
    {

        int index = ID;

        _jokers.RemoveAt(index);


        //ジョーカーのオブジェクトの削除処理
        JokerObjectUtility.RemoveJoker(index);

        return true;
    }



    /// <summary>
    /// ラウンドの開始時のジョーカーの処理
    /// </summary>
    public void RoundStart()
    {
        for (int i = 0; i < _jokers.Count; i++) _jokers[i].RoundStart();
    }
    /// <summary>
    /// ラウンドの終了時のジョーカーの処理
    /// </summary>
    public void RoundEnd()
    {

        for (int i = 0; i < _jokers.Count; i++) _jokers[i].RoundEnd();
    }


    /// <summary>
    /// ジョーカーを追加する関数
    /// </summary>
    /// <param name="ID"></param>
    public void AddJoker(int ID)
    {


        int jokerCount = 0;
        JokerUtility.JokerALLAction(joker => { if (joker.GetJokerBuff() != Card.JokerBuff.Negative) jokerCount++; });

        if (jokerCount == JOKER_MAX_COUNT) return;

        JokerBase joker = ALLJoker.GetJoker((ALLJoker._allJokerEnum)ID);
        joker.Initializ();
        _jokers.Add(joker);
        JokerObjectUtility.AddJoker(joker);

    }

    public void JokerChenge(int ID)
    {
        JokerObjectUtility.GetIDJokerObject(ID).StartChenge();

    }

    public void SetMaterial(int ID)
    {
        JokerBase jokerBase = _jokers[ID];

        MeshRenderer meshRenderer = JokerObjectUtility.GetIDObject(ID).transform.GetChild(0).GetComponent<MeshRenderer>();

        //一度キャッシュする必要あり
        Material[] materials = meshRenderer.materials;

        materials[0] = BuffUtility.GetJokerMaterial((int)jokerBase.GetJokerBuff());

        if (jokerBase.GetCardBuff() != Card.cardBuff.None) materials[0] = BuffUtility.GetCardMaterial((int)jokerBase.GetCardBuff());


        meshRenderer.materials = materials;





    }


    /// <summary>
    /// ジョーカーを選択できる状態にする関数
    /// ランダム
    /// </summary>
    public void ShopJokerAdd(System.Func<JokerBase> func = null)
    {
        //　ジョーカー選択条件を何も入れなかったらランダムで生成する
        if (func == null) func = GetRoundomJoker;

        //カードとカードの間
        JokerBase jokerBase = func();

        JokerObjectUtility.AddDomyyJoker(jokerBase);



    }



    /// <summary>
    /// ID指定の売られたときの挙動
    /// </summary>
    /// <param name="ID"></param>
    public void SaleAction(int ID)
    {
        _jokers[ID].SaleAction();
    }

    /// <summary>
    /// 今のフレームないで行われたターゲットの動き
    /// </summary>
    /// <returns></returns>
    public JokerActionUseEnum.JokerActionTarget GetTarget() { return _target[0]; }
    /// <summary>
    /// 今のフレームないで行われたターゲットの動き
    /// </summary>
    /// <returns></returns>
    public Card.suit GetTargetSuit() { return _targetSuit; }
    /// <summary>
    /// 今のフレームないで行われたターゲットの動き
    /// </summary>
    /// <returns></returns>
    public Card.number GetTargetNumer() { return _targetNumer; }
    /// <summary>
    /// 今のラウンドないで行われたターゲットの動き
    /// </summary>
    /// <returns></returns>
    public RoleManager.Role GetTargetRole() { return _targetRole; }



    /// <summary>
    /// 順番を入れ替える関数
    /// </summary>
    /// <param name="lostID"></param>
    /// <param name="nextID"></param>
    public void ChengeOrder(int lostID, int nextID)
    {
        _jokers = Extra.ChengeOrder(_jokers, lostID, nextID);

        JokerObjectUtility.ChengeOrder(lostID, nextID);

    }

    /// <summary>
    /// ジョーカーによって倍率が上昇する関数
    /// </summary>
    /// <param name="magnification"></param>
    public void JokerAddMagnification(float magnification)
    {
        ScoreManager.instance.MagnificationPlus(magnification);

    }
    /// <summary>
    /// ジョーカーによって基礎値が上昇する関数
    /// </summary>
    /// <param name="baseValue"></param>
    public void JokerAddBaseValue(float baseValue)
    {

        ScoreManager.instance.BasicPlus(baseValue);
    }

    /// <summary>
    /// 条件が満たされた瞬間にターゲットの中に代入する関数
    /// </summary>
    public void SetTarget(JokerActionUseEnum.JokerActionTarget target)
    {
        _target.Add(target);
    }
    /// <summary>
    /// 条件が満たされた瞬間にターゲットの中に代入する関数
    /// </summary>
    public void SetTarget(Card.suit target)
    {
        _targetSuit = target;
    }
    /// <summary>
    /// 条件が満たされた瞬間にターゲットの中に代入する関数
    /// </summary>
    public void SetTarget(Card.number target)
    {
        _targetNumer = target;
    }
    /// <summary>
    /// 条件が満たされた瞬間にターゲットの中に代入する関数
    /// </summary>
    public void SetTarget(RoleManager.Role target)
    {
        _targetRole = target;
    }



    public int GetIndex() { return useIndex; }
    public int GetIndex(JokerBase jokerBase) { return _jokers.IndexOf(jokerBase); }

    public void GrabChange(int id, bool flag)
    {
        JokerObjectUtility.GrabChange(id, flag);

    }

    public void SetSale(int ID)
    {
        GameObject joker = JokerObjectUtility.GetIDObject(ID);

        SaleUtility.SetSale(_jokers[ID], joker, _jokers[ID].GetSaleValue());


    }
    public void ShowExplanation(int ID)
    {
        ExplanationManager.instance.AddExplanation(JokerObjectUtility.GetIDObject(ID), _jokers[ID], _jokers[ID].JokerBuffs(), new Vector2(0, 1));

    }
    public void ShowExplanation(GameObject gameObject,JokerBase jokerBase,Vector2 offset)
    {
        ExplanationManager.instance.AddExplanation(gameObject, jokerBase, jokerBase.JokerBuffs(), offset);

    }
    /// <summary>
    /// 全てのジョーカーに何かする関数
    /// </summary>
    public void JokerALLAction(System.Action<JokerBase> action)
    {
        for (int i = 0; i < _jokers.Count; i++)
            action(_jokers[i]);
    }

    public List<JokerBase> GetJoker() { return _jokers; }


    /// <summary>
    /// ジョーカーのアップデート処理を回す関数
    /// </summary>
    private void JokerUpData()
    {

        for (int i = 0; i < _jokers.Count; i++)
        {
            useIndex = i;
            _jokers[i].UpData();
        }

        //ターゲットの初期化
        if (_target.Count <= 1) _target[0] = JokerActionUseEnum.JokerActionTarget.None; else _target.RemoveAt(0);
        _targetSuit = Card.suit.None;
        _targetNumer = Card.number.None;
        _targetRole = RoleManager.Role.None;


        useIndex = -1;
    }
    /// <summary>
    /// ラウンド終了時のジョーカーの挙動
    /// </summary>
    private void SetRoundEndAction()
    {
        for (int i = 0; i < _jokers.Count; i++)
            _jokers[i].RoundEnd();
    }

    private JokerBase GetRoundomJoker() { return ALLJoker.GetJoker((ALLJoker._allJokerEnum)Random.Range(0, (int)ALLJoker._allJokerEnum.MAX)); }



}
