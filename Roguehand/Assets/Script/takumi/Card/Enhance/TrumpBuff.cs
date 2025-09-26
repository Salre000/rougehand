using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpBuff
{

    /// <summary>
    /// カードをプレイした時のバフ
    /// </summary>
    public void Play(Card.deckBuff deckBuff)
    {

        //対応したバフを記述
        switch (deckBuff)
        {
            case Card.deckBuff.None:
                break;
        }
    }

    /// <summary>
    /// カードをディスカードした時のバフ
    /// </summary>
    public void Discard(Card.deckBuff deckBuff)
    {

        //対応したバフを記述
        switch (deckBuff)
        {
            case Card.deckBuff.None:
                break;
        }
    }

    /// <summary>
    /// カードをプレイした時にハンドにある時のバフ
    /// </summary>
    public void Hand(Card.deckBuff deckBuff)
    {

        //対応したバフを記述
        switch (deckBuff)
        {
            case Card.deckBuff.None:
                break;
        }
    }
    /// <summary>
    /// ラウンドの終了時に手札にあるときに発動するバフ
    /// </summary>
    /// <param name="deckBuff"></param>
    public void RoundEnd(Card.deckBuff deckBuff)
    {

        //対応したバフを記述
        switch (deckBuff)
        {
            case Card.deckBuff.None:
                break;
        }
    }




}