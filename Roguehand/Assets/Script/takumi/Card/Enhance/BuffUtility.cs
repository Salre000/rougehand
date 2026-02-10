using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バフのユーティリティ
/// </summary>
public static class BuffUtility 
{
    public static BuffManager BuffManager { set; private get; }


    public static void PlayBuff(Card.cardBuff card) { BuffManager.PlayBuff(card); }
    public static void PlayBuff(Card.JokerBuff card) { BuffManager.PlayBuff(card); }
    public static void HandBuff(Card.Trump trump) { BuffManager.HandBuff(trump); }

    public static Material GetCardMaterial(int ID) { return BuffManager.GetCardMaterial(ID); }
    public static Material GetTrumpMaterial(int ID) { return BuffManager.GetTrumpMaterial(ID); }
    public static Material GetJokerMaterial(int ID) { return BuffManager.GetJokerMaterial(ID); }
    public static Material GetSealMaterial(int ID) { return BuffManager.GetSealMaterial(ID); }
    public static Material GetDommyMaterial() {return BuffManager.GetDommyMaterial(); }
    public static System.Action GetActionPlayBuffCard(Card.cardBuff cardBuff) { return BuffManager.GetActionPlayBuffCard(cardBuff); }
    public static System.Action GetActionPlayBuffDeck(Card.deckBuff cardBuff) { return BuffManager.GetActionPlayBuffDeck(cardBuff); }
    public static System.Action GetActionPlayBuffSeal(Card.sealBuff sealBuff) { return BuffManager.GetActionPlayBuffSeal(sealBuff); }
    public static bool CheckPlayBuffDeck(Card.deckBuff cardBuff) { return BuffManager.CheckPlayBuffDeck(cardBuff); }
    public static bool CheckPlayBuffCard(Card.cardBuff cardBuff) { return BuffManager.CheckPlayBuffCard(cardBuff); }
    public static bool CheckPlayBuffSeal(Card.sealBuff sealBuff) { return BuffManager.CheckPlayBuffSeal(sealBuff); }
    public static bool CheckHandBuffs(Card.Trump trump) { return BuffManager.CheckHandBuffs(trump); }

}