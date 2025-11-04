using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortHand : MonoBehaviour
{

    /// <summary>
    /// ソート時に使用するボタン
    /// </summary>
    [SerializeField] Button sortButton;


    public void Start()
    {
        sortButton.onClick.AddListener(OnSortButton);
    }


    /// <summary>
    /// ボタンを押された時の処理
    /// </summary>
    private void OnSortButton() 
    {

        List <Card.Trump> nowHand=CardManager.instance.GetHand();

        
        List <Card.Trump> nextHand=CardManager.instance.GetHand();

        nextHand = CardManager.instance.NumberSort(nextHand);

        // ソート後のリストを作成
        CardManager.instance.SetHand(nextHand);


        // ソート後のオブジェクトの並びに変更
        CardObjectUtility.ObjectSort(nowHand, nextHand);

    }

}
