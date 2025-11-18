using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBuffDetalis : DommyDetalis
{
    private readonly int MAX_SIZE = 25;
    [SerializeField] GameObject prefab;
    [SerializeField] List<UIErrorBuff> uIErrorBuffs = new List<UIErrorBuff>();
    private GameObject _objectPoolParent;
    private List<GameObject> _objectPool = new List<GameObject>();

    public enum systemBuff
    {
        None = -1,
        Mouse,
        Brack,
        ObujectMove,
        Number
    }

    public override void Initializ()
    {
        CreateObject();
    }
    private void CreateObject()
    {
        _objectPoolParent = new GameObject("ObjectPool");
        _objectPoolParent.transform.parent = transform;
        for (int i = 0; i < MAX_SIZE; i++)
        {

            _objectPool.Add(Instantiate(prefab, _objectPoolParent.transform));
            _objectPool[i].SetActive(false);

        }

    }
    private List<GameObject> GetActiveObject(int count,List<Card.Trump> trumps,List<JokerBase> jokers)
    {
        List<GameObject> list = new List<GameObject>();

        if(count==0)return list;

        for (int i = 0; i < MAX_SIZE; i++)
        {
            if (_objectPool[i].activeSelf) continue;

            _objectPool[i].SetActive(true);

            list.Add(_objectPool[i]);

            if (list.Count < count) continue;

            return list;
        }

        return list;
    }
    public override void Show()
    {
        for (int i = 0; i < uIErrorBuffs.Count; i++)
        {

            int count = 0;

            List<Card.Trump> trumps=new List<Card.Trump>();

            List<JokerBase> jokers=new List<JokerBase>();

            // それぞれのバフの対象の数を取得
            switch ((systemBuff)i)
            {
                case systemBuff.Mouse:
                    // ジョーカーのマウスジャマーの数を追加
                    count += JokerUtility.GetJokers().GetCount(joker => 
                    { if (joker.GetCardBuff() == Card.cardBuff.MouseJammer) { jokers.Add(joker); return true; }return false; });
                    // デッキのマウスジャマーの数を追加
                    count += CardManager.instance.GetDeck().GetCount(card =>
                    { if (card.cardBuff == Card.cardBuff.MouseJammer) { trumps.Add(card); return true; }return false; });
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

            uIErrorBuffs[i].SetCard(GetActiveObject(count, trumps, jokers));

        }


    }

    public override void Hide()
    {

        for (int i = 0; i < MAX_SIZE; i++)
        {

            _objectPool[i].transform.SetParent(_objectPoolParent.transform);
            _objectPool[i].SetActive(false);
        }
    }
}
