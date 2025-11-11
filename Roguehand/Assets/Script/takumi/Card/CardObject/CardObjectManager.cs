using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
public class CardObjectManager : MonoBehaviour
{
    /// <summary>
    /// トランプのマテリアル番号の列挙体
    /// </summary>
    public enum cardMaterialType
    {
        /// <summary>
        /// トランプのバフ内容で決まる
        /// </summary>
        effect,

        /// <summary>
        /// トランプの裏面
        /// </summary>
        back,
        /// <summary>
        /// トランプのスートとナンバーで決まる
        /// </summary>
        main,

        buff,

        sael

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
    private readonly Vector3 _PLAY_WAIT = new Vector3(0, 50, 0);


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


    public Material GetTrunpMatarial(int suit, int number) { return _materialManager.GetMaterial(suit, number); }


    /// <summary>
    /// デッキから手札への移動関数
    /// </summary>
    /// <param name="carDatas"><s/param>
    public void HandToCard(List<Card.Trump> cardDatas)
    {
        //選択中をリセット
        CardManager.instance.ResetPick();

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
            CardPaint(cardDatas[i], _cardObjectHands.Count - 1);

            int index = CardManager.instance.GetHand().IndexOf(cardDatas[i]);

            for (int j = _cardObjectHands.Count - 1; j > index; j--)
            {
                _cardObjectHands[j] = _cardObjectHands[j - 1];

            }

            _cardObjectHands[index] = cardObject;


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
    public void ChengeStandby(int id, bool isSelect)
    {
        // 動作の途中での割り込みを制限
        if (_cardObjectHands[id].IsMovable()) return;


        _cardObjectHands[id].SetStatus(isSelect ? CardObject.status.playWait : CardObject.status.hand);
        _cardObjectHands[id].ResetMoveTime();
    }

    public bool CheckGrab(int ID)
    {
        bool flag = true;

        if (_cardObjectHands[ID].GetStatus() == CardObject.status.play) flag = false;



        return flag;
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

    public void PlayEnd()
    {
        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i].GetStatus() != CardObject.status.play) continue;
            _cardObjectHands[i].SetStatus(CardObject.status.discard);
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
    /// 掴みの処理に使用する番号を返す関数
    /// </summary>
    /// <param name="cardObject"></param>
    /// <returns></returns>
    public int GetGrabCardIndex(CardObject cardObject)
    {
        int index = _cardObjectHands.FindIndex(card => card == cardObject);

        if (index < 0) return -1;

        // つかむことの出来る対象かどうかを判断
        bool returnFlag = true;

        //動いている時に不正値
        if (cardObject.IsMovable()) returnFlag = false;

        // 動いていても捕まれていたら正常値
        if (cardObject.IsGrab()) returnFlag = true;

        // プレイ中だと問答無用で不正値
        if (cardObject.GetStatus() == CardObject.status.play) returnFlag = false;

        return returnFlag ? index : -1;
    }

    public void GrabChenge(int ID, bool flag)
    {

        _cardObjectHands[ID].SetGrab(flag);
        _isGrab = flag;
        _isGrabID = ID;

        if (flag) _cardObjectHands[ID].SetStatus(CardObject.status.hand);

        else if (_cardObjectHands[ID].GetLostStatus() == CardObject.status.playWait) _cardObjectHands[ID].SetStatus(CardObject.status.playWait);

        _cardObjectHands[ID].ResetMoveTime();



    }

    public void ChengeOrder(int lostID, int nextID)
    {
        _cardObjectHands = Extra.ChengeOrder(_cardObjectHands, lostID, nextID);

        //ソートの影響で移動するオブジェクトを移動させる

        for (int i = 0; i < _cardObjectHands.Count; i++) _cardObjectHands[i].ResetMoveTime();

        CardManager.instance.SetHand(Extra.ChengeOrder(CardManager.instance.GetHand(), lostID, nextID));


    }

    /// <summary>
    /// カードの移動時間をゼロにする
    /// </summary>
    /// <param name="ID"></param>
    public void StopMoveCardObject(int ID) { _cardObjectHands[ID].StopMove(); }

    /// <summary>
    /// カードの情報を描画状態に変更する
    /// </summary>
    /// <param name="trump"></param>
    /// <param name="ID"></param>
    public void ShowExplanation(Card.Trump trump, int ID)
    {
        //説明を描画させるダミーのクラス
        DommyExplanation dommyExplanation = new DommyExplanation();

        //名前の文字
        dommyExplanation.dommyName = () =>
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(MasterData.instance.GetStringMaster((int)trump.suit + 10, true));
            sb.Append(MasterData.instance.GetStringMaster((int)trump.suit));
            sb.Append(MasterData.instance.GetStringMaster(-10, true));
            sb.Append(Extra.ErrorText("の"));
            sb.Append(Extra.ErrorText(((int)trump.number).ToString()));

            return sb.ToString();
        };

        // 説明の文字
        dommyExplanation.dommyExplanation = () =>
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Extra.ErrorText("基本スコア"));
            if ((int)trump.number > 10 || (int)trump.number == 1) sb.Append(Extra.ErrorText(Extra.GetBlueString("+11")));
            else sb.Append(Extra.ErrorText(Extra.GetBlueString("+" + ((int)trump.number).ToString())));
            sb.Append("\n");
            if (trump.deckBuff != Card.deckBuff.None)
            {
                sb.Append(MasterData.instance.GetStringMaster(6250 + (int)trump.deckBuff));
            }

