using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バフのマネージャー
/// </summary>
public class BuffManager : MonoBehaviour
{

    /// <summary>
    /// シール属性のバフ内容
    /// </summary>
    private SealBuff _sealBuff;
    [SerializeField] List<Material> sealMaterial = new List<Material>();

    /// <summary>
    /// カード属性のバフ内容
    /// </summary>
    private CardBuff _cardBuff;
    [SerializeField] List<Material> cardMaterial = new List<Material>();

    /// <summary>
    /// トランプ属性のバフ内容
    /// </summary>
    private TrumpBuff _trumpBuff;
    [SerializeField] List<Material> trumpMaterial = new List<Material>();


    [SerializeField] List<Material> jokerMaterial = new List<Material>();
    /// <summary>
    /// システムに干渉する可能性のあるバフのクラス
    /// </summary>
    private SystemErrorBuff _errorBuff;

    /// <summary>
    /// エラーバフのスコア上昇などのリスト
    /// </summary>
    private List<System.Func<float>> errorBuffAction = new List<System.Func<float>>();

    /// <summary>
    /// ブラックシールに使う倍率
    /// </summary>
    private readonly float BLACK_ERROR_BUFF_MAGNIFICATION = 2f;
    /// <summary>
    /// マウスジャマーに使う倍率
    /// </summary>
    private readonly float MOUSEJAMMER_ERROR_BUFF_MAGNIFICATION = 5f;
    /// <summary>
    /// ブラインドスコアに使う倍率
    /// </summary>
    private readonly float BLINDSCORE_ERROR_BUFF_MAGNIFICATION = 1f;

    /// <summary>
    /// オブジェクトムーブに使う倍率
    /// </summary>
    private readonly float OBJECTMOVES_BUFF_MAGNIFICATION = 1f;


    public void Awake()
    {
        Initializ();
    }
    public void Update()
    {
        _errorBuff?.UpData();




    }
    private void Initializ()
    {
        // クラスの生成
        _sealBuff = new SealBuff();
        _cardBuff = new CardBuff();
        _trumpBuff = new TrumpBuff();
        _errorBuff = new SystemErrorBuff();

        BuffUtility.BuffManager = this;
        _errorBuff.CreateErrorBuff();


    }

    /// <summary>
    /// システムエラーバフに分類されるスコアなどの上昇を行う関数
    /// </summary>
    public void SystemErrorBuff()
    {
        //前回のを初期化
        errorBuffAction.Clear();

        // ブラックシール
        errorBuffAction.Add(() => CardManager.instance.GetDeck().GetCount(card => card.sealBuff == Card.sealBuff.Black) * 2);

        // オブジェクトムーブ
        errorBuffAction.Add(() => JokerUtility.GetJokers().GetCount(joker => joker.GetJokerBuff() == Card.JokerBuff.ObjectMoves));
        // ブラインド
        errorBuffAction.Add(() => CardManager.instance.GetDeck().GetCount(card => card.deckBuff == Card.deckBuff.BlindScore));

        // マウスジャマー
        errorBuffAction.Add(() =>
        {
            float counter = MOUSEJAMMER_ERROR_BUFF_MAGNIFICATION;

            // 他のエラーバフの段階を加算
            counter += CardManager.instance.GetDeck().GetCount(card => card.sealBuff == Card.sealBuff.Black);
            counter += JokerUtility.GetJokers().GetCount(joker => joker.GetJokerBuff() == Card.JokerBuff.ObjectMoves);
            counter += CardManager.instance.GetDeck().GetCount(card => card.deckBuff == Card.deckBuff.BlindScore);



            return counter;
        });
    }



    /// <summary>
    /// カードをプレイした時に発動するバフ
    /// </summary>
    public void PlayBuff(Card.Trump trump)
    {
        //シールの効果を発動する
        _sealBuff.Play(trump.sealBuff);

        //カードのバフを発動
        _cardBuff.Hand(trump.cardBuff);

        //デッキカードのバフを発動
        _trumpBuff.Play(trump.deckBuff);


    }

    /// <summary>
    /// カードをプレイした時に手札で発動するバフ
    /// </summary>
    /// <param name="trump"></param>
    public void HandBuff(Card.Trump trump)
    {
        //シールの効果を発動する
        _sealBuff.Hand(trump.sealBuff);

        //カードのバフを発動
        _cardBuff.Hand(trump.cardBuff);

        //デッキカードのバフを発動
        _trumpBuff.Hand(trump.deckBuff);


    }
    /// <summary>
    /// カードをディスカードした時に発動するバフ
    /// </summary>
    /// <param name="trump"></param>
    public void DiscardBuff(Card.Trump trump)
    {
        //シールの効果を発動する
        _sealBuff.Discard(trump.sealBuff);

        //カードのバフを発動
        _cardBuff.Discard(trump.cardBuff);

        //デッキカードのバフを発動
        _trumpBuff.Discard(trump.deckBuff);


    }

    /// <summary>
    /// ラウンドの終了時に手札にあると発動するバフ
    /// </summary>
    public void RoundEndBuff(Card.Trump trump)
    {
        //シールの効果を発動する
        _sealBuff.RoundEnd(trump.sealBuff);

        //カードのバフを発動
        _cardBuff.RoundEnd(trump.cardBuff);

        //デッキカードのバフを発動
        _trumpBuff.RoundEnd(trump.deckBuff);

    }

    public void PlayBuff(Card.cardBuff cardBuff)
    {
        _cardBuff.Play(cardBuff);
    }
    public void PlayBuff(Card.JokerBuff jokerbuff)
    {

        switch (jokerbuff)
        {
            case Card.JokerBuff.Sepia:
                //int score=ScoreManager.instance.Get

                break;
        }


    }

    public Material GetCardMaterial(int ID) { return cardMaterial[ID]; }
    public Material GetTrumpMaterial(int ID) { return trumpMaterial[ID]; }
    public Material GetJokerMaterial(int ID) { return jokerMaterial[ID]; }
    public Material GetSealMaterial(int ID) { return sealMaterial[ID]; }


}
