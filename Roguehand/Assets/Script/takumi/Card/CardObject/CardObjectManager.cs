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
        /// トランプのスートとナンバーで決まる
        /// </summary>
        main,
        /// <summary>
        /// トランプの裏面
        /// </summary>
        back,
        /// <summary>
        /// トランプのバフ内容で決まる
        /// </summary>
        effect,

    }


    /// <summary>
    /// カードのオブジェクトのベース
    /// </summary>
    [SerializeField] private GameObject _cardBase;

    /// <summary>
    /// 手札のカードの座標の一番左側
    /// </summary>
    [SerializeField, Header("手札のカードの座標の一番左側")] private Transform _handPositionLeft;
    /// <summary>
    /// 手札のカードの座標の一番右側
    /// </summary>
    [SerializeField, Header("手札のカードの座標の一番右側")] private Transform _handPositionRight;
    /// <summary>
    /// プレイの座標の一番左側
    /// </summary>
    [SerializeField, Header("プレイの座標の一番左側")] private Transform _playPositionLeft;
    /// <summary>
    /// プレイの座標の一番右側
    /// </summary>
    [SerializeField, Header("プレイの座標の一番右側")] private Transform _playPositionRight;

    /// <summary>
    /// ラウンド中使われない破棄されるカードの座標
    /// </summary>
    [SerializeField, Header("ラウンド中使われない破棄されるカードの座標")] private Transform _handTrash;
    /// <summary>
    /// カードオブジェクトを置いていくデッキの基準座標
    /// </summary>
    [SerializeField, Header("デッキの基準座標")] private Transform _cardDeck;

    /// <summary>
    /// トラッシュに移動中のカードの角度の定数
    /// </summary>
    private readonly Vector3 _TRASH_ANGLE = new Vector3(-90, 90, 90);

    /// <summary>
    /// カードの基本状態の角度
    /// </summary>
    private readonly Vector3 _NORMALl_ANGLE = new Vector3(0, -3, 0);

    /// <summary>
    /// カードの裏面状態の角度
    /// </summary>
    private readonly Vector3 _BACK_SIDE = new Vector3(0, 183, 0);

    /// <summary>
    /// プレイ待機状態のときに移動する相対移動量
    /// </summary>
    private readonly Vector3 _PLAY_WAIT = new Vector3(0, 50, -50);


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

    /// <summary>
    /// カードの内容を変化する時に使用するIDの入ったリスト
    /// </summary>
    [SerializeField] private List<int> _chengeCardID = new List<int>();
    /// <summary>
    /// カードの内容を変化する時に使用する内容の入ったリスト
    /// </summary>
    private List<Card.Trump> _chengeCardTrump = new List<Card.Trump>();

    /// <summary>
    /// トランプのマテリアルをまとめたクラス
    /// </summary>
    private TrumpMaterialManager _materialManager;

    /// <summary>
    /// カードオブジェクトを纏めるプール
    /// </summary>
    private GameObject _cardPool;

    private bool _isGrab = false;
    private int _isGrabID = -1;


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

        _materialManager = GetComponent<TrumpMaterialManager>();
        _materialManager.Initializ();


        CreateCard();

        // 手札の幅を計算
        _handPositionRange = Vector3.Distance(_handPositionLeft.position, _handPositionRight.position);


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


    public int GetCardIndex(CardObject cardObject) { return _cardObjectHands.FindIndex(card => card == cardObject); }

    public void GrabChenge(int ID,bool flag)
    {
        _cardObjectHands[ID].SetGrab(flag);
        _isGrab = flag;
        _isGrabID = ID;

        _cardObjectHands[ID].ResetMoveTime();
    }

    public void ChengeOrder(int lostID,int nextID)
    {
        _cardObjectHands = Extra.ChengeOrder(_cardObjectHands, lostID, nextID);

        //ソートの影響で移動するオブジェクトを移動させる

        for (int i = 0; i < _cardObjectHands.Count; i++) _cardObjectHands[i].ResetMoveTime();

        CardManager.instance.SetHand( Extra.ChengeOrder(CardManager.instance.GetHand(),lostID,nextID));
        

    }

    /// <summary>
    /// カードの移動時間をゼロにする
    /// </summary>
    /// <param name="ID"></param>
    public void StopMoveCardObject(int ID) { _cardObjectHands[ID].StopMove();}

    /// <summary>
    /// つまんでいるカードの移動をする関数
    /// </summary>
    private void MovingCard()
    {
        if (!_isGrab) return;


        //ジョーカー同士の距離
        float renge = Vector3.Distance(_handPositionLeft.transform.position, _handPositionRight.transform.position) / (_cardObjectHands.Count + 1);


        float Cardrenge = (_handPositionLeft.transform.position.x + renge * (_isGrabID + 1)) - _cardObjectHands[_isGrabID].transform.position.x;


        //横方向への移動距離が小さかったら順番の変更を加えない
        if (Mathf.Abs(Cardrenge) + 30 < renge) return;

        //移動方向を調整
        int count = 1;
        if (Cardrenge > 1) count = -1;

        if (_isGrabID + count >= _cardObjectHands.Count || _isGrabID + count < 0) return;

        //ジョーカーの順番を入れ替える関数を呼ぶ
        CardObjectUtility.ChengeOrder(_isGrabID, _isGrabID + count);



        _isGrabID = _isGrabID + count;




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

        MovingCard();


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
        Vector3 goalPos = _handPositionLeft.position + new Vector3(handCardRange, 0, 0);

        // 移動量と座標を合計を算出
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());
        // 角度の算出
        Vector3 angle = Vector3.Lerp(cardObjectHand.GetBeforeAngle(), _NORMALl_ANGLE, cardObjectHand.GetMoveTimeRata());

        if (cardObjectHand.IsGrab())
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(_handPositionLeft.transform.position).z-30);
            moveVec = Camera.main.ScreenToWorldPoint(mousePos);

            angle = _NORMALl_ANGLE;
        }


        // 移動
        cardObjectHand.transform.position = moveVec;
        // デッキから出たときだけ角度の代入
        if(cardObjectHand.GetLostStatus()==CardObject.status.deck)cardObjectHand.transform.eulerAngles = angle;


        if (cardObjectHand.IsMovable()) return;

        //その後の仕掛けの為に必要
        cardObjectHand.SetStatus(CardObject.status.hand);
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
        Vector3 goalPos = _handPositionLeft.position + new Vector3(handCardRange, 0, 0) + _PLAY_WAIT;

        // 移動量と座標を合計を算出
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 移動
        cardObjectHand.transform.position = moveVec;

    }
    /// <summary>
    /// 手札からプレイ状態への移動
    /// </summary>
    /// <param name="cardObjectHand"></param>
    /// <param name="handCardRange"></param>
    private void CardMovePlay(CardObject cardObjectHand, float handRange, int counter)
    {

        float vec = (Vector3.Distance(_playPositionLeft.position, _playPositionRight.position) / (GetPlayCardCount()+1))*(counter+1);


        // 移動目標地点を確認
        Vector3 goalPos = _playPositionLeft.position+new Vector3(vec,0,0);

        Debug.Log("座標:" + vec);

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
        Vector3 goalPos = _handTrash.position;

        // 移動量と座標を合計を算出
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 移動
        cardObjectHand.transform.position = moveVec;

        // 角度の変更
        cardObjectHand.transform.eulerAngles = Vector3.Lerp(_NORMALl_ANGLE, _TRASH_ANGLE,
            (cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED) > 1 ? 1 : cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED);


        if (cardObjectHand.GetMoveTimeRata() < 1) return;

        _cardObjectHands.Remove(cardObjectHand);




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

        MeshRenderer meshRenderer = _cardObjectHands[id].transform.GetChild(0).GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        // トランプのエフェクトマテリアルをセット（いまはない）

        // トランプのソーツとナンバーを含んだマテリアルをセット
         materials[(int)cardMaterialType.main] = _materialManager.GetMaterial((int)cardData.suit,(int)cardData.number);

        meshRenderer.materials = materials;
    }

    /// <summary>
    /// 使用可能なカードを返す関数
    /// </summary>
    /// <returns></returns>
    private CardObject GetUseCardObject()
    {
        for (int i = _cardObjects.Count-1; i >=0; i--)
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
        _cardPool = new GameObject("CardPool");
        for (int i = 0; i < (int)Card.suit.max * (int)Card.number.king; i++)
        {
            _cardObjects.Add(Instantiate(_cardBase, _cardDeck.position, Quaternion.identity).AddComponent<CardObject>());
            _cardObjects[i].SetStatus(CardObject.status.deck);
            _cardObjects[i].transform.eulerAngles = _BACK_SIDE;
            _cardObjects[i].transform.parent = _cardPool.transform;
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
