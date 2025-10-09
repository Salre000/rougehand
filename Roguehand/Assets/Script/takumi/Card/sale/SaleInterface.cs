using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SaleInterface 
{

    /// <summary>
    /// îÑãpäzÇÃï`âÊÇ∑ÇÈä÷êî
    /// </summary>
    public void SaleShow(Vector3 pos,int saleValue,System.Action action)
    {
        Vector2 ButtonPos=Camera.main.WorldToScreenPoint(pos);
        if (GUI.Button(new Rect(ButtonPos.x+75,Screen.height-ButtonPos.y-30, 70, 90), 
            ("<size=25><color=#ffffff>îÑãp\n$" + saleValue.ToString()+ "</color></size>"), SaleUtility.GetStyle()))
        {

            action();

        }
    }

    //çwì¸éûÇÃï`âÊÇÇ∑ÇÈä÷êî
    public void BuyShow(Vector3 pos, int saleValue, System.Action action) 
    {
        Vector2 ButtonPos = Camera.main.WorldToScreenPoint(pos);

        if (GUI.Button(new Rect(ButtonPos.x, Screen.height - ButtonPos.y + 100, 200, 60),
            ("<size=25><color=#ffffff>$" + saleValue.ToString() + "</color></size>"), SaleUtility.GetStyle()))
        {

            action();
        }
    }
} 