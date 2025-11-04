using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortHand : MonoBehaviour
{
    public static SortHand instance;

    /// <summary>
    /// ソート時に使用するボタン
    /// </summary>
    [SerializeField] Button siutButton;
    [SerializeField] Button numberButton;


    public void Start()
    {
        instance = this;
        siutButton.onClick.AddListener(OnSortSuitButton);
        numberButton.onClick.AddListener(OnSortNumberButton);
    }


    /// <summary>
    /// ボタンを押された時の処理
    /// </summary>
    private void OnSortSuitButton() 
    {

        List <Card.Trump> nowHand=CardManager.instance.GetHand();

        
        List <Card.Trump> nextHand=CardManager.instance.GetHand();

        nextHand = CardManager.instance.SuitSort(nextHand);

        // ソート後のリストを作成
        CardManager.instance.SetHand(nextHand);


        // ソート後のオブジェクトの並びに変更
        CardObjectUtility.ObjectSort(nowHand, nextHand);

    }
    public void OnSortNumberButton() 
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
