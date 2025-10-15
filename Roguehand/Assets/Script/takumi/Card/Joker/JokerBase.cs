using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerBase : SaleInterface, ExplanationInterface
{
    /// <summary>
    /// ジョーカーのオブジェクトの動き方
    /// </summary>
    protected int jokerObjecttype = 0;

    protected int _saleValue = 0;

    private int explanationID;



    /// <summary>
    /// ジョーカーのバフ内容
    /// </summary>
    Card.JokerBuff _jokerBuff;

    /// <summary>
    /// ジョーカー以外にも着く可能性のあるバフの内容
    /// </summary>
    Card.cardBuff _cardBuff;

    public void SetJokerBuff(Card.JokerBuff buff) { _jokerBuff = buff; }

    public Card.JokerBuff GetJokerBuff() { return _jokerBuff; }
    public void SetCardBuff(Card.cardBuff buff) { _cardBuff = buff; }

    public Card.cardBuff GetCardBuff() { return _cardBuff; }

    public void SetID(int ID) { explanationID = ID; }
    public virtual string Get() { return StringMaster.instance.GetMaster(explanationID); }

    /// <summary>
    /// ジョーカーのオブジェクトの動き方を返す関数
    /// </summary>
    /// <returns></returns>
    public int GetJokerObjectType() { return jokerObjecttype; }
    /// <summary>
    /// ラウンドの開始時のジョーカーの挙動
    /// </summary>
    public virtual void RoundStart() { }

    /// <summary>
    /// 常に回すジョーカーの挙動（基本的に直ぐにリターンで返す関数）
    /// </summary>
    public virtual void UpData() { }

    /// <summary>
    /// ジョーカーのターンが回って来た時に動く挙動
    /// </summary>
    /// <returns><基本ゼロだけどこれが倍率増加量/returns>
    public virtual float Trun() { return 0; }

    /// <summary>
    /// ラウンドの終了時のジョーカーの挙動
    /// </summary>
    public virtual void RoundEnd() { }

    /// <summary>
    /// ターン事の処理をするために挟むリセットの処理
    /// </summary>
    public virtual void TrunReset() { }

    /// <summary>
    /// ジョーカーのレアリティを返す関数
    /// </summary>
    /// <returns></returns>
    public virtual JokerActionUseEnum.JokerRarity GetRarity() { return JokerActionUseEnum.JokerRarity.Common; }

    /// <summary>
    /// 売却額を返す関数
    /// </summary>
    /// <returns></returns>
    public int GetSaleValue() { return _saleValue + (int)GetRarity(); }

    public void AddSaleValue(int add) { _saleValue += add; }


    /// <summary>
    /// ジョーカーの倍率の上昇方法が加算なのか乗算なのかを表す関数
    /// </summary>
    /// <returns></returns>
    public virtual bool GetAddType() { return true; }


    /// <summary>
    /// 売却されたときの挙動
    /// </summary>
    public virtual void SaleAction() { }

    public virtual string  GetName()
    {
        return string.Empty;
    }

    public virtual string GetExplanation()
    {
        return StringMaster.instance.GetMaster(explanationID);
    }

    public virtual string GetTypes()
    {
        return GetRarity().GetJokerRarityNema();

    }

    public virtual string GetExplanation2()
    {
        return string.Empty;
    }

}
