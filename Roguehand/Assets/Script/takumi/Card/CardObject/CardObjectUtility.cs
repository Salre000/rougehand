using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardObjectUtility 
{
    public static CardObjectManager CardObjectManager { set; private get; }

    public static void HandToCard(List<Card.Trump> cardDatas) { CardObjectManager.HandToCard(cardDatas); }

    public static void StartHandMove() {  CardObjectManager.StartHandMove(); }


}