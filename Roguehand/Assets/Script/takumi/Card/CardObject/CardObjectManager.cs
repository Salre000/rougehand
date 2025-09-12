using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject cardBase;

    /// <summary>
    /// 全てのカードを生成する必要がないかもしれない
    /// </summary>
    private List<GameObject> cardObjects = new List<GameObject>((int)Card.suit.max * (int)Card.number.king);

    private Material[][] cardMaterial = new Material[(int)Card.suit.max][];


    public void Awake()
    {
        CreateCard();

    }

    /// <summary>
    /// 52枚生成する関数
    /// </summary>
    private void CreateCard()
    {
        for (int i = 0; i < (int)Card.suit.max * (int)Card.number.king; i++)
        {
            cardObjects.Add(GameObject.Instantiate(cardBase,transform));

        }
    }

    /// <summary>
    /// デッキから手札への移動関数
    /// </summary>
    /// <param name="carDatas"><s/param>
    public void HandToCard(List<Card> carDatas) 
    {



    }

    /// <summary>
    /// カードの情報を元にカードのマテリアルをセットする関数
    /// </summary>
    private void CardPaint(Card cardData)
    {
        




    }




}
