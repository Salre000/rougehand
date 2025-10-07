using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaleUtility
{
    public static SaleManager instance {  set;private get; }

    public static void Claer() { instance.Clear(); }
    public static void SetSale(SaleInterface saleInterface, GameObject saleObject, int saleValue) { instance.SetSale(saleInterface,saleObject,saleValue); }

}