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

    public static void SetChengeCard(int id, Card.Trump trump) { CardObjectManager.SetChengeCard(id, trump);}

}