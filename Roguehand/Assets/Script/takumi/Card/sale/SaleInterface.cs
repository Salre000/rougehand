using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SaleInterface 
{

    /// <summary>
    /// ”„‹pŠz‚Ì•`‰æ‚·‚éŠÖ”
    /// </summary>
    public void SaleShow(Vector3 pos,int saleValue,System.Action action)
    {
        Vector2 ButtonPos=Camera.main.WorldToScreenPoint(pos);

        if (GUI.Button(new Rect(ButtonPos.x+50,Screen.height-ButtonPos.y-30, 100, 100), "BAIKYAKU(TEST)"+ saleValue.ToString()))
        {

            action();

        }
    }

    //w“ü‚Ì•`‰æ‚ğ‚·‚éŠÖ”
    public void BuyShow(Vector3 pos, int saleValue) 
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "Click Me"))
        {
            Debug.Log("Button Clicked!");
        }



    }




} 