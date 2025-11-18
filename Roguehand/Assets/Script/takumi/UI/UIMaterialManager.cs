using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaterialManager : MonoBehaviour
{


    [SerializeField] private List<List<Material>> _trumpMaterial = new List<List<Material>>();
    [SerializeField] private List<Material> _deckBuffMaterial = new();
    [SerializeField] private List<Material> _cardBuffMaterial = new();
    [SerializeField] private List<Material> _sealBuffMaterial = new();

    public static UIMaterialManager instance;

    public void Start()
    {
        instance= this;
        for (int i = 0; i < (int)Card.suit.max; i++)
        {
            _trumpMaterial.Add(new List<Material>());
            for (int j = 1; j < (int)Card.number.max; j++)
            {

                Material material = new Material(CardObjectUtility.GetMaterial(i, j));

                material.shader = Shader.Find("UI/Default");

                material.color = Color.white;

                _trumpMaterial[i].Add(material);


            }

        }

        // シールのマテリアルを追加
        for (int i = 0; i < (int)Card.sealBuff.MAX; i++)
        {
            Material material = new Material(BuffUtility.GetSealMaterial(i));

            material.shader = Shader.Find("UI/Default");

            material.color = Color.white;

            _sealBuffMaterial.Add(material);

        }

        // cardBuffのマテリアルを追加
        for (int i = 0; i < (int)Card.cardBuff.MAX; i++)
        {
            Material material = new Material(BuffUtility.GetCardMaterial(i));

            material.shader = Shader.Find("UI/Default");

            material.color = Color.white;

            _cardBuffMaterial.Add(material);

        }
        // DeckBuffのマテリアルを追加
        for (int i = 0; i < (int)Card.deckBuff.MAX; i++)
        {
            Material material = new Material(BuffUtility.GetTrumpMaterial(i));

            material.shader = Shader.Find("UI/Default");

            material.color = Color.white;

            _deckBuffMaterial.Add(material);

        }




    }


    public Material GetSealBuff(Card.sealBuff sealBuff)
    {
        if (sealBuff != Card.sealBuff.None) return _sealBuffMaterial[(int)sealBuff];
        return null;


    }
    public Material GetTrump(int suit,int number)
    {

        return _trumpMaterial[suit][number];
    }
    public Material GetEffctBuff(Card.Trump buff)
    {
        if (buff.deckBuff != Card.deckBuff.None) return _deckBuffMaterial[(int)buff.deckBuff];
        if (buff.cardBuff != Card.cardBuff.None) return _cardBuffMaterial[(int)buff.cardBuff];



        return null;


    }


}
