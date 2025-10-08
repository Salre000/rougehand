using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineBlendDefinition;

public static class SaleUtility
{
    public static SaleManager instance {  set;private get; }

    public static void Claer() { instance.Clear(); }
    public static void SetSale(SaleInterface saleInterface, GameObject saleObject, int saleValue,bool flag=true) { instance.SetSale(saleInterface,saleObject,saleValue,flag); }
    public static GUIStyle GetStyle() { return instance.GetStyle(); }

}