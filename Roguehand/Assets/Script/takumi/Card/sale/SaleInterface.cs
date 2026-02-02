using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScriptCountNumber;
public interface SaleInterface
{

    /// <summary>
    /// ”„‹pŠz‚Ì•`‰æ‚·‚éŠÖ”
    /// </summary>
    public void SaleShow(Vector3 pos, int saleValue, System.Action action)
    {
        Vector2 ButtonPos = Camera.main.WorldToScreenPoint(pos);
        if (GUI.Button(new Rect(ButtonPos.x + 75, Screen.height - ButtonPos.y - 30, 70, 90),
            ("<size=25><color=#ffffff>”„‹p\n$" + saleValue.ToString() + "</color></size>"), SaleUtility.GetStyle()))
        {

            action();

            //‚¨‹à‚ğ‘‚â‚·ˆ—
            GameUtility.SetMyMoney(GameUtility.GetMyMoney() + saleValue);


        }
    }


    /// <summary>
    /// w“ü‚Ì•`‰æ‚ğ‚·‚éŠÖ”
    /// </summary>
    public void BuyShow(Vector3 pos, int saleValue, System.Action action)
    {
        Vector2 ButtonPos = Camera.main.WorldToScreenPoint(pos);

        float BUY_WIDHT = 100;

        if (!AddFlag()) 
        {

            NotAddButton(ButtonPos);

            return;
        }


        if (GUI.Button(new Rect(ButtonPos.x - BUY_WIDHT /HALF, Screen.height - ButtonPos.y + 100, BUY_WIDHT, 60),
            ("<size=30><color=#ffffff>" + Extra.ErrorText("w“ü") + "</color></size>"), SaleUtility.GetStyle()))
        {
            // ‚¨‹à‚ª‘«‚è‚Ä‚¢‚é‚©‚Ç‚¤‚©‚Ì”»’f
            if (GameUtility.GetMyMoney() < saleValue) { Debug.Log("‚¨‹à‚ª‘«‚è‚È‚¢"); return; }

            // ‚¨‹à‚ğŒ¸‚ç‚·ˆ—
            GameUtility.SetMyMoney(GameUtility.GetMyMoney() - saleValue);

            action();

            VolumeManager.instance.PlayMoneyShop();

        }
    }

    /// <summary>
    /// ’Ç‰Á‚ª‰Â”\‚©‚Ç‚¤‚©‚ğ”»’f‚·‚éŠÖ”    
    /// </summary>
    /// <returns></returns>
    public bool AddFlag() {  return true; }

    /// <summary>
    /// ’Ç‰Á‚ğ‚Å‚«‚È‚¢‚Æ‚«‚Ìƒ{ƒ^ƒ“
    /// </summary>
    public void NotAddButton(Vector2 ButtonPos) { }
}