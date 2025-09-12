using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardObjectManager : MonoBehaviour
{
    /// <summary>
    /// カードのオブジェクトのベース
    /// </summary>
    [SerializeField] private GameObject _cardBase;

    /// <summary>
    /// 全てのカードを生成する必要がないかもしれない
    /// </summary>
    private List<GameObject> _cardObjects = new List<GameObject>((int)Card.suit.max * (int)Card.number.king);

    private Material[][] _cardMaterials = new Material[(int)Card.suit.max][];



    public void Awake()
    {
        Initialize();

    }

    
    public void Initialize()
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
            _cardObjects.Add(Instantiate(_cardBase, transform));

        }
    }

    /// <summary>
    /// デッキから手札への移動関数
    /// </summary>
    /// <param name="carDatas"><s/param>
    public void HandToCard(List<Card> cardDatas)
    {

    }

    /// <summary>
    /// カードの情報を元にカードのマテリアルをセットする関数
    /// </summary>
    private void CardPaint(Card cardData)
    {





    }




}
