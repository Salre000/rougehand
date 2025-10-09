using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バフのユーティリティ
/// </summary>
public static class BuffUtility 
{
    public static BuffManager BuffManager { set; private get; }


    public static void PlayBuff(Card.Trump trump) { BuffManager.PlayBuff(trump); }
    public static void HandBuff(Card.Trump trump) { BuffManager.HandBuff(trump); }
    public static void DiscardBuff(Card.Trump trump) { BuffManager.DiscardBuff(trump); }
    public static void RoundEndBuff(Card.Trump trump) { BuffManager.RoundEndBuff(trump); }

    public static Material GetCardMaterial(int ID) { return BuffManager.GetCardMaterial(ID); }
    public static Material GetTrumpMaterial(int ID) { return BuffManager.GetTrumpMaterial(ID); }
    public static Material GetJokerMaterial(int ID) { return BuffManager.GetJokerMaterial(ID); }
    public static Material GetSealMaterial(int ID) { return BuffManager.GetSealMaterial(ID); }


}