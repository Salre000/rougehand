using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardObjectManager : MonoBehaviour
{
    /// <summary>
    /// トランプのマテリアル番号の列挙体
    /// </summary>
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
    /// 手札のカードの座標の一番左側
    /// </summary>
    [SerializeField] private Vector3 _handPositionLeft = Vector3.zero;
    /// <summary>
    /// 手札のカードの座標の一番右側
    /// </summary>
    [SerializeField] private Vector3 _handPositionRight = Vector3.zero;

    /// <summary>
    /// 手札のカードの座標の一番左側から右側までの距離
    /// </summary>
    private float _handPositionRange = 0;


    /// <summary>
    /// 全てのカードを生成する必要がないかもしれない
    /// </summary>
    private List<CardObject> _cardObjects = new List<CardObject>((int)Card.suit.max * (int)Card.number.king);

   [SerializeField] private List<CardObject> _cardObjectHands = new List<CardObject>();

    private Material[][] _cardMaterials = new Material[(int)Card.suit.max][];



    public void Awake()
    {
        Initialize();

    }
    public void Update()
    {
        HandCardSetPosition();
    }


    public void Initialize()
    {
        //Utilityに登録
        CardObjectUtility.CardObjectManager = this;

        CreateCard();

        //手札の幅を計算
        _handPositionRange = Vector3.Distance(_handPositionLeft, _handPositionRight);

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

            cardObject.SetStatus(CardObject.status.hand);

            //手札に追加
            _cardObjectHands.Add(cardObject);

            //手札に追加されたカードにマテリアルをセット
            CardPaint(cardDatas[i], i);
        }
    }

    /// <summary>
    /// ハンドの移動を開始する関数
    /// </summary>
    public void StartHandMove() 
    {
        for(int i = 0; i < _cardObjectHands.Count; i++)
            _cardObjectHands[i].ResetMoveTime();

    }

    /// <summary>
    /// ハンドカードオブジェクトの座標を移動させて定位置に移動させる関数
    /// </summary>
    private void HandCardSetPosition()
    {
        //ハンドの枚数
        int handCardCount = _cardObjectHands.Count;

        //カードとカードの間
        float handCardRange = _handPositionRange / (float)(handCardCount + 1f);

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            //移動可能かどうかを確認
            if (!_cardObjectHands[i].IsMovable()) continue;
            if (_cardObjectHands[i].GetStatus() != CardObject.status.hand) continue;    

            _cardObjectHands[i].CountDown();

            //移動目標地点を確認
            Vector3 goalPos= _handPositionLeft+new Vector3(handCardRange*(i+1),0,0);

            //移動量と座標を合計を算出
            Vector3 moveVec=Vector3.Lerp(_cardObjectHands[i].GetBeforePosition(), goalPos, _cardObjectHands[i].GetMoveTimeRata());

            //移動
            _cardObjectHands[i].transform.position = moveVec;
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

        //トランプのソーツとナンバーを含んだマテリアルをセット
        //materials[(int)cardMaterialType.main] = _cardMaterials[(int)cardData.suit][(int)cardData.number];

        meshRenderer.materials = materials;
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


    /// <summary>
    /// 52枚生成する関数
    /// </summary>
    private void CreateCard()
    {
        for (int i = 0; i < (int)Card.suit.max * (int)Card.number.king; i++)
        {
            _cardObjects.Add(Instantiate(_cardBase, transform).AddComponent<CardObject>());
            _cardObjects[i].SetStatus(CardObject.status.deck);

        }
    }




}
