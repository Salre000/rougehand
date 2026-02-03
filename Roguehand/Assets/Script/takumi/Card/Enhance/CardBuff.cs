using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardBuff 
{
    public static GameObject target;

    /// <summary>
    /// カードをプレイした時のバフ
    /// </summary>
    public void Play(Card.cardBuff cardBuff)
    {
        int value = 0;
        bool Magnification = false;
        //対応したバフを記述
        switch (cardBuff)
        {
            case Card.cardBuff.Foil:
                //基本スコアに５０を加算
                value = 50;
                ScoreManager.instance.BasicPlus(value);
                break;
            case Card.cardBuff.Hologram:
                //倍率に１０を加算
                value = 10;
                Magnification = true;
                ScoreManager.instance.MagnificationPlus(value);
                break;
            case Card.cardBuff.Polychrome:
                //倍率に1.5の乗算
                value = (int)(ScoreManager.instance.GetMagnification() / 2f);
                Magnification = true;
                ScoreManager.instance.MagnificationPlus(value);

                break;
            default:
                break;
        }


        // TODO: 文字を出す
        //　value 値
        // target.transform.position 座標
        // Magnificationがtrueの時は倍率falseの時は基本スコア
        // valueが０の時は出さない
        if (value <= 0) return;
        ScoreManager.instance.SetScoreViewTrans(target.transform.position);
        if (Magnification)
            ScoreManager.instance.SetScoreViewText("x+" + value);
        else
            ScoreManager.instance.SetScoreViewText("+" + value);
    }

    /// <summary>
    /// カードをディスカードした時のバフ
    /// </summary>
    public void Discard(Card.cardBuff cardBuff)
    {
        //対応したバフを記述
        switch (cardBuff)
        {
            case Card.cardBuff.None:
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// カードをプレイした時にハンドにある時のバフ
    /// </summary>
    public void Hand(Card.cardBuff cardBuff)
    {
        //対応したバフを記述
        switch (cardBuff)
        {
            case Card.cardBuff.None:
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// ラウンドの終了時に手札にあるときに発動するバフ
    /// </summary>
    /// <param name="cardBuff"></param>
    public void RoundEnd(Card.cardBuff cardBuff)
    {
        //対応したバフを記述
        switch (cardBuff)
        {
            case Card.cardBuff.None:
                break;
            default:
                break;
        }
    }




}