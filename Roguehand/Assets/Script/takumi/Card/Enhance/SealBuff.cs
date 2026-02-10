using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealBuff
{

    public static GameObject target;
    public static int targetID;

    private List<int> cashIndexs=new List<int>();

    /// <summary>
    /// カードをプレイした時のバフ
    /// </summary>
    public void Play(Card.sealBuff sealBuff)
    {
        bool Magnification = false;
        int value = 0;

        //対応したバフの効果を記述
        switch (sealBuff)
        {
            case Card.sealBuff.Red:

                CardObject cardObject = target.GetComponent<CardObject>();

                int cardIndex = CardObjectUtility.GetCardHands().IndexOf(cardObject);

                if (cashIndexs.Contains(cardIndex)) return;
                int cashIndex = cardIndex;
                cashIndexs.Add(cashIndex);

                int dommyCount = SaleObjectManager.instance.GetDynamicActionCount();
                int dommyCountCash = dommyCount;

                // 常に検知可能なアクションリストがこれだけなので利用
                SaleObjectManager.instance.AddDynamicAction(() =>
                {
                    if (CardObjectUtility.GetCardHands()[cashIndex].
                    GetStatus() == CardObject.status.action) return;

                    CardObjectUtility.GetCardHands()[cashIndex].SetStatus(CardObject.status.action);

                    CardObjectUtility.GetCardHands()[cashIndex].GetCheckBuff(
                        CardManager.instance.GetHand()[cashIndex]
                        ,
                         CardObjectUtility.CardObjectManager.TrunpScore,
                        cashIndex);
                    CardObjectUtility.GetCardHands()[cashIndex].ResetMoveTime();
                    CardObjectUtility.GetCardHands()[cashIndex].SetGrab(true);

                    SaleObjectManager.instance.RemoveDynamicAction(dommyCountCash);


                });


                break;
            case Card.sealBuff.Green:

                int youngId = -1;
                int index = 0;

                // 常に検知可能なアクションリストがこれだけなので利用
                SaleObjectManager.instance.AddDynamicAction(() =>
                {

                    if (CardObjectUtility.GetActionCount() > 0) return;

                    CardObjectUtility.GetCardHands().GetAction(card =>
                    {

                        if (youngId != -1) return card;

                        if (card.GetStatus() == CardObject.status.hand)
                        {

                            CardObjectUtility.RemoveTrump(
                                CardManager.instance.GetHand()[index]);
                            youngId = 1;
                        }
                        index++;

                        return card;
                    });


                    SaleObjectManager.instance.RemoveDynamicAction(0);

                });

                break;
            case Card.sealBuff.Orange:

                int upValue = 2;
                JokerUtility.JokerALLAction(joker =>
                {
                    joker.AddSaleValue(upValue);

                    JokerUtility.JokerChenge(JokerUtility.GetIndex(joker));

                });


                break;
            case Card.sealBuff.Black:

                int count = 0;

                List<Card.Trump> trumps = CardManager.instance.GetDeck();


                for (int i = 0; i < trumps.Count; i++)
                {
                    if (trumps[i].sealBuff != Card.sealBuff.Black) continue;
                    count++;
                }
                value = count;
                Magnification = true;
                ScoreManager.instance.MagnificationPlus(value);



                break;
            default:
                break;
        }

        if (value <= 0) return;
        ScoreManager.instance.SetScoreViewID(targetID);
        ScoreManager.instance.SetScoreViewTrans(target.transform.position);
        ScoreManager.instance.SetScoreViewText(value, Magnification);
    }
    /// <summary>
    /// カードをプレイした時に手札にあると発動するバフ
    /// </summary>
    /// <param name="sealBuff"></param>
    public void Hand(Card.sealBuff sealBuff)
    {
        //対応したバフの効果を記述
        switch (sealBuff)
        {

        }



    }

    /// <summary>
    /// カードをディスカードした時のバフ
    /// </summary>
    public void Discard(Card.sealBuff sealBuff)
    {
        //対応したバフの効果を記述
        switch (sealBuff)
        {
            case Card.sealBuff.Purple:

                // アイテムをランダム生成
                ItemUtility.AddItem(Random.Range(0, (int)ALLItem.ALLItemEnum._MAX));

                break;
        }



    }
    /// <summary>
    /// ラウンドの終了時に手札にあるときのバフ
    /// </summary>
    /// <param name="sealBuff"></param>
    public void RoundEnd(Card.sealBuff sealBuff)
    {
        //対応したバフの効果を記述
        switch (sealBuff)
        {
            case Card.sealBuff.Bule:
                // 星座カードを生成
                ItemUtility.AddItem(0);
                break;
        }


        Reset();
    }

    private void Reset() 
    {
        cashIndexs.Clear();
    }




}