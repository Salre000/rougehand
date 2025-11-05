using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllDeckDetails : DetailsBase
{
    [SerializeField] GameObject _cardPrefab;

    [SerializeField]List<UICardManager> uICards = new List<UICardManager>();

    private GameObject _pool;
    private List<GameObject> _pollList=new List<GameObject>();
    private float _poolCount=52;

    public override void Show()
    {
        SetCard();

        for (int i = 0; i < uICards.Count; i++)
            uICards[i].Show();


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

            game.GetComponent<Image>().material = CardObjectUtility.GetMaterial((int)deckList[i].suit, (int)deckList[i].number);



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

}
