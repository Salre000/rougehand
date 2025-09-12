using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject cardBase;

    /// <summary>
    /// �S�ẴJ�[�h�𐶐�����K�v���Ȃ���������Ȃ�
    /// </summary>
    private List<GameObject> cardObjects = new List<GameObject>((int)Card.suit.max * (int)Card.number.king);

    private Material[][] cardMaterial = new Material[(int)Card.suit.max][];


    public void Awake()
    {
        CreateCard();

    }

    /// <summary>
    /// 52����������֐�
    /// </summary>
    private void CreateCard()
    {
        for (int i = 0; i < (int)Card.suit.max * (int)Card.number.king; i++)
        {
            cardObjects.Add(GameObject.Instantiate(cardBase,transform));

        }
    }

    /// <summary>
    /// �f�b�L�����D�ւ̈ړ��֐�
    /// </summary>
    /// <param name="carDatas"><s/param>
    public void HandToCard(List<Card> carDatas) 
    {



    }

    /// <summary>
    /// �J�[�h�̏������ɃJ�[�h�̃}�e���A�����Z�b�g����֐�
    /// </summary>
    private void CardPaint(Card cardData)
    {
        




    }




}
