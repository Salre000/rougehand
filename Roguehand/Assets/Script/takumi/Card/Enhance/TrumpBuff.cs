using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrumpBuff
{

    public static GameObject target;
    public static int targetID;

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
                value = 20;
                ScoreManager.instance.BasicPlus(value);
                break;
            case Card.deckBuff.Magnification:
                value = 10;
                Magnification = true;
                ScoreManager.instance.MagnificationPlus(value);
                break;
            case Card.deckBuff.Glass:

                //　十分の一で発動
                if (Random.Range(0, 10) != 1) return;

                value = (int)ScoreManager.instance.GetMagnification() / 2;
                Magnification = true;
                ScoreManager.instance.MagnificationPlus(value);



                break;
            case Card.deckBuff.Lucky:

                if (Random.Range(0, 10) != 1) return;
                value = 20;
                Magnification = true;
                ScoreManager.instance.MagnificationPlus(value);

                if (Random.Range(0, 10) != 1) return;

                GameUtility.SetMyMoney(GameUtility.GetMyMoney() + 3);




                break;

            case Card.deckBuff.BlindScore:
                int count = 0;

                List<Card.Trump> trumps = CardManager.instance.GetDeck();


                for (int i = 0; i < trumps.Count; i++)
                {
                    if (trumps[i].deckBuff != Card.deckBuff.BlindScore) continue;
                    count++;
                }

                value = count;
                Magnification = true;
                ScoreManager.instance.MagnificationPlus(value);


                break;
        }

        // TODO: 文字を出す
        //value 値
        // target.transform.position 座標
        // Magnification このフラグがtrueの時は倍率falseの時は基本スコア
        //　valueが０の時は出さない
        if (value <= 0) return;
        ScoreManager.instance.SetScoreViewID(targetID);
        ScoreManager.instance.SetScoreViewTrans(target.transform.position);
        ScoreManager.instance.SetScoreViewText(value, Magnification);
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

                bool Magnification = true;
                int value = (int)(ScoreManager.instance.GetMagnification() / 2f);

                ScoreManager.instance.SetScoreViewID(targetID);
                ScoreManager.instance.SetScoreViewTrans(target.transform.position);
                ScoreManager.instance.SetScoreViewText(value, Magnification);




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