using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpBuff
{

    /// <summary>
    /// カードをプレイした時のバフ
    /// </summary>
    public void Play(Card.deckBuff deckBuff)
    {

        //対応したバフを記述
        switch (deckBuff)
        {
            case Card.deckBuff.Bonus:
                ScoreManager.instance.BasicPlus(30);
                break;
            case Card.deckBuff.Magnification:
                ScoreManager.instance.MagnificationPlus(10);
                break;
            case Card.deckBuff.Wild:
                break;
            case Card.deckBuff.Glass:

                //　十分の一で発動
                if (Random.Range(0, 10) != 0) return;


                break;
            case Card.deckBuff.Lucky:
                break;
        }
    }

    /// <summary>
    /// カードをディスカードした時のバフ
    /// </summary>
    public void Discard(Card.deckBuff deckBuff)
    {

        //対応したバフを記述
        switch (deckBuff)
        {
        }
    }

    /// <summary>
    /// カードをプレイした時にハンドにある時のバフ
    /// </summary>
    public void Hand(Card.deckBuff deckBuff)
    {

        //対応したバフを記述
        switch (deckBuff)
        {
            case Card.deckBuff.Steel:
                //倍率に１．５倍
                break;
        }
    }
    /// <summary>
    /// ラウンドの終了時に手札にあるときに発動するバフ
    /// </summary>
    /// <param name="deckBuff"></param>
    public void RoundEnd(Card.deckBuff deckBuff)
    {

        //対応したバフを記述
        switch (deckBuff)
        {
            case Card.deckBuff.Gold:
                //固定値のお金を上昇
                break;
        }
    }




}