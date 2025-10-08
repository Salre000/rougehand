using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JokerUtility
{

    public static JokerManager instance { private get; set; }

    public static void Remove(JokerBase joker) { instance.Remove(joker); }
    public static void Remove(int ID) { instance.Remove(ID); }

    public static JokerActionUseEnum.JokerActionTarget GetTarget() { return instance.GetTarget(); }

    public static void AddMagnification(float magnification) {instance.JokerAddMagnification(magnification);}

    public static void Addjoker(int ID) { instance.AddJoker(ID); }

    public static void JokerPlayStart() { JokerObjectUtility.StartJokerPlay(); }

    public static void RoundStartJoker() { instance.RoundStart(); }
    public static void RoundEndJoker() { instance.RoundEnd(); }

    public static void ChengeOrder(int lostID,int nextID) { instance.ChengeOrder(lostID, nextID); }

    public static int GetIndex() {  return instance.GetIndex(); }   

    public static void SetTraget(JokerActionUseEnum.JokerActionTarget target) { instance.SetTarget(target); }

    public static void GrabChange(int ID,bool flag) { instance.GrabChange(ID,flag); }

    public static void SetSale(int ID) { instance.SetSale(ID);}

    public static void JokerALLAction(System.Action<JokerBase> action) { instance.JokerALLAction(action); }

    public static void SaleAction(int ID) { instance.SaleAction(ID);}

}