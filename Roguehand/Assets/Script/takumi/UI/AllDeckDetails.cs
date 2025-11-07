using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllDeckDetails : DetailsBase
{
    [SerializeField] GameObject _cardPrefab;

    [SerializeField]List<UICardManager> uICards = new List<UICardManager>();

    [SerializeField] private List<TextMeshProUGUI> _cardNumberCounters = new List<TextMeshProUGUI>((int)Card.number.max);

    /// <summary>
    /// Aのカウントを入れるテキスト
    /// </summary>
    [SerializeField] private TextMeshProUGUI _aseText;
    /// <summary>
    /// Faceのカウントを入れるテキスト
    /// </summary>
    [SerializeField] private TextMeshProUGUI _faceText;
    /// <summary>
    /// Numberのカウントを入れるテキスト
    /// </summary>
    [SerializeField] private TextMeshProUGUI _numberText;

    /// <summary>
    /// スートのカウントを入れるテキスト
    /// </summary>
    [SerializeField] private List<TextMeshProUGUI> _suitText=new List<TextMeshProUGUI>((int)Card.suit.max);

    private GameObject _pool;
    private List<GameObject> _pollList=new List<GameObject>();
    private float _poolCount=52;

    public override void Show()
    {
        SetCard();

        for (int i = 0; i < uICards.Count; i++)
            uICards[i].Show();

        SetCounter();

    }
    public override void Initializ()
    {
        _pool = new GameObject("CardUIPool");
        _pool.transform.SetParent(transform);

        for(int i=0;i< _poolCount; i++) 
        {


            _pollList.Add(Instantiate(_cardPrefab, _pool.transform));
            _pollList[i].SetActive(false);


        }


        
    }

    private void SetCard() 
    {
        List<Card.Trump> deckList = CardManager.instance.GetDeck();

        for(int i = 0; i < deckList.Count; i++) 
        {
            GameObject game = GetActive();

            game.transform.SetParent(uICards[(int)deckList[i].suit].transform);

            //　背景の白を描画するためにマテリアルを貼り付けるのは子供s
            game = game.transform.GetChild(0).gameObject;

            Material material=new Material( CardObjectUtility.GetMaterial((int)deckList[i].suit, (int)deckList[i].number));

            material.shader = Shader.Find("UI/Default");

            game.GetComponent<Image>().material = material;



        }


    }

    private GameObject GetActive() 
    {
        for(int i = 0; i < _pollList.Count; i++) 
        {
            
            if (_pollList[i].activeSelf) continue;

            _pollList[i].SetActive(true);

            return _pollList[i];


        }
        return null;

    }
    private void SetCounter()
    {

        // 数字に関係する値を入れる
        _cardNumberCounters[0].text = CardManager.instance.GetDeck().GetCount(card => card.number == Card.number.ace).ToString();
        int counter = 1;
        for (int i = (int)Card.number.king; i > 1; i--)
        {
            Card.number number = (Card.number)i;
            _cardNumberCounters[counter].text = CardManager.instance.GetDeck().GetCount(card => card.number == number).ToString();
            counter++;
        }

        // Aのカウント
        _aseText.text= CardManager.instance.GetDeck().GetCount(card => card.number == Card.number.ace).ToString();
        // フェイス
        _faceText.text= CardManager.instance.GetDeck().GetCount(card => card.isFeice).ToString();
        // number
        _numberText.text= CardManager.instance.GetDeck().GetCount(card => !card.isFeice).ToString();


        // スートのカウント
        for(int i = 0; i < (int)Card.suit.max; i++) 
        {
            _suitText[i].text= CardManager.instance.GetDeck().GetCount(card => card.suit==(Card.suit)i).ToString();
        }



    }

}
