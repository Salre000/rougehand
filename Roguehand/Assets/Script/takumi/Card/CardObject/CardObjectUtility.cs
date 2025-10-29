using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardObjectUtility 
{
    public static CardObjectManager CardObjectManager { set; private get; }

    public static void HandToCard(List<Card.Trump> cardDatas) { CardObjectManager.HandToCard(cardDatas); }

    public static void StartHandMove() {  CardObjectManager.StartHandMove(); }

    public static void ChengeStandby(int id,bool isSelect) {  CardObjectManager.ChengeStandby(id, isSelect);}
    public static void Play() { CardObjectManager.Play(); }
    public static void PlayEnd() { CardObjectManager.PlayEnd(); }
    public static void Discard() { CardObjectManager.Discard(); }
    public static void End() { CardObjectManager.End(); }

    public static void SetChengeCard(int id, Card.Trump trump) { CardObjectManager.SetChengeCard(id, trump);}

    public static int GetCardObjectIndex(CardObject card)  { return CardObjectManager.GetCardIndex(card); }
    public static void GrabChenge(int ID,bool flag)  {  CardObjectManager.GrabChenge(ID,flag); }

    public static void ChengeOrder(int lostID,int nextID) { CardObjectManager.ChengeOrder(lostID, nextID); }

    public static void StopCardObject(int ID) { CardObjectManager.StopMoveCardObject(ID); }

    public static void ShowExplanation(Card.Trump trump,int ID) { CardObjectManager.ShowExplanation(trump,ID); }

    public static bool CheckGrab(int ID) {return CardObjectManager.CheckGrab(ID); }

}