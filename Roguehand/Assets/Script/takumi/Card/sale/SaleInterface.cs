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

        if (GUI.Button(new Rect(ButtonPos.x+50,Screen.height-ButtonPos.y-30, 100, 100), "Click Me"))
        {

            action();

        }
    }

    public void BuyShow(Vector3 pos, int saleValue) 
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Click Me"))
        {
            Debug.Log("Button Clicked!");
        }



    }




} 