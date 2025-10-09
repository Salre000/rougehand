using System.Collections;
using System.Collections.Generic;
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
    /// </summary>
    private JokerActionUseEnum.JokerActionTarget _target;

    /// <summary>
    /// 現在のループ中のインデックス番号
    /// </summary>
    private int useIndex=-1;

    public void Awake()
    {
        JokerUtility.instance = this;

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
        JokerBase joker = ALLJoker.GetJoker((ALLJoker._allJokerEnum)ID);
        _jokers.Add(joker);
        JokerObjectUtility.AddJoker(joker);

    }

    /// <summary>
    /// ジョーカーを選択できる状態にする関数
    /// ランダム
    /// </summary>
    /// <param name="count"><ジョーカーの数/param>
    public void ShopJokerAdd(int count=3) 
    {

        for(int i = 0; i < count; i++) 
        {
            JokerBase jokerBase = ALLJoker.GetJoker((ALLJoker._allJokerEnum)Random.Range(0,(int)ALLJoker._allJokerEnum.MAX));

            _dommyJoker.Add(jokerBase);

            JokerObjectUtility.AddDomyyJoker(jokerBase);



        }



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
    public JokerActionUseEnum.JokerActionTarget GetTarget() { return _target; }

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

        _target=target;

        //JokerObjectUtility.SetNumPos();

    }

    public int GetIndex() {  return useIndex; }

    public void GrabChange(int id,bool flag) 
    {
        JokerObjectUtility.GrabChange(id, flag);

    }

    public void SetSale(int ID) 
    {
        GameObject joker = JokerObjectUtility.GetIDObject(ID);

        SaleUtility.SetSale(_jokers[ID], joker, _jokers[ID].GetSaleValue());
           
    }
    /// <summary>
    /// 全てのジョーカーに何かする関数
    /// </summary>
    public void JokerALLAction(System.Action<JokerBase> action)
    {
        for (int i = 0; i < _jokers.Count; i++)
            action(_jokers[i]);
    }


    /// <summary>
    /// ジョーカーのアップデート処理を回す関数
    /// </summary>
    private void JokerUpData() 
    {

        for(int i=0;i< _jokers.Count; i++) 
        {
            useIndex = i;
            _jokers[i].UpData();
        }

        //ターゲットの初期化
        _target = JokerActionUseEnum.JokerActionTarget.None;

        useIndex = -1;
    }



}
