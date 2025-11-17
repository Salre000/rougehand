using System.Collections.Generic;
using UnityEngine;

public class UIErrorBuff : MonoBehaviour
{
    private enum systemBuff 
    {
        None = -1,
        Mouse,
        Brack,
        ObujectMove,
        Number
    }

    [SerializeField] systemBuff _thisBuff= systemBuff.None;


    public void SetCard() 
    {
        List<MeshRenderer> meshRenderers = new();

        int count = 0;

        // それぞれのバフの対象の数を取得
        switch (_thisBuff)
        {
            case systemBuff.Mouse:
                // ジョーカーのマウスジャマーの数を追加
                count += JokerUtility.GetJokers().GetCount(joker => joker.GetCardBuff() == Card.cardBuff.MouseJammer);
                // デッキのマウスジャマーの数を追加
                count += CardManager.instance.GetDeck().GetCount(card => card.cardBuff == Card.cardBuff.MouseJammer);
                break;
            case systemBuff.Brack:
                // デッキのブラックシールの数を追加
                count += CardManager.instance.GetDeck().GetCount(card => card.sealBuff == Card.sealBuff.Black);

                break;
            case systemBuff.ObujectMove:
                // ジョーカーのオブジェクトムーブの数を追加
                count += JokerUtility.GetJokers().GetCount(Joker => Joker.GetJokerBuff() == Card.JokerBuff.ObjectMoves);


                break;
            case systemBuff.Number:
                // デッキのブラインドスコアの数を追加
                count += CardManager.instance.GetDeck().GetCount(card => card.deckBuff == Card.deckBuff.BlindScore);
                break;
        }


        for (int i = 0; i < count; i++) 
        {
            switch (_thisBuff)
            {

                case systemBuff.Mouse:
                    break;
                case systemBuff.Brack:
                    break;
                case systemBuff.ObujectMove:
                    break;
                case systemBuff.Number:
                    break;
            }





        }




    }



}
