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

    public static int GetCardObjectIndex(CardObject card)  { return CardObjectManager.GetGrabCardIndex(card); }
    public static void GrabChenge(int ID,bool flag)  {  CardObjectManager.GrabChenge(ID,flag); }

    public static void ChengeOrder(int lostID,int nextID) { CardObjectManager.ChengeOrder(lostID, nextID); }

    public static void StopCardObject(int ID) { CardObjectManager.StopMoveCardObject(ID); }

    public static void ShowExplanation(Card.Trump trump,int ID) { CardObjectManager.ShowExplanation(trump,ID); }
    public static void ShowExplanation(Card.Trump trump,GameObject _object) { CardObjectManager.ShowExplanation(trump,_object); }

    public static bool CheckGrab(int ID) {return CardObjectManager.CheckGrab(ID); }

    public static void ResetCard() { CardObjectManager.RoundReset(); }

    public static void ObjectSort(List<Card.Trump> nowHand, List<Card.Trump> nexthand) { CardObjectManager.ObjectSort(nowHand, nexthand); }

    public static int GetActionCount() { return CardObjectManager.GetActionCount(); }
    public static void ActionStart() { CardObjectManager.PlayStart(); }

    public static bool IsPlaying() { return CardObjectManager.IsPlaying(); }

    public static Material GetMaterial(int suit,int number) { return CardObjectManager.GetTrunpMatarial(suit, number); }

}