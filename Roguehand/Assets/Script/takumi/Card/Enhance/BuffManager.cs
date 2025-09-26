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

    /// <summary>
    /// カード属性のバフ内容
    /// </summary>
    private CardBuff _cardBuff;

    /// <summary>
    /// トランプ属性のバフ内容
    /// </summary>
    private TrumpBuff _trumpBuff;



    public void Awake()
    {
        Initializ();
    }

    private void Initializ() 
    {
        // クラスの生成
        _sealBuff = new SealBuff();
        _cardBuff = new CardBuff();
        _trumpBuff = new TrumpBuff();

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


}
