using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardObjectManager : MonoBehaviour
{
    /// <summary>
    /// �g�����v�̃}�e���A���ԍ��̗񋓑�
    /// </summary>
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
    /// ��D�̃J�[�h�̍��W�̈�ԍ���
    /// </summary>
    [SerializeField] private Vector3 _handPositionLeft = Vector3.zero;
    /// <summary>
    /// ��D�̃J�[�h�̍��W�̈�ԉE��
    /// </summary>
    [SerializeField] private Vector3 _handPositionRight = Vector3.zero;

    /// <summary>
    /// ��D�̃J�[�h�̍��W�̈�ԍ�������E���܂ł̋���
    /// </summary>
    private float _handPositionRange = 0;


    /// <summary>
    /// �S�ẴJ�[�h�𐶐�����K�v���Ȃ���������Ȃ�
    /// </summary>
    private List<CardObject> _cardObjects = new List<CardObject>((int)Card.suit.max * (int)Card.number.king);

   [SerializeField] private List<CardObject> _cardObjectHands = new List<CardObject>();

    private Material[][] _cardMaterials = new Material[(int)Card.suit.max][];



    public void Awake()
    {
        Initialize();

    }
    public void Update()
    {
        HandCardSetPosition();
    }


    public void Initialize()
    {
        //Utility�ɓo�^
        CardObjectUtility.CardObjectManager = this;

        CreateCard();

        //��D�̕����v�Z
        _handPositionRange = Vector3.Distance(_handPositionLeft, _handPositionRight);

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

            cardObject.SetStatus(CardObject.status.hand);

            //��D�ɒǉ�
            _cardObjectHands.Add(cardObject);

            //��D�ɒǉ����ꂽ�J�[�h�Ƀ}�e���A�����Z�b�g
            CardPaint(cardDatas[i], i);
        }
    }

    /// <summary>
    /// �n���h�̈ړ����J�n����֐�
    /// </summary>
    public void StartHandMove() 
    {
        for(int i = 0; i < _cardObjectHands.Count; i++)
            _cardObjectHands[i].ResetMoveTime();

    }

    /// <summary>
    /// �n���h�J�[�h�I�u�W�F�N�g�̍��W���ړ������Ē�ʒu�Ɉړ�������֐�
    /// </summary>
    private void HandCardSetPosition()
    {
        //�n���h�̖���
        int handCardCount = _cardObjectHands.Count;

        //�J�[�h�ƃJ�[�h�̊�
        float handCardRange = _handPositionRange / (float)(handCardCount + 1f);

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            //�ړ��\���ǂ������m�F
            if (!_cardObjectHands[i].IsMovable()) continue;
            if (_cardObjectHands[i].GetStatus() != CardObject.status.hand) continue;    

            _cardObjectHands[i].CountDown();

            //�ړ��ڕW�n�_���m�F
            Vector3 goalPos= _handPositionLeft+new Vector3(handCardRange*(i+1),0,0);

            //�ړ��ʂƍ��W�����v���Z�o
            Vector3 moveVec=Vector3.Lerp(_cardObjectHands[i].GetBeforePosition(), goalPos, _cardObjectHands[i].GetMoveTimeRata());

            //�ړ�
            _cardObjectHands[i].transform.position = moveVec;
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

        //�g�����v�̃\�[�c�ƃi���o�[���܂񂾃}�e���A�����Z�b�g
        //materials[(int)cardMaterialType.main] = _cardMaterials[(int)cardData.suit][(int)cardData.number];

        meshRenderer.materials = materials;
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


    /// <summary>
    /// 52����������֐�
    /// </summary>
    private void CreateCard()
    {
        for (int i = 0; i < (int)Card.suit.max * (int)Card.number.king; i++)
        {
            _cardObjects.Add(Instantiate(_cardBase, transform).AddComponent<CardObject>());
            _cardObjects[i].SetStatus(CardObject.status.deck);

        }
    }




}
