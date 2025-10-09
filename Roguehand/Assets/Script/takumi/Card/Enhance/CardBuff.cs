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
            case Card.cardBuff.None:
                break;
            case Card.cardBuff.Foil:
                break;
            case Card.cardBuff.Hologram:
                break;
            case Card.cardBuff.Polychrome:
                break;
            case Card.cardBuff.MouseJammer:
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