using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardObjectManager : MonoBehaviour
{

    private enum cardMaterialType 
    {
        /// <summary>
        /// �g�����v�̗���
        /// </summary>
        back,
        /// <summary>
        /// �g�����v�̃o�t���e�Ō��܂�
        /// </summary>
        effect,
        /// <summary>
        /// �g�����v�̃X�[�g�ƃi���o�[�Ō��܂�
        /// </summary>
        main

    }


    /// <summary>
    /// �J�[�h�̃I�u�W�F�N�g�̃x�[�X
    /// </summary>
    [SerializeField] private GameObject _cardBase;

    /// <summary>
    /// �S�ẴJ�[�h�𐶐�����K�v���Ȃ���������Ȃ�
    /// </summary>
    private List<CardObject> _cardObjects = new List<CardObject>((int)Card.suit.max * (int)Card.number.king);

    private List<CardObject> _cardObjectHands = new List<CardObject>();

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
            _cardObjects.Add(Instantiate(_cardBase, transform).AddComponent<CardObject>());

        }
    }

    /// <summary>
    /// �f�b�L�����D�ւ̈ړ��֐�
    /// </summary>
    /// <param name="carDatas"><s/param>
    public void HandToCard(List<Card.Trump> cardDatas)
    {
        //cardDatas�̒��g���m�F���Ď擾
        for (int i = 0; i < cardDatas.Count; i++)
        {
            //�g�p�\�ȃJ�[�h�����m�F
            CardObject cardObject = GetUseCardObject();
            if (cardObject == null) continue;

            //��D�ɒǉ�
            _cardObjectHands.Add(cardObject);

            //��D�ɒǉ����ꂽ�J�[�h�Ƀ}�e���A�����Z�b�g
            CardPaint(cardDatas[i], i);
        }
    }

    /// <summary>
    /// �J�[�h�̏������ɃJ�[�h�̃}�e���A�����Z�b�g����֐�
    /// </summary>
    private void CardPaint(Card.Trump cardData, int id)
    {

        MeshRenderer meshRenderer = _cardObjectHands[id].GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;

        //�g�����v�̃G�t�F�N�g�}�e���A�����Z�b�g�i���܂͂Ȃ��j

        materials[(int)cardMaterialType.main] = _cardMaterials[cardData.][];





    }

    /// <summary>
    /// �g�p�\�ȃJ�[�h��Ԃ��֐�
    /// </summary>
    /// <returns></returns>
    private CardObject GetUseCardObject()
    {
        for (int i = 0; i < _cardObjects.Count; i++)
        {
            //�J�[�h��deck�ɂȂ������������x
            if (_cardObjects[i].GetStatus() != CardObject.status.deck) continue;

            return _cardObjects[i];
        }
        //�����Ԃ��镨���Ȃ�
        return null;

    }




}
