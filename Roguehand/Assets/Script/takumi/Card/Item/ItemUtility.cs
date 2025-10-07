using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemUtility
{


    public static ItemManager instance { set; private get; }

    public static void AddItem(int iD) {  instance.AddItem(iD); }

    public static void GrabChange(int ID,bool flag) { instance.GrabChange(ID, flag); }

    public static void ChengeOrder(int lostID,int nextID) {instance.ChengeOrder(lostID,nextID);}

    public static int GetItemIndex(ItemObject item) {  return instance.GetItemIndex(item);}

    public static void SetSale(int ID) {  instance.SetSale(ID); }

}