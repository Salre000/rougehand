using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SaleInterface 
{

    /// <summary>
    /// îÑãpäzÇÃï`âÊÇ∑ÇÈä÷êî
    /// </summary>
    public void SaleShow(Vector3 pos,int saleValue)
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Click Me"))
        {
            Debug.Log("Button Clicked!");
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