using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardObjectManager : MonoBehaviour
{
    /// <summary>
    /// �J�[�h�̃I�u�W�F�N�g�̃x�[�X
    /// </summary>
    [SerializeField] private GameObject _cardBase;

    /// <summary>
    /// �S�ẴJ�[�h�𐶐�����K�v���Ȃ���������Ȃ�
    /// </summary>
    private List<GameObject> _cardObjects = new List<GameObject>((int)Card.suit.max * (int)Card.number.king);

    private Material[][] _cardMaterials = new Material[(int)Card.suit.max][];



    public void Awake()
    {
        Initialize();

    }

    
    public void Initialize()
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
            _cardObjects.Add(Instantiate(_cardBase, transform));

        }
    }

    /// <summary>
    /// �f�b�L�����D�ւ̈ړ��֐�
    /// </summary>
    /// <param name="carDatas"><s/param>
    public void HandToCard(List<Card> cardDatas)
    {

    }

    /// <summary>
    /// �J�[�h�̏������ɃJ�[�h�̃}�e���A�����Z�b�g����֐�
    /// </summary>
    private void CardPaint(Card cardData)
    {





    }




}
