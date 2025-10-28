using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBuff 
{

    /// <summary>
    /// カードをプレイした時のバフ
    /// </summary>
    public void Play(Card.cardBuff cardBuff)
    {
        //対応したバフを記述
        switch (cardBuff)
        {
            case Card.cardBuff.Foil:
                //基本スコアに５０を加算
                ScoreManager.instance.BasicPlus(50);
                break;
            case Card.cardBuff.Hologram:
                //倍率に１０を加算
                ScoreManager.instance.MagnificationPlus(10);
                break;
            case Card.cardBuff.Polychrome:
                //倍率に1.5の乗算

                break;
            default:
                break;
        }
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