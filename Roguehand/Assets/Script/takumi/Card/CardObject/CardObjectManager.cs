using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardObjectManager : MonoBehaviour
{

    private enum cardMaterialType 
    {
        /// <summary>
        /// トランプの裏面
        /// </summary>
        back,
        /// <summary>
        /// トランプのバフ内容で決まる
        /// </summary>
        effect,
        /// <summary>
        /// トランプのスートとナンバーで決まる
        /// </summary>
        main

    }


    /// <summary>
    /// カードのオブジェクトのベース
    /// </summary>
    [SerializeField] private GameObject _cardBase;

    /// <summary>
    /// 全てのカードを生成する必要がないかもしれない
    /// </summary>
    private List<CardObject> _cardObjects = new List<CardObject>((int)Card.suit.max * (int)Card.number.king);

    private List<CardObject> _cardObjectHands = new List<CardObject>();

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
            _cardObjects.Add(Instantiate(_cardBase, transform).AddComponent<CardObject>());

        }
    }

    /// <summary>
    /// デッキから手札への移動関数
    /// </summary>
    /// <param name="carDatas"><s/param>
    public void HandToCard(List<Card.Trump> cardDatas)
    {
        //cardDatasの中身を確認して取得
        for (int i = 0; i < cardDatas.Count; i++)
        {
            //使用可能なカードかを確認
            CardObject cardObject = GetUseCardObject();
            if (cardObject == null) continue;

            //手札に追加
            _cardObjectHands.Add(cardObject);

            //手札に追加されたカードにマテリアルをセット
            CardPaint(cardDatas[i], i);
        }
    }

    /// <summary>
    /// カードの情報を元にカードのマテリアルをセットする関数
    /// </summary>
    private void CardPaint(Card.Trump cardData, int id)
    {

        MeshRenderer meshRenderer = _cardObjectHands[id].GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;

        //トランプのエフェクトマテリアルをセット（いまはない）

        materials[(int)cardMaterialType.main] = _cardMaterials[cardData.][];





    }

    /// <summary>
    /// 使用可能なカードを返す関数
    /// </summary>
    /// <returns></returns>
    private CardObject GetUseCardObject()
    {
        for (int i = 0; i < _cardObjects.Count; i++)
        {
            //カードがdeckになかったらもう一度
            if (_cardObjects[i].GetStatus() != CardObject.status.deck) continue;

            return _cardObjects[i];
        }
        //何も返せる物がない
        return null;

    }




}
