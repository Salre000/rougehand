using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// à–¾•ª‚ğ•`‰æ‚·‚éinterface
/// </summary>
public interface ExplanationInterface 
{

    public enum ExplanationType
    {
        Buy,//w“ü
        Normal,//’Êí



    }

    /// <summary>
    /// à–¾•ª‚Ì•`‰æ‚·‚éŠÖ”
    /// </summary>
    public void CreateExplanation(ExplanationType type,string explanation,Vector3 centerPos) 
    {
        switch (type)
        {
            case ExplanationType.Buy:
                break;
            case ExplanationType.Normal:
                break;
        }
    }

    private void ShowBuy(string explanation, Vector3 centerPos) 
    {


        GUI.Box(new Rect(centerPos.x, Screen.height - centerPos.y + 100, 200, 60),explanation, SaleUtility.GetStyle());




    }
    private void ShowNormal(string explanation, Vector3 centerPos) 
    {

        GUI.Box(new Rect(centerPos.x, Screen.height - centerPos.y + 100, 200, 60),explanation, SaleUtility.GetStyle());





    }





}