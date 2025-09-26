using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealBuff 
{
    /// <summary>
    /// カードをプレイした時のバフ
    /// </summary>
    public void Play(Card.sealBuff sealBuff) 
    {
        //対応したバフの効果を記述
        switch (sealBuff)
        {
            default:
                break;
        }



    }
    /// <summary>
    /// カードをプレイした時に手札にあると発動するバフ
    /// </summary>
    /// <param name="sealBuff"></param>
    public void Hand(Card.sealBuff sealBuff) 
    {
        //対応したバフの効果を記述
        switch (sealBuff)
        {
            default:
                break;
        }



    }

    /// <summary>
    /// カードをディスカードした時のバフ
    /// </summary>
    public void Discard(Card.sealBuff sealBuff)
    {
        //対応したバフの効果を記述
        switch (sealBuff)
        {
            default:
                break;
        }



    }
    /// <summary>
    /// ラウンドの終了時に手札にあるときのバフ
    /// </summary>
    /// <param name="sealBuff"></param>
    public void RoundEnd(Card.sealBuff sealBuff)
    {
        //対応したバフの効果を記述
        switch (sealBuff)
        {
            default:
                break;
        }



    }





}