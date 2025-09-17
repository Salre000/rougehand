using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardObjectUtility 
{
    public static CardObjectManager CardObjectManager { set; private get; }

    public static void HandToCard(List<Card.Trump> cardDatas) { CardObjectManager.HandToCard(cardDatas); }

    public static void StartHandMove() {  CardObjectManager.StartHandMove(); }

    public static void ChengeStandby(int id) {  CardObjectManager.ChengeStandby(id);}
    public static void Play() { CardObjectManager.Play(); }
    public static void Discard() { CardObjectManager.Discard(); }
    public static void End() { CardObjectManager.End(); }

    /// <summary>
    /// 既に表になっているカードに変更を加える関数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="trump"></param>
    public static void SetChengeCard(int id, Card.Trump trump) { CardObjectManager.SetChengeCard(id, trump);}

}