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

}
