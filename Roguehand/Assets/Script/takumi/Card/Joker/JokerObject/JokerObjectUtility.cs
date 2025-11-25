using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JokerObjectUtility 
{

    public static JokerObjectManager instance{ private get; set; }

    public static void NestJokerPlay(JokerObject  jokerObject) { instance.NestJokerPlay(jokerObject); } 
    public static void NextAction(JokerObject  jokerObject) { instance.NextAction(jokerObject); } 

    public static void StartJokerPlay() {  instance.StartJokerPlay(); }
    public static bool PlayCheck() {return  instance.PlayCheck(); }

    public static void AddJoker(JokerBase jokerBase) { instance.AddJoker(jokerBase); }
    public static void AddDomyyJoker(JokerBase jokerBase) { instance.AddDommyJoker(jokerBase); }
    public static void RemoveJoker(int ID) { instance.RemoveJoker(ID); }
    public static void RemoveDomyyJoker() { instance.DommyDestroy(); }

    public static int GetJokerIndex(JokerObject jokerObject) {return instance.GetJokerIndex(jokerObject); }

    public static void GrabChange(int ID ,bool flag) {  instance.GrabChange(ID ,flag); }

    public static void ChengeOrder(int lostID,int nextID) {instance.ChengeOrder(lostID,nextID);}

    public static void CardAddAction(int ID,int AddNum) { instance.CardAddPlay(ID, AddNum);}

    public static void SetNumPos(Vector2 vector) { instance.SetNumPos(vector); }
    public static Vector2 GetNumPos() {return instance.GetNumPos(); }

    public static int GetActionCount() { return instance.ActionCount(); }

    public static GameObject GetIDObject(int id) { return instance.GetIDObject(id); }
    public static JokerObject GetIDJokerObject(int id) { return instance.GetIDJokerObject(id); }
    public static void JokerObjectALLAction(System.Func<JokerObject, JokerObject> funk) {instance.JokerObjectALLAction(funk);}
}