            return sb.ToString();
        };
        dommyExplanation.dommyExplanation2 = () => string.Empty;
        dommyExplanation.dommyType = () => string.Empty;

        int[] buff = { 6200 + (int)trump.deckBuff, 6100 + (int)trump.cardBuff, 6000 + (int)trump.sealBuff };

        //UIの大きさを調整
        ExplanationManager.instance._uiSize = new Vector2(200, 150);
        ExplanationManager.instance._uiSizeMini = new Vector2(200, 90);

        ExplanationManager.instance.AddExplanation(_cardObjectHands[ID].gameObject, dommyExplanation, buff, new Vector2(0, -1));






    }


    /// <summary>
    /// ラウンドの再設定時の関数
    /// </summary>
    public void RoundReset()
    {

        _cardObjects.GetAction(card =>
        {
            // 角度をリセット
            card.transform.eulerAngles = _BACK_SIDE;

            // 座標をリセット
            card.transform.position = _cardDeck.position;

            // カードオブジェクトのリセットをする
            card.ResetCard();

            return card;

        });

        _cardObjectHands.Clear();


        //ラウンドの終了準備をする
        RoundObserver.Instance.StartRoundEnd();


    }

    /// <summary>
    /// カードのオブジェクトを並び変える関数
    /// 手札のオブジェクトと内容がずれていない前提
    /// </summary>
    /// <param name="nowHand"></param>
    /// <param name="nexthand"></param>
    public void ObjectSort(List<Card.Trump> nowHand, List<Card.Trump> nexthand)
    {
        List<CardObject> dommyObjectList = new List<CardObject>();

         

        for (int i = 0; i < nexthand.Count; i++)
        {
            int index = nowHand.IndexOf(nexthand[i]);


            dommyObjectList.Add(_cardObjectHands[index]);

            nowHand.RemoveAt(index);

            _cardObjectHands.RemoveAt(index);

        }

        _cardObjectHands = dommyObjectList;

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            _cardObjectHands[i].ResetMoveTime();


        }


    }

    /// <summary>
    /// プレイを開始する関数
    /// </summary>
    public void PlayStart()
    {
        List<Card.Trump> trumps = CardManager.instance.GetHand();

        // スコアの加算をしないカードの場合は返す関数
        List<int> index = CardManager.instance.GetPlayRoleIndexs();

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i].GetStatus() != CardObject.status.play) continue;

            _cardObjectHands[i].ResetMoveTime();
            _cardObjectHands[i].SetGrab(true);
            if (!index.Contains(i))
            {
                _cardObjectHands[i].StopMove();
                _cardObjectHands[i].SetGrab(false);
                continue;
            }
            else 
            {
            
                _cardObjectHands[i].SetStatus(CardObject.status.action);
                _cardObjectHands[i].GetCheckBuff(trumps[i]);
            }
        }
    }

    public int GetActionCount() { return _cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.action); }

    /// <summary>
    /// 現在プレイの途中かどうかを判断する関数
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        int count = 0;

        // アクション中の枚数
        count += _cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.action);
        // プレイ中の枚数
        count += _cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.play);
        // ディスカード中の枚数
        count += _cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.discard);

        // 上記の枚数が一枚でもあったらプレイ途中と判定


        // カウントが増えていたらプレイの途中
        return count > 0 ? true : false;
    }


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
                case CardObject.status.discard:
                    CardMoveDiscard(_cardObjectHands[i]);
                    break;
                //既に表になっているカードに変更を加える状態
                case CardObject.status.change:
                    HandCardChengeTrump(_cardObjectHands[i], i);
                    break;
                case CardObject.status.action:
                    HandCardActionTrump(_cardObjectHands[i], i);
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
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(_handPositionLeft.transform.position).z - 30);
            moveVec = Camera.main.ScreenToWorldPoint(mousePos);

            angle = _NORMALl_ANGLE;
        }


        // 移動
        cardObjectHand.transform.position = moveVec;
        // デッキから出たときだけ角度の代入
        if (cardObjectHand.GetLostStatus() == CardObject.status.deck) cardObjectHand.transform.eulerAngles = angle;


        if (cardObjectHand.IsMovable()) return;

        //その後の仕掛けの為に必要
        cardObjectHand.SetStatus(CardObject.status.hand);
        cardObjectHand.GravityStart();


        GameUtility.SetIsPushButton(true);

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

        float vec = (Vector3.Distance(_playPositionLeft.position, _playPositionRight.position) / (GetPlayCardCount() + 1)) * (counter + 1);


        // 移動目標地点を確認
        Vector3 goalPos = _playPositionLeft.position + new Vector3(vec, 0, 0);

        // 移動量と座標を合計を算出
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 移動
        cardObjectHand.transform.position = moveVec;

        if (cardObjectHand.GetMoveTimeRata() < 1) return;



        if (_cardObjectHands.GetCount(hand => hand.GetStatus() == CardObject.status.play) !=
            _cardObjectHands.GetCount(
                hand =>
                {

                    if (hand.GetStatus() != CardObject.status.play) return false;
                    if (hand.GetMoveTimeRata() < 1) return false;
                    return true;

                    //一行のラムダ式
                    //hand.GetStatus() != CardObject.status.play ? false : hand.GetMoveTimeRata() < 1 ? false : true
                })) return;


        Debug.Log("プレイスタート");
        // 到着
        PlayManager.instance.SetCardTransComp(true);

        PlayStart();
    }

    /// <summary>
    /// 選択状態のカードを全てトラッシュに送る
    /// </summary>
    private void IsSelectTrash()
    {

        // プレイを行ったカードをトラッシュに移行
        List<Card.Trump> hands = CardManager.instance.GetHand();

        hands.GetAction(hands =>
        {
            Card.Trump trump = hands;
            if (!hands.isSelect) return hands;
            hands.state = Card.State.trash;

            return hands;
        });

        bool flag = false;

        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].isSelect)
            {

                hands.RemoveAt(i);
                i--;
                flag = true;
            }


        }

        if (!flag)
        {
            int ss = 0;
        }

        CardManager.instance.SetHand(hands);

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

        if (_cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.discard) != 0) return;

        // 選択状態のカードを全てトラッシュに送る
        IsSelectTrash();



        //ラウンドの終了準備をする
        RoundObserver.Instance.StartRoundEnd();




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

        // 変更をしているカードが配列の何番かを確認
        int targetID = _chengeCardID.FindIndex(n => n == id);

        if (!_chengeCardID.Contains(id) && cardObjectHand.GetStatus() != CardObject.status.hand)
        {

            cardObjectHand.SetStatus(CardObject.status.hand);

            CardPaint(_chengeCardTrump[0], id);

            _chengeCardTrump.RemoveAt(0);

        }

        if (targetID < 0) return;

        // 確認した番号の配列を除外
        _chengeCardID.RemoveAt(targetID);


    }

    private float _time = 0;
    private int reta = 1;
    private Vector3 _lostAngle = Vector3.zero;
    /// <summary>
    /// カードのアクションを行うクラス
    /// </summary>
    /// <param name="cardObjectHand"></param>
    /// <param name="ID"></param>
    private void HandCardActionTrump(CardObject cardObjectHand, int ID)
    {

        // アクション待機の中で一番若いオブジェクトのときだけ通す
        if (ID != _cardObjectHands.FindIndex(hand => hand.GetStatus() == CardObject.status.action)) return;


        _time += Time.deltaTime * GameConfig.GetGameSpeed() * 10;

        cardObjectHand.transform.eulerAngles = Vector3.Lerp(_lostAngle, new Vector3(0, 0, 45 * reta), _time);

        if (_time < 1) return;

        _time = 0;
        if (reta == 0) reta = -1;
        if (reta == 1) reta = 0;
        _lostAngle = cardObjectHand.transform.eulerAngles;

        if (reta != -1) return;

        // アクションを追加した事にあたり変更した値を戻している
        _cardObjectHands[ID].SetStatus(_cardObjectHands[ID].GetLostStatus());
        _cardObjectHands[ID].SetGrab(false);
        _cardObjectHands[ID].StopMove();
        reta = 1;
        _time = 0;
        //仮組み　スコアの加算 後で変更する
        TrunpScore(ID);
        //アクション待機が存在している
        if (_cardObjectHands.GetCount(hand => hand.GetStatus() == CardObject.status.action) > 0) return;

        JokerUtility.JokerPlayStart();

    }

    /// <summary>
    /// 仮組み
    /// </summary>
    /// <param name="ID"></param>
    private void TrunpScore(int ID)
    {
        float score = 0;

        score = (int)CardManager.instance.GetHand()[ID].number;

        if (score <= 1 || 11 < score) score = 11;


        ScoreManager.instance.BasicPlus(score);

    }


    /// <summary>
    /// カードの情報を元にカードのマテリアルをセットする関数
    /// </summary>
    private void CardPaint(Card.Trump cardData, int id)
    {

        MeshRenderer meshRenderer = _cardObjectHands[id].transform.GetChild(0).GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        // トランプのエフェクトマテリアルをセット
        if (Card.deckBuff.None != cardData.deckBuff) materials[(int)cardMaterialType.effect] = BuffUtility.GetTrumpMaterial((int)cardData.deckBuff);
        if (Card.cardBuff.None != cardData.cardBuff) materials[(int)cardMaterialType.effect] = BuffUtility.GetCardMaterial((int)cardData.cardBuff);
        if (Card.sealBuff.None != cardData.sealBuff) materials[(int)cardMaterialType.sael] = BuffUtility.GetSealMaterial((int)cardData.sealBuff);

        // トランプのソーツとナンバーを含んだマテリアルをセット
        materials[(int)cardMaterialType.main] = _materialManager.GetMaterial((int)cardData.suit, (int)cardData.number);

        if (cardData.deckBuff == Card.deckBuff.Glass)
        {
            //グラズのマテリアルのときだけベースのマテリアルのレンダリングモードをFadeに変更する
            materials[(int)cardMaterialType.main].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            materials[(int)cardMaterialType.main].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            materials[(int)cardMaterialType.main].SetInt("_ZWrite", 1);
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHATEST_ON");
            materials[(int)cardMaterialType.main].EnableKeyword("_ALPHABLEND_ON");
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[(int)cardMaterialType.main].renderQueue = 3000;
        }
        else
        {
            materials[(int)cardMaterialType.main].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            materials[(int)cardMaterialType.main].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            materials[(int)cardMaterialType.main].SetInt("_ZWrite", 1);
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHATEST_ON");
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHABLEND_ON");
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[(int)cardMaterialType.main].renderQueue = -1;
        }


        meshRenderer.materials = materials;
    }

    /// <summary>
    /// 使用可能なカードを返す関数
    /// </summary>
    /// <returns></returns>
    private CardObject GetUseCardObject()
    {
        for (int i = _cardObjects.Count - 1; i >= 0; i--)
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
