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
    /// ラウンド中使われない破棄されるカードの座標
    /// </summary>
    [SerializeField] private Vector3 _handTrash = Vector3.zero;

    /// <summary>
    /// トラッシュに移動中のカードの角度の定数
    /// </summary>
    private readonly Vector3 _TRASH_ANGLE = new Vector3(91, 90, 90);

    /// <summary>
    /// カードの基本状態の角度
    /// </summary>
    private readonly Vector3 _NORMALl_ANGLE = new Vector3(91, 90, 90);

    /// <summary>
    /// カードの裏面状態の角度
    /// </summary>
    private readonly Vector3 _BACK_SIDE = new Vector3(-91, 90, 90);


    /// <summary>
    /// 手札のカードの座標の一番左側から右側までの距離
    /// </summary>
    private float _handPositionRange = 0;

    private readonly int _ANGLE_CHANGE_SPEED = 2 * GameConfig.GetGameSpeed();


    /// <summary>
    /// 全てのカードを生成する必要がないかもしれない
    /// </summary>
    private List<CardObject> _cardObjects = new List<CardObject>((int)Card.suit.max * (int)Card.number.king);

    /// <summary>
    /// その時の手札のカード
    /// </summary>
    [SerializeField] private List<CardObject> _cardObjectHands = new List<CardObject>();

    private Material[][] _cardMaterials = new Material[(int)Card.suit.max][];

    /// <summary>
    /// カードの内容を変化する時に使用するIDの入ったリスト
    /// </summary>
    [SerializeField] private List<int> _chengeCardID = new List<int>();
    /// <summary>
    /// カードの内容を変化する時に使用する内容の入ったリスト
    /// </summary>
    private List<Card.Trump> _chengeCardTrump = new List<Card.Trump>();

    /// <summary>
    /// カードを移動させる時に使用するキャッシュ先
    /// </summary>
    private int _movingCard = -1;

    /// <summary>
    /// トランプのマテリアルをまとめたクラス
    /// </summary>
    private TrumpMaterialManager _materialManager;


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
        // トランプのマテリアルをまとめたクラスを取得
        _materialManager = GetComponent<TrumpMaterialManager>();

        // Utilityに登録
        CardObjectUtility.CardObjectManager = this;

        CreateCard();

        // 手札の幅を計算
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

            // 使用可能なカードかを確認
            CardObject cardObject = GetUseCardObject();
            if (cardObject == null) continue;

            cardObject.SetStatus(CardObject.status.hand);

            // 手札に追加
            _cardObjectHands.Add(cardObject);

            // 手札に追加されたカードにマテリアルをセット
            CardPaint(cardDatas[i], i);
        }
    }

    /// <summary>
    /// ハンドの移動を開始する関数
    /// </summary>
    public void StartHandMove()
    {
        for (int i = 0; i < _cardObjectHands.Count; i++)
            _cardObjectHands[i].ResetMoveTime();

    }

    /// <summary>
    /// プレイ準備状態と手札にある状態を切り替える関数
    /// </summary>
    /// <param name="id"></param>
    public void ChengeStandby(int id)
    {
        // 動作の途中での割り込みを制限
        if (_cardObjectHands[id].IsMovable()) return;

        _cardObjectHands[id].SetStatus(_cardObjectHands[id].GetStatus() == CardObject.status.hand ? CardObject.status.playWait : CardObject.status.hand);
        _cardObjectHands[id].ResetMoveTime();
    }

    /// <summary>
    /// 既に表になっているカードに変更を加える関数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="trump"></param>
    public void SetChengeCard(int id, Card.Trump trump)
    {
        // 動作の途中での割り込みを制限
        if (_cardObjectHands[id].IsMovable()) return;


        if (_chengeCardID.Contains(id)) return;
        // 変換をさせる内容を記録
        _chengeCardID.Add(id);
        _chengeCardTrump.Add(trump);

        _cardObjectHands[id].SetStatus(CardObject.status.change);
        _cardObjectHands[id].ResetMoveTime();
    }


    /// <summary>
    /// プレイ準備状態からプレイに移行する関数
    /// </summary>
    /// <returns></returns>
    public void Play()
    {
        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i].GetStatus() != CardObject.status.playWait) continue;
            _cardObjectHands[i].SetStatus(CardObject.status.play);
            _cardObjectHands[i].ResetMoveTime();
        }
    }

    /// <summary>
    /// プレイ準備状態から破棄状態に移行する関数
    /// </summary>
    public void Discard()
    {

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i].GetStatus() != CardObject.status.playWait) continue;
            _cardObjectHands[i].SetStatus(CardObject.status.discard);
            _cardObjectHands[i].ResetMoveTime();
        }

    }

    /// <summary>
    /// プレイが終わって手札とプレイカードを破棄状態にする関数
    /// </summary>
    public void End()
    {
        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            _cardObjectHands[i].SetStatus(CardObject.status.discard);
            _cardObjectHands[i].ResetMoveTime();
        }

    }

    /// <summary>
    /// カードをつまんで移動させる開始時の関数
    /// </summary>
    /// <param name="id"></param>
    public void StartMovingCard(int id)
    {
        if (_movingCard != -1) return;

        // オブジェクトのIDをキャッシュ
        _movingCard = id;

    }

    /// <summary>
    /// カードをつまんで移動させる終了時の関数
    /// </summary>
    public void EndMovingCard()
    {
        _cardObjectHands[_movingCard].SetStatus(CardObject.status.hand);
        _cardObjectHands[_movingCard].ResetMoveTime();
    }


    /// <summary>
    /// つまんでいるカードの移動をする関数
    /// </summary>
    private void MovingCard()
    {
        //何もつまんでいなかったら何もしない
        if (_movingCard == -1) return;

        //　TODO
        // マウスの移動量を参照して
        // _cardObjectHands[_movingCard]の座標を移動させる

        // もしもIDが入れ替わることがあるならば次に進む

        // 入れ替わったIDを正しく入れなおす
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

        //プレイ準備のカウンター
        int playCounter = 0;

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            //移動可能かどうかを確認
            if (!_cardObjectHands[i].IsMovable()) continue;
            _cardObjectHands[i].CountDown();


            //カードの状態ごとの移動処理
            switch (_cardObjectHands[i].GetStatus())
            {
                case CardObject.status.none:
                    break;
                case CardObject.status.deck:
                    break;

                //カードが手札への移動の時の処理
                case CardObject.status.hand:
                    CardMoveHand(_cardObjectHands[i], handCardRange * (i + 1));
                    break;
                case CardObject.status.playWait:
                    CardMovePlayWait(_cardObjectHands[i], handCardRange * (i + 1));
                    break;
                case CardObject.status.play:
                    CardMovePlay(_cardObjectHands[i], _handPositionRange, playCounter);
                    playCounter++;
                    break;
                case CardObject.status.trash:
                    CardMoveDiscard(_cardObjectHands[i]);
                    break;
                case CardObject.status.discard:
                    CardMoveDiscard(_cardObjectHands[i]);
                    break;
                //既に表になっているカードに変更を加える状態
                case CardObject.status.change:
                    HandCardChengeTrump(_cardObjectHands[i], i);
                    break;
            }


        }
    }


    /// <summary>
    /// デッキから手札への移動
    /// </summary>
    /// <param name="cardObjectHand"></param>
    /// <param name="handCardRange"></param>
    private void CardMoveHand(CardObject cardObjectHand, float handCardRange)
    {
        // 移動目標地点を確認
        Vector3 goalPos = _handPositionLeft + new Vector3(handCardRange, 0, 0);

        // 移動量と座標を合計を算出
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 移動
        cardObjectHand.transform.position = moveVec;

        // 角度の算出
        Vector3 angle = Vector3.Lerp(cardObjectHand.GetBeforeAngle(), _NORMALl_ANGLE, cardObjectHand.GetMoveTimeRata());

        // 角度の代入
        cardObjectHand.transform.eulerAngles = angle;


        if (cardObjectHand.IsMovable()) return;

        cardObjectHand.GravityStart();

    }
    /// <summary>
    /// 手札からプレイ準備状態への移動
    /// </summary>
    /// <param name="cardObjectHand"></param>
    /// <param name="handCardRange"></param>
    private void CardMovePlayWait(CardObject cardObjectHand, float handCardRange)
    {
        // 移動目標地点を確認
        Vector3 goalPos = _handPositionLeft + new Vector3(handCardRange, 10, 0);

        // 移動量と座標を合計を算出
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 移動
        cardObjectHand.transform.position = moveVec;

    }
    /// <summary>
    /// 手札からプレイ準備状態への移動
    /// </summary>
    /// <param name="cardObjectHand"></param>
    /// <param name="handCardRange"></param>
    private void CardMovePlay(CardObject cardObjectHand, float handRange, int counter)
    {

        float handCardRange = (handRange / GetPlayCardCount()) * counter;

        // 移動目標地点を確認
        Vector3 goalPos = _handPositionLeft + new Vector3(handCardRange, 0, 10);

        // 移動量と座標を合計を算出
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 移動
        cardObjectHand.transform.position = moveVec;

    }
    /// <summary>
    /// カードをトラッシュに移動させる関数
    /// </summary>
    /// <param name="cardObjectHand"></param>
    private void CardMoveDiscard(CardObject cardObjectHand)
    {

        // 移動目標地点を確認
        Vector3 goalPos = _handTrash;

        // 移動量と座標を合計を算出
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 移動
        cardObjectHand.transform.position = moveVec;

        // 角度の変更
        cardObjectHand.transform.eulerAngles = Vector3.Lerp(_NORMALl_ANGLE, _TRASH_ANGLE,
            (cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED) > 1 ? 1 : cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED);






    }
    /// <summary>
    /// 既に表になっているカードに変更を加える
    /// </summary>
    private void HandCardChengeTrump(CardObject cardObjectHand, int id)
    {

        // 目標角度を設定
        Vector3 goal = _chengeCardID.Contains(id) ? _BACK_SIDE : _NORMALl_ANGLE;

        // 初期角度を設定
        Vector3 start = _chengeCardID.Contains(id) ? _NORMALl_ANGLE : _BACK_SIDE;


        cardObjectHand.transform.eulerAngles = Vector3.Lerp(start, goal,
            (cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED) > 1 ? 1 : cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED);


        // 現在動ける状態かを確認
        if (cardObjectHand.IsMovable()) return;

        // もう一度動けるように変更
        cardObjectHand.ResetMoveTime();

        if (!_chengeCardID.Contains(id)) cardObjectHand.SetStatus(CardObject.status.hand);

        // 変更をしているカードが配列の何番かを確認
        int targetID = _chengeCardID.FindIndex(n => n == id);

        if (targetID < 0) return;

        // 確認した番号の配列を除外
        _chengeCardID.RemoveAt(targetID);
        _chengeCardTrump.RemoveAt(targetID);

    }


    /// <summary>
    /// カードの情報を元にカードのマテリアルをセットする関数
    /// </summary>
    private void CardPaint(Card.Trump cardData, int id)
    {

        MeshRenderer meshRenderer = _cardObjectHands[id].GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        // トランプのエフェクトマテリアルをセット（いまはない）

        // トランプのソーツとナンバーを含んだマテリアルをセット
        // materials[(int)cardMaterialType.main] = _cardMaterials[(int)cardData.suit][(int)cardData.number];

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
            // カードがdeckになかったらもう一度
            if (_cardObjects[i].GetStatus() != CardObject.status.deck) continue;

            return _cardObjects[i];
        }
        // 何も返せる物がない
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




    /// <summary>
    /// プレイ状態のカードの数をカウントする関数
    /// </summary>
    /// <returns></returns>
    private int GetPlayCardCount()
    {
        int count = 0;
        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i].GetStatus() != CardObject.status.play) continue;
            count++;

        }
        return count;
    }

}
