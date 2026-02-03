using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpBuff
{

    public static GameObject target;

    /// <summary>
    /// カードをプレイした時のバフ
    /// </summary>
    public void Play(Card.deckBuff deckBuff)
    {
        bool Magnification = false;
        int value = 0;

        //対応したバフを記述
        switch (deckBuff)
        {
            case Card.deckBuff.Bonus:
                value = 30;
                ScoreManager.instance.BasicPlus(value);
                break;
            case Card.deckBuff.Magnification:
                value = 10;
                Magnification = true;
                ScoreManager.instance.MagnificationPlus(value);
                break;
            case Card.deckBuff.Wild:
                break;
            case Card.deckBuff.Glass:

                //　十分の一で発動
                if (Random.Range(0, 10) != 1) return;

                value =(int)ScoreManager.instance.GetMagnification();
                Magnification = true;
                ScoreManager.instance.MagnificationPlus(value);



                break;
            case Card.deckBuff.Lucky:
                break;
        }

        // TODO: 文字を出す
        //value 値
        // target.transform.position 座標
        // Magnification このフラグがtrueの時は倍率falseの時は基本スコア
        //　valueが０の時は出さない
        if(value <= 0) return;
        ScoreManager.instance.SetScoreViewTrans(target.transform.position);
        if(Magnification)
            ScoreManager.instance.SetScoreViewText("x" + value, Magnification);
        else
            ScoreManager.instance.SetScoreViewText("+" + value, Magnification);

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