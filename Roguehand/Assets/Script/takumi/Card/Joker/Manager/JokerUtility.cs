using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JokerUtility
{

    public static JokerManager instance { private get; set; }

    public static void Remove(JokerBase joker) { instance.Remove(joker); }
    public static void Remove(int ID) { instance.Remove(ID); }

    public static JokerActionUseEnum.JokerActionTarget GetTarget() { return instance.GetTarget(); }
    public static Card.suit GetTargetSuit() { return instance.GetTargetSuit(); }
    public static Card.number GetTargetNumber() { return instance.GetTargetNumer(); }
    public static RoleManager.Role GetTargetRole() { return instance.GetTargetRole(); }

    public static void AddMagnification(float magnification) {instance.JokerAddMagnification(magnification);}

    public static void AddJoker(int ID) { instance.AddJoker(ID); }

    public static void AddJoker(JokerBase jokerBase) { instance.AddJoker(jokerBase); }

    public static void JokerPlayStart() { JokerObjectUtility.StartJokerPlay(); }

    public static void RoundStartJoker() { instance.RoundStart(); }
    public static void RoundEndJoker() { instance.RoundEnd(); }

    public static void ChengeOrder(int lostID,int nextID) { instance.ChengeOrder(lostID, nextID); }

    public static int GetIndex() {  return instance.GetIndex(); }   
    public static int GetIndex(JokerBase jokerBase) {  return instance.GetIndex(jokerBase); }   

    public static void SetTraget(JokerActionUseEnum.JokerActionTarget target) { instance.SetTarget(target); }
    public static void SetTragetSuit(Card.suit target) { instance.SetTarget(target); }
    public static void SetTragetNumber(Card.number target) { instance.SetTarget(target); }
    public static void SetTragetRole(RoleManager.Role target) { instance.SetTarget(target); }

    /// <summary>
    /// トランプ単位で今使用したものを持つ関数
    /// </summary>
    /// <param name="trump"></param>
    public static void SetNowCrad(Card.Trump trump) { SetTragetNumber(trump.number);SetTragetSuit(trump.suit); }

    public static void GrabChange(int ID,bool flag) { instance.GrabChange(ID,flag); }

    public static void SetSale(int ID) { instance.SetSale(ID);}

    public static void JokerALLAction(System.Action<JokerBase> action) { instance.JokerALLAction(action); }
    public static List<JokerBase> GetJokers() { return instance.GetJoker(); }

    public static void SaleAction(int ID) { instance.SaleAction(ID);}

    public static void JokerChenge(int ID) {instance.JokerChenge(ID);}

    public static void SetMaterial(int ID) { instance.SetMaterial(ID); }

    public static void ShowExplanation(int ID) { instance.ShowExplanation(ID);}
    public static void ShowExplanation(GameObject gameObject,JokerBase jokerBase,Vector2 offset) 
    { instance.ShowExplanation(gameObject,jokerBase,offset);}
     
    public static void ShopJoker(System.Func<JokerBase> func = null) { instance.ShopJokerAdd(func); }

    public static bool JokerAddCheck() {  return instance.JokerAddCheck(); }
}