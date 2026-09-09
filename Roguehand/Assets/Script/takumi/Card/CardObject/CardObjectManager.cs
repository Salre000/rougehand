using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
public class CardObjectManager : MonoBehaviour
{
    /// <summary>
    /// 繝医Λ繝ｳ繝励・繝槭ユ繝ｪ繧｢繝ｫ逡ｪ蜿ｷ縺ｮ蛻玲嫌菴・
    /// </summary>
    public enum cardMaterialType
    {
        /// <summary>
        /// 繝医Λ繝ｳ繝励・繝舌ヵ蜀・ｮｹ縺ｧ豎ｺ縺ｾ繧・
        /// </summary>
        effect,

        /// <summary>
        /// 繝医Λ繝ｳ繝励・陬城擇
        /// </summary>
        back,
        /// <summary>
        /// 繝医Λ繝ｳ繝励・繧ｹ繝ｼ繝医→繝翫Φ繝舌・縺ｧ豎ｺ縺ｾ繧・
        /// </summary>
        main,

        buff,

        sael

    }

    /// <summary>
    /// 繧ｫ繝ｼ繝峨・繧ｪ繝悶ず繧ｧ繧ｯ繝医・繝吶・繧ｹ
    /// </summary>
    [SerializeField] private GameObject _cardBase;

    /// <summary>
    /// 謇区惆縺ｮ繧ｫ繝ｼ繝峨・蠎ｧ讓吶・荳逡ｪ蟾ｦ蛛ｴ
    /// </summary>
    [SerializeField, Header("謇区惆縺ｮ繧ｫ繝ｼ繝峨・蠎ｧ讓吶・荳逡ｪ蟾ｦ蛛ｴ")] private Transform _handPositionLeft;
    /// <summary>
    /// 謇区惆縺ｮ繧ｫ繝ｼ繝峨・蠎ｧ讓吶・荳逡ｪ蜿ｳ蛛ｴ
    /// </summary>
    [SerializeField, Header("謇区惆縺ｮ繧ｫ繝ｼ繝峨・蠎ｧ讓吶・荳逡ｪ蜿ｳ蛛ｴ")] private Transform _handPositionRight;
    /// <summary>
    /// 繝励Ξ繧､縺ｮ蠎ｧ讓吶・荳逡ｪ蟾ｦ蛛ｴ
    /// </summary>
    [SerializeField, Header("繝励Ξ繧､縺ｮ蠎ｧ讓吶・荳逡ｪ蟾ｦ蛛ｴ")] private Transform _playPositionLeft;
    /// <summary>
    /// 繝励Ξ繧､縺ｮ蠎ｧ讓吶・荳逡ｪ蜿ｳ蛛ｴ
    /// </summary>
    [SerializeField, Header("繝励Ξ繧､縺ｮ蠎ｧ讓吶・荳逡ｪ蜿ｳ蛛ｴ")] private Transform _playPositionRight;

    /// <summary>
    /// 繝ｩ繧ｦ繝ｳ繝我ｸｭ菴ｿ繧上ｌ縺ｪ縺・ｴ譽・＆繧後ｋ繧ｫ繝ｼ繝峨・蠎ｧ讓・
    /// </summary>
    [SerializeField, Header("繝ｩ繧ｦ繝ｳ繝我ｸｭ菴ｿ繧上ｌ縺ｪ縺・ｴ譽・＆繧後ｋ繧ｫ繝ｼ繝峨・蠎ｧ讓・)] private Transform _handTrash;
    /// <summary>
    /// 繧ｫ繝ｼ繝峨が繝悶ず繧ｧ繧ｯ繝医ｒ鄂ｮ縺・※縺・￥繝・ャ繧ｭ縺ｮ蝓ｺ貅門ｺｧ讓・
    /// </summary>
    [SerializeField, Header("繝・ャ繧ｭ縺ｮ蝓ｺ貅門ｺｧ讓・)] private Transform _cardDeck;

    /// <summary>
    /// 繝医Λ繝・す繝･縺ｫ遘ｻ蜍穂ｸｭ縺ｮ繧ｫ繝ｼ繝峨・隗貞ｺｦ縺ｮ螳壽焚
    /// </summary>
    private readonly Vector3 _TRASH_ANGLE = new Vector3(-90, 90, 90);

    /// <summary>
    /// 繧ｫ繝ｼ繝峨・蝓ｺ譛ｬ迥ｶ諷九・隗貞ｺｦ
    /// </summary>
    private readonly Vector3 _NORMALl_ANGLE = new Vector3(0, -3, 0);

    /// <summary>
    /// 繧ｫ繝ｼ繝峨・陬城擇迥ｶ諷九・隗貞ｺｦ
    /// </summary>
    private readonly Vector3 _BACK_SIDE = new Vector3(0, 183, 0);

    /// <summary>
    /// 繝励Ξ繧､蠕・ｩ溽憾諷九・縺ｨ縺阪↓遘ｻ蜍輔☆繧狗嶌蟇ｾ遘ｻ蜍暮㍼
    /// </summary>
    private readonly Vector3 _PLAY_WAIT = new Vector3(0, 50, 0);

    /// <summary>
    /// 謇区惆縺ｮ繧ｫ繝ｼ繝峨・蠎ｧ讓吶・荳逡ｪ蟾ｦ蛛ｴ縺九ｉ蜿ｳ蛛ｴ縺ｾ縺ｧ縺ｮ霍晞屬
    /// </summary>
    private float _handPositionRange = 0;

    private readonly float _ANGLE_CHANGE_SPEED = 2 * GameConfig.GetGameSpeed();

    /// <summary>
    /// 蜈ｨ縺ｦ縺ｮ繧ｫ繝ｼ繝峨ｒ逕滓・縺吶ｋ蠢・ｦ√′縺ｪ縺・°繧ゅ＠繧後↑縺・
    /// </summary>
    private List<CardObject> _cardObjects = new List<CardObject>((int)Card.suit.max * (int)Card.number.king);

    /// <summary>
    /// 縺昴・譎ゅ・謇区惆縺ｮ繧ｫ繝ｼ繝・
    /// </summary>
    [SerializeField] private List<CardObject> _cardObjectHands = new List<CardObject>();

    /// <summary>
    /// 繧ｫ繝ｼ繝峨・蜀・ｮｹ繧貞､牙喧縺吶ｋ譎ゅ↓菴ｿ逕ｨ縺吶ｋID縺ｮ蜈･縺｣縺溘Μ繧ｹ繝・
    /// </summary>
    [SerializeField] private List<int> _chengeCardID = new List<int>();
    /// <summary>
    /// 繧ｫ繝ｼ繝峨・蜀・ｮｹ繧貞､牙喧縺吶ｋ譎ゅ↓菴ｿ逕ｨ縺吶ｋ蜀・ｮｹ縺ｮ蜈･縺｣縺溘Μ繧ｹ繝・
    /// </summary>
    private List<Card.Trump> _chengeCardTrump = new List<Card.Trump>();

    /// <summary>
    /// 繝医Λ繝ｳ繝励・繝槭ユ繝ｪ繧｢繝ｫ繧偵∪縺ｨ繧√◆繧ｯ繝ｩ繧ｹ
    /// </summary>
    private TrumpMaterialManager _materialManager;

    /// <summary>
    /// 繝励Ξ繧､縺励◆繧ｫ繝ｼ繝峨・譫壽焚繧定ｨ俶・縺吶ｋ螟画焚
    /// </summary>
    private int _playCardCount = 0;

    /// <summary>
    /// 繝・ぅ繧ｹ繧ｫ繝ｼ繝峨＠縺溘き繝ｼ繝峨・譫壽焚繧定ｨ俶・縺吶ｋ螟画焚
    /// </summary>
    private int _discardCardCount = 0;

    /// <summary>
    /// 繧ｫ繝ｼ繝峨が繝悶ず繧ｧ繧ｯ繝医ｒ郤上ａ繧九・繝ｼ繝ｫ
    /// </summary>
    private GameObject _cardPool;

    private bool _isGrab = false;
    private int _isGrabID = -1;

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
        // 繝医Λ繝ｳ繝励・繝槭ユ繝ｪ繧｢繝ｫ繧偵∪縺ｨ繧√◆繧ｯ繝ｩ繧ｹ繧貞叙蠕・
        _materialManager = GetComponent<TrumpMaterialManager>();

        // Utility縺ｫ逋ｻ骭ｲ
        CardObjectUtility.CardObjectManager = this;

        _materialManager = GetComponent<TrumpMaterialManager>();
        _materialManager.Initializ();

        CreateCard();

        // 謇区惆縺ｮ蟷・ｒ險育ｮ・
        _handPositionRange = Vector3.Distance(_handPositionLeft.position, _handPositionRight.position);

    }

    public Material GetTrunpMatarial(int suit, int number) { return _materialManager.GetMaterial(suit, number); }

    /// <summary>
    /// 繝・ャ繧ｭ縺九ｉ謇区惆縺ｸ縺ｮ遘ｻ蜍暮未謨ｰ
    /// </summary>
    /// <param name="carDatas"><s/param>
    public void HandToCard(List<Card.Trump> cardDatas)
    {
        //驕ｸ謚樔ｸｭ繧偵Μ繧ｻ繝・ヨ
        CardManager.instance.ResetPick();

        //cardDatas縺ｮ荳ｭ霄ｫ繧堤｢ｺ隱阪＠縺ｦ蜿門ｾ・
        for (int i = 0; i < cardDatas.Count; i++)
        {
            // 菴ｿ逕ｨ蜿ｯ閭ｽ縺ｪ繧ｫ繝ｼ繝峨°繧堤｢ｺ隱・
            CardObject cardObject = GetUseCardObject();
            if (cardObject == null) continue;

            cardObject.SetStatus(CardObject.status.hand);

            // 謇区惆縺ｫ霑ｽ蜉
            _cardObjectHands.Add(cardObject);

            Debug.Log(cardDatas[i].suit.ToString() + i);

            // 謇区惆縺ｫ霑ｽ蜉縺輔ｌ縺溘き繝ｼ繝峨↓繝槭ユ繝ｪ繧｢繝ｫ繧偵そ繝・ヨ
            CardPaint(cardDatas[i], _cardObjectHands.Count - 1);

            int index = CardManager.instance.GetHand().IndexOf(cardDatas[i]);

            for (int j = _cardObjectHands.Count - 1; j > index; j--)
            {
                _cardObjectHands[j] = _cardObjectHands[j - 1];

            }

            _cardObjectHands[index] = cardObject;

        }

    }

    /// <summary>
    /// 繝上Φ繝峨・遘ｻ蜍輔ｒ髢句ｧ九☆繧矩未謨ｰ
    /// </summary>
    public void StartHandMove()
    {
        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i] == null) continue;

            _cardObjectHands[i].ResetMoveTime();
        }

    }

    /// <summary>
    /// 繝励Ξ繧､貅門ｙ迥ｶ諷九→謇区惆縺ｫ縺ゅｋ迥ｶ諷九ｒ蛻・ｊ譖ｿ縺医ｋ髢｢謨ｰ
    /// </summary>
    /// <param name="id"></param>
    public void ChengeStandby(int id, bool isSelect)
    {
        // 蜍穂ｽ懊・騾比ｸｭ縺ｧ縺ｮ蜑ｲ繧願ｾｼ縺ｿ繧貞宛髯・
        if (_cardObjectHands[id].IsMovable()) return;

        _cardObjectHands[id].SetStatus(isSelect ? CardObject.status.playWait : CardObject.status.hand);
        _cardObjectHands[id].ResetMoveTime();
    }

    public bool CheckGrab(int ID)
    {
        bool flag = true;

        if (_cardObjectHands[ID].GetStatus() == CardObject.status.play) flag = false;

        return flag;
    }

    /// <summary>
    /// 譌｢縺ｫ陦ｨ縺ｫ縺ｪ縺｣縺ｦ縺・ｋ繧ｫ繝ｼ繝峨↓螟画峩繧貞刈縺医ｋ髢｢謨ｰ
    /// </summary>
    /// <param name="id"></param>
    /// <param name="trump"></param>
    public void SetChengeCard(int id, Card.Trump trump)
    {
        // 蜍穂ｽ懊・騾比ｸｭ縺ｧ縺ｮ蜑ｲ繧願ｾｼ縺ｿ繧貞宛髯・
        if (_cardObjectHands[id].IsMovable()) return;

        if (_chengeCardID.Contains(id)) return;
        // 螟画鋤繧偵＆縺帙ｋ蜀・ｮｹ繧定ｨ倬鹸
        _chengeCardID.Add(id);
        _chengeCardTrump.Add(trump);

        _cardObjectHands[id].SetStatus(CardObject.status.change);
        _cardObjectHands[id].ResetMoveTime();
    }

    /// <summary>
    /// 繝励Ξ繧､貅門ｙ迥ｶ諷九°繧峨・繝ｬ繧､縺ｫ遘ｻ陦後☆繧矩未謨ｰ
    /// </summary>
    /// <returns></returns>
    public void Play()
    {
        _playCardCount++;

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i] == null) continue;

            if (_cardObjectHands[i].GetStatus() != CardObject.status.playWait) continue;
            _cardObjectHands[i].SetStatus(CardObject.status.play);
            _cardObjectHands[i].ResetMoveTime();
        }
    }

    public void PlayEnd()
    {
        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i] == null) continue;

            if (_cardObjectHands[i].GetStatus() != CardObject.status.play) continue;
            _cardObjectHands[i].SetStatus(CardObject.status.discard);
            _cardObjectHands[i].ResetMoveTime();
        }

    }

    /// <summary>
    /// 繝励Ξ繧､貅門ｙ迥ｶ諷九°繧臥ｴ譽・憾諷九↓遘ｻ陦後☆繧矩未謨ｰ
    /// </summary>
    public void Discard()
    {
        _discardCardCount++;

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i] == null) continue;

            if (_cardObjectHands[i].GetStatus() != CardObject.status.playWait) continue;
            _cardObjectHands[i].SetStatus(CardObject.status.discard);
            _cardObjectHands[i].ResetMoveTime();

        }

    }

    /// <summary>
    /// 繝励Ξ繧､縺檎ｵゅｏ縺｣縺ｦ謇区惆縺ｨ繝励Ξ繧､繧ｫ繝ｼ繝峨ｒ遐ｴ譽・憾諷九↓縺吶ｋ髢｢謨ｰ
    /// </summary>
    public void End()
    {
        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i] == null) continue;
            _cardObjectHands[i].SetStatus(CardObject.status.discard);
            _cardObjectHands[i].ResetMoveTime();
        }

    }

    /// <summary>
    /// 謗ｴ縺ｿ縺ｮ蜃ｦ逅・↓菴ｿ逕ｨ縺吶ｋ逡ｪ蜿ｷ繧定ｿ斐☆髢｢謨ｰ
    /// </summary>
    /// <param name="cardObject"></param>
    /// <returns></returns>
    public int GetGrabCardIndex(CardObject cardObject)
    {
        int index = _cardObjectHands.FindIndex(card => card == cardObject);

        if (index < 0) return -1;

        // 縺､縺九・縺薙→縺ｮ蜃ｺ譚･繧句ｯｾ雎｡縺九←縺・°繧貞愛譁ｭ
        bool returnFlag = true;

        //蜍輔＞縺ｦ縺・ｋ譎ゅ↓荳肴ｭ｣蛟､
        if (cardObject.IsMovable()) returnFlag = false;

        // 蜍輔＞縺ｦ縺・※繧よ黒縺ｾ繧後※縺・◆繧画ｭ｣蟶ｸ蛟､
        if (cardObject.IsGrab()) returnFlag = true;

        // 繝励Ξ繧､荳ｭ縺縺ｨ蝠冗ｭ皮┌逕ｨ縺ｧ荳肴ｭ｣蛟､
        if (cardObject.GetStatus() == CardObject.status.play) returnFlag = false;

        return returnFlag ? index : -1;
    }

    public void GrabChenge(int ID, bool flag)
    {

        _cardObjectHands[ID].SetGrab(flag);
        _isGrab = flag;
        _isGrabID = ID;

        if (flag) _cardObjectHands[ID].SetStatus(CardObject.status.hand);

        else if (_cardObjectHands[ID].GetLostStatus() == CardObject.status.playWait) _cardObjectHands[ID].SetStatus(CardObject.status.playWait);

        _cardObjectHands[ID].ResetMoveTime();

    }

    public void ChengeOrder(int lostID, int nextID)
    {
        _cardObjectHands = Extra.ChengeOrder(_cardObjectHands, lostID, nextID);

        //繧ｽ繝ｼ繝医・蠖ｱ髻ｿ縺ｧ遘ｻ蜍輔☆繧九が繝悶ず繧ｧ繧ｯ繝医ｒ遘ｻ蜍輔＆縺帙ｋ

        for (int i = 0; i < _cardObjectHands.Count; i++) _cardObjectHands[i].ResetMoveTime();

        CardManager.instance.SetHand(Extra.ChengeOrder(CardManager.instance.GetHand(), lostID, nextID));

    }

    /// <summary>
    /// 繧ｫ繝ｼ繝峨・遘ｻ蜍墓凾髢薙ｒ繧ｼ繝ｭ縺ｫ縺吶ｋ
    /// </summary>
    /// <param name="ID"></param>
    public void StopMoveCardObject(int ID) { _cardObjectHands[ID].StopMove(); }

    /// <summary>
    /// 繧ｫ繝ｼ繝峨・諠・ｱ繧呈緒逕ｻ迥ｶ諷九↓螟画峩縺吶ｋ
    /// </summary>
    /// <param name="trump"></param>
    /// <param name="ID"></param>
    public void ShowExplanation(Card.Trump trump, int ID)
    {
        //隱ｬ譏弱ｒ謠冗判縺輔○繧九ム繝溘・縺ｮ繧ｯ繝ｩ繧ｹ
        DommyExplanation dommyExplanation = new DommyExplanation();

        //蜷榊燕縺ｮ譁・ｭ・
        dommyExplanation.dommyName = () =>
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(MasterData.instance.GetStringMaster((int)trump.suit + 10, true));
            sb.Append(MasterData.instance.GetStringMaster((int)trump.suit));
            sb.Append(MasterData.instance.GetStringMaster(-10, true));
            sb.Append(Extra.ErrorText("縺ｮ"));
            sb.Append(Extra.ErrorText(((int)trump.number).ToString()));

            return sb.ToString();
        };

        // 隱ｬ譏弱・譁・ｭ・
        dommyExplanation.dommyExplanation = () =>
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Extra.ErrorText("蝓ｺ譛ｬ繧ｹ繧ｳ繧｢"));
            if ((int)trump.number > 10 || (int)trump.number == 1) sb.Append(Extra.ErrorText(Extra.GetBlueString("+11")));
            else sb.Append(Extra.ErrorText(Extra.GetBlueString("+" + ((int)trump.number).ToString())));
            sb.Append("\n");
            if (trump.deckBuff != Card.deckBuff.None)
            {
                sb.Append(MasterData.instance.GetStringMaster(6250 + (int)trump.deckBuff));
            }

            return sb.ToString();
        };
        dommyExplanation.dommyExplanation2 = () => string.Empty;
        dommyExplanation.dommyType = () => string.Empty;

        int[] buff = { 6200 + (int)trump.deckBuff, 6100 + (int)trump.cardBuff, 6000 + (int)trump.sealBuff };

        //UI縺ｮ螟ｧ縺阪＆繧定ｪｿ謨ｴ
        ExplanationManager.instance._uiSize = new Vector2(200, 150);
        ExplanationManager.instance._uiSizeMini = new Vector2(200, 90);

        ExplanationManager.instance.AddExplanation(_cardObjectHands[ID].gameObject, dommyExplanation, buff, new Vector2(0, -1));
    }

    /// <summary>
    /// 繝・ャ繧ｭ縺ｮ繧ｫ繝ｼ繝峨ｒ蠅励ｄ縺咎未謨ｰ
    /// </summary>
    /// <param name="trump"></param>
    public void AddTrump(Card.Trump trump)
    {
        List<Card.Trump> deck = CardManager.instance.GetDeck();
        deck.Add(trump);
        CardManager.instance.SetDeck(deck);

        _cardObjects.Add(Instantiate(_cardBase, _cardDeck.position, Quaternion.identity).AddComponent<CardObject>());
        _cardObjects[_cardObjects.Count - 1].SetStatus(CardObject.status.deck);
        _cardObjects[_cardObjects.Count - 1].transform.eulerAngles = _BACK_SIDE;
        _cardObjects[_cardObjects.Count - 1].transform.parent = _cardPool.transform;

    }
    /// <summary>
    /// 繧ｫ繝ｼ繝峨ｒ貂帙ｉ縺咎未謨ｰ
    /// </summary>
    /// <param name="trump"></param>
    public void RemoveTrump(Card.Trump trump)
    {
        CardManager.instance.GetDeck().IndexOf(trump);

        int index = CardManager.instance.GetHand().IndexOf(trump);

        CardManager.instance.hand.Remove(trump);
        CardManager.instance.pick.Remove(trump);
        CardManager.instance.deck.Remove(trump);
        CardObject dommy = _cardObjectHands[index];
        _cardObjects.Remove(dommy);
        _cardObjectHands.Remove(dommy);
        ExplanationManager.instance.Remove();
        BreakUtility.StartBreak(dommy.gameObject);
        Destroy(dommy.gameObject);

    }

    public void ShowExplanation(Card.Trump trump, GameObject _object, Vector2 offset)
    {
        //隱ｬ譏弱ｒ謠冗判縺輔○繧九ム繝溘・縺ｮ繧ｯ繝ｩ繧ｹ
        DommyExplanation dommyExplanation = new DommyExplanation();

        //蜷榊燕縺ｮ譁・ｭ・
        dommyExplanation.dommyName = () =>
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(MasterData.instance.GetStringMaster((int)trump.suit + 10, true));
            sb.Append(MasterData.instance.GetStringMaster((int)trump.suit));
            sb.Append(MasterData.instance.GetStringMaster(-10, true));
            sb.Append(Extra.ErrorText("縺ｮ"));
            sb.Append(Extra.ErrorText(((int)trump.number).ToString()));

            return sb.ToString();
        };

        // 隱ｬ譏弱・譁・ｭ・
        dommyExplanation.dommyExplanation = () =>
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Extra.ErrorText("蝓ｺ譛ｬ繧ｹ繧ｳ繧｢"));
            if ((int)trump.number > 10 || (int)trump.number == 1) sb.Append(Extra.ErrorText(Extra.GetBlueString("+11")));
            else sb.Append(Extra.ErrorText(Extra.GetBlueString("+" + ((int)trump.number).ToString())));
            sb.Append("\n");
            if (trump.deckBuff != Card.deckBuff.None)
            {
                sb.Append(MasterData.instance.GetStringMaster(6250 + (int)trump.deckBuff));
            }

            return sb.ToString();
        };
        dommyExplanation.dommyExplanation2 = () => string.Empty;
        dommyExplanation.dommyType = () => string.Empty;

        int[] buff = { 6200 + (int)trump.deckBuff, 6100 + (int)trump.cardBuff, 6000 + (int)trump.sealBuff };

        //UI縺ｮ螟ｧ縺阪＆繧定ｪｿ謨ｴ
        ExplanationManager.instance._uiSize = new Vector2(200, 150);
        ExplanationManager.instance._uiSizeMini = new Vector2(200, 90);

        ExplanationManager.instance.AddExplanation(_object, dommyExplanation, buff, offset);
    }

    /// <summary>
    /// 繝ｩ繧ｦ繝ｳ繝峨・蜀崎ｨｭ螳壽凾縺ｮ髢｢謨ｰ
    /// </summary>
    public void RoundReset()
    {

        _cardObjects.GetAction(card =>
        {
            // 隗貞ｺｦ繧偵Μ繧ｻ繝・ヨ
            card.transform.eulerAngles = _BACK_SIDE;

            // 蠎ｧ讓吶ｒ繝ｪ繧ｻ繝・ヨ
            card.transform.position = _cardDeck.position;

            // 繧ｫ繝ｼ繝峨が繝悶ず繧ｧ繧ｯ繝医・繝ｪ繧ｻ繝・ヨ繧偵☆繧・
            card.ResetCard();

            return card;

        });

        _cardObjectHands.Clear();

    }

    /// <summary>
    /// 蝠城｡後≠繧翫菫ｮ豁｣縺励↑縺・→縺・￠縺ｪ縺・
    /// 繧ｫ繝ｼ繝峨・繧ｪ繝悶ず繧ｧ繧ｯ繝医ｒ荳ｦ縺ｳ螟峨∴繧矩未謨ｰ
    /// 謇区惆縺ｮ繧ｪ繝悶ず繧ｧ繧ｯ繝医→蜀・ｮｹ縺後★繧後※縺・↑縺・燕謠・
    /// </summary>
    /// <param name="nowHand"></param>
    /// <param name="nexthand"></param>
    public void ObjectSort(List<Card.Trump> nowHand, List<Card.Trump> nexthand)
    {

        List<CardObject> dommyObjectList = new List<CardObject>();

        for (int i = 0; i < nexthand.Count; i++)
        {
            int index = nowHand.IndexOf(nexthand[i]);

            dommyObjectList.Add(_cardObjectHands[index]);

            nowHand.RemoveAt(index);

            _cardObjectHands.RemoveAt(index);

        }

        _cardObjectHands = dommyObjectList;

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            _cardObjectHands[i].ResetMoveTime();

        }

    }

    /// <summary>
    /// 繝励Ξ繧､繧帝幕蟋九☆繧矩未謨ｰ
    /// </summary>
    public void PlayStart()
    {
        List<Card.Trump> trumps = CardManager.instance.GetHand();

        // 繧ｹ繧ｳ繧｢縺ｮ蜉邂励ｒ縺励↑縺・き繝ｼ繝峨・蝣ｴ蜷医・霑斐☆髢｢謨ｰ
        List<int> index = CardManager.instance.GetPlayRoleIndexs();

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i].GetStatus() != CardObject.status.play) continue;

            _cardObjectHands[i].ResetMoveTime();
            _cardObjectHands[i].SetGrab(true);
            if (!index.Contains(i))
            {
                _cardObjectHands[i].StopMove();
                _cardObjectHands[i].SetGrab(false);
                continue;
            }
            else
            {

                _cardObjectHands[i].SetStatus(CardObject.status.action);
                _cardObjectHands[i].GetCheckBuff(trumps[i], TrunpScore, i);
            }
        }

        for(int i=0;i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i].GetStatus() != CardObject.status.hand) continue;

            int cash = i;
            if (BuffUtility.CheckHandBuffs(CardManager.instance.GetHand()[cash])) continue;
            _cardObjectHands[i].AddAction(() =>
            {
                BuffUtility.HandBuff(CardManager.instance.GetHand()[cash]);

            });

        }

    }

    public int GetActionCount() { return _cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.action); }

    /// <summary>
    /// 迴ｾ蝨ｨ繝励Ξ繧､縺ｮ騾比ｸｭ縺九←縺・°繧貞愛譁ｭ縺吶ｋ髢｢謨ｰ
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        int count = 0;

        // 繧｢繧ｯ繧ｷ繝ｧ繝ｳ荳ｭ縺ｮ譫壽焚
        count += _cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.action);
        // 繝励Ξ繧､荳ｭ縺ｮ譫壽焚
        count += _cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.play);
        // 繝・ぅ繧ｹ繧ｫ繝ｼ繝我ｸｭ縺ｮ譫壽焚
        count += _cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.discard);

        // 荳願ｨ倥・譫壽焚縺御ｸ譫壹〒繧ゅ≠縺｣縺溘ｉ繝励Ξ繧､騾比ｸｭ縺ｨ蛻､螳・

        // 繧ｫ繧ｦ繝ｳ繝医′蠅励∴縺ｦ縺・◆繧峨・繝ｬ繧､縺ｮ騾比ｸｭ
        return count > 0 ? true : false;
    }

    public List<CardObject> CardObjects() { return _cardObjects; }

    public List<CardObject> CardHands() { return _cardObjectHands; }

    public void SetPlayCardCount(int value) { _playCardCount = value; }
    public int GetPlayCardCountMemry() { return _playCardCount; }
    public void SetDiscardCardCount(int value) { _discardCardCount = value; }
    public int GetDiscardCardCount() { return _discardCardCount; }

    public bool checkCardMove()
    {
        bool flag = false;
        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i].IsMovable()) flag = true;

        }

        return flag;
    }

    /// <summary>
    /// 縺､縺ｾ繧薙〒縺・ｋ繧ｫ繝ｼ繝峨・遘ｻ蜍輔ｒ縺吶ｋ髢｢謨ｰ
    /// </summary>
    private void MovingCard()
    {
        if (!_isGrab) return;

        //繧ｸ繝ｧ繝ｼ繧ｫ繝ｼ蜷悟｣ｫ縺ｮ霍晞屬
        float renge = Vector3.Distance(_handPositionLeft.transform.position, _handPositionRight.transform.position) / (_cardObjectHands.Count + 1);

        float Cardrenge = (_handPositionLeft.transform.position.x + renge * (_isGrabID + 1)) - _cardObjectHands[_isGrabID].transform.position.x;

        //讓ｪ譁ｹ蜷代∈縺ｮ遘ｻ蜍戊ｷ晞屬縺悟ｰ上＆縺九▲縺溘ｉ鬆・分縺ｮ螟画峩繧貞刈縺医↑縺・
        if (Mathf.Abs(Cardrenge) + 30 < renge) return;

        //遘ｻ蜍墓婿蜷代ｒ隱ｿ謨ｴ
        int count = 1;
        if (Cardrenge > 1) count = -1;

        if (_isGrabID + count >= _cardObjectHands.Count || _isGrabID + count < 0) return;

        //繧ｸ繝ｧ繝ｼ繧ｫ繝ｼ縺ｮ鬆・分繧貞・繧梧崛縺医ｋ髢｢謨ｰ繧貞他縺ｶ
        CardObjectUtility.ChengeOrder(_isGrabID, _isGrabID + count);

        _isGrabID = _isGrabID + count;

    }

    /// <summary>
    /// 繝上Φ繝峨き繝ｼ繝峨が繝悶ず繧ｧ繧ｯ繝医・蠎ｧ讓吶ｒ遘ｻ蜍輔＆縺帙※螳壻ｽ咲ｽｮ縺ｫ遘ｻ蜍輔＆縺帙ｋ髢｢謨ｰ
    /// </summary>
    private void HandCardSetPosition()
    {
        //繝上Φ繝峨・譫壽焚
        int handCardCount = _cardObjectHands.Count;

        //繧ｫ繝ｼ繝峨→繧ｫ繝ｼ繝峨・髢・
        float handCardRange = _handPositionRange / (float)(handCardCount + 1f);

        //繝励Ξ繧､貅門ｙ縺ｮ繧ｫ繧ｦ繝ｳ繧ｿ繝ｼ
        int playCounter = 0;

        MovingCard();

        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            //遘ｻ蜍募庄閭ｽ縺九←縺・°繧堤｢ｺ隱・
            if (!_cardObjectHands[i].IsMovable()) continue;
            _cardObjectHands[i].CountDown();

            //繧ｫ繝ｼ繝峨・迥ｶ諷九＃縺ｨ縺ｮ遘ｻ蜍募・逅・
            switch (_cardObjectHands[i].GetStatus())
            {
                case CardObject.status.none:
                    break;
                case CardObject.status.deck:
                    break;

                //繧ｫ繝ｼ繝峨′謇区惆縺ｸ縺ｮ遘ｻ蜍輔・譎ゅ・蜃ｦ逅・
                case CardObject.status.hand:
                    CardMoveHand(_cardObjectHands[i], handCardRange * (i + 1));
                    break;
                case CardObject.status.playWait:
                    CardMovePlayWait(_cardObjectHands[i], handCardRange * (i + 1));
                    break;
                case CardObject.status.play:
                    CardMovePlay(_cardObjectHands[i], _handPositionRange, playCounter);
                    playCounter++;
                    break;
                case CardObject.status.discard:
                    CardMoveDiscard(_cardObjectHands[i]);
                    break;
                //譌｢縺ｫ陦ｨ縺ｫ縺ｪ縺｣縺ｦ縺・ｋ繧ｫ繝ｼ繝峨↓螟画峩繧貞刈縺医ｋ迥ｶ諷・
                case CardObject.status.change:
                    HandCardChengeTrump(_cardObjectHands[i], i);
                    break;
                case CardObject.status.action:
                    HandCardActionTrump(_cardObjectHands[i], i);
                    break;
            }

        }

    }

    /// <summary>
    /// 繝・ャ繧ｭ縺九ｉ謇区惆縺ｸ縺ｮ遘ｻ蜍・
    /// </summary>
    /// <param name="cardObjectHand"></param>
    /// <param name="handCardRange"></param>
    private void CardMoveHand(CardObject cardObjectHand, float handCardRange)
    {

        // 遘ｻ蜍慕岼讓吝慍轤ｹ繧堤｢ｺ隱・
        Vector3 goalPos = _handPositionLeft.position + new Vector3(handCardRange, 0, 0);

        // 遘ｻ蜍暮㍼縺ｨ蠎ｧ讓吶ｒ蜷郁ｨ医ｒ邂怜・
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());
        // 隗貞ｺｦ縺ｮ邂怜・
        Vector3 angle = Vector3.Lerp(cardObjectHand.GetBeforeAngle(), _NORMALl_ANGLE, cardObjectHand.GetMoveTimeRata());

        if (cardObjectHand.IsGrab())
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(_handPositionLeft.transform.position).z - 30);
            moveVec = Camera.main.ScreenToWorldPoint(mousePos);

            angle = _NORMALl_ANGLE;
        }

        // 遘ｻ蜍・
        cardObjectHand.transform.position = moveVec;
        // 繝・ャ繧ｭ縺九ｉ蜃ｺ縺溘→縺阪□縺題ｧ貞ｺｦ縺ｮ莉｣蜈･
        if (cardObjectHand.GetLostStatus() == CardObject.status.deck) cardObjectHand.transform.eulerAngles = angle;

        if (cardObjectHand.IsMovable()) return;

        //縺昴・蠕後・莉墓寺縺代・轤ｺ縺ｫ蠢・ｦ・
        cardObjectHand.SetStatus(CardObject.status.hand);
        cardObjectHand.GravityStart();

        GameUtility.SetIsPushButton(true);

    }
    /// <summary>
    /// 謇区惆縺九ｉ繝励Ξ繧､貅門ｙ迥ｶ諷九∈縺ｮ遘ｻ蜍・
    /// </summary>
    /// <param name="cardObjectHand"></param>
    /// <param name="handCardRange"></param>
    private void CardMovePlayWait(CardObject cardObjectHand, float handCardRange)
    {
        // 遘ｻ蜍慕岼讓吝慍轤ｹ繧堤｢ｺ隱・
        Vector3 goalPos = _handPositionLeft.position + new Vector3(handCardRange, 0, 0) + _PLAY_WAIT;

        // 遘ｻ蜍暮㍼縺ｨ蠎ｧ讓吶ｒ蜷郁ｨ医ｒ邂怜・
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 遘ｻ蜍・
        cardObjectHand.transform.position = moveVec;

    }
    /// <summary>
    /// 謇区惆縺九ｉ繝励Ξ繧､迥ｶ諷九∈縺ｮ遘ｻ蜍・
    /// </summary>
    /// <param name="cardObjectHand"></param>
    /// <param name="handCardRange"></param>
    private void CardMovePlay(CardObject cardObjectHand, float handRange, int counter)
    {

        float vec = (Vector3.Distance(_playPositionLeft.position, _playPositionRight.position) / (GetPlayCardCount() + 1)) * (counter + 1);

        // 遘ｻ蜍慕岼讓吝慍轤ｹ繧堤｢ｺ隱・
        Vector3 goalPos = _playPositionLeft.position + new Vector3(vec, 0, 0);

        // 遘ｻ蜍暮㍼縺ｨ蠎ｧ讓吶ｒ蜷郁ｨ医ｒ邂怜・
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 遘ｻ蜍・
        cardObjectHand.transform.position = moveVec;

        if (cardObjectHand.GetMoveTimeRata() < 1) return;

        if (_cardObjectHands.GetCount(hand => hand.GetStatus() == CardObject.status.play) !=
            _cardObjectHands.GetCount(
                hand =>
                {

                    if (hand.GetStatus() != CardObject.status.play) return false;
                    if (hand.GetMoveTimeRata() < 1) return false;
                    return true;

                    //荳陦後・繝ｩ繝繝蠑・
                    //hand.GetStatus() != CardObject.status.play ? false : hand.GetMoveTimeRata() < 1 ? false : true
                })) return;

        // 蛻ｰ逹
        PlayManager.instance.SetCardTransComp(true);

        PlayStart();
    }

    /// <summary>
    /// 驕ｸ謚樒憾諷九・繧ｫ繝ｼ繝峨ｒ蜈ｨ縺ｦ繝医Λ繝・す繝･縺ｫ騾√ｋ
    /// </summary>
    private void IsSelectTrash()
    {

        // 繝励Ξ繧､繧定｡後▲縺溘き繝ｼ繝峨ｒ繝医Λ繝・す繝･縺ｫ遘ｻ陦・
        List<Card.Trump> hands = CardManager.instance.GetHand();

        hands.GetAction(hands =>
        {
            Card.Trump trump = hands;
            if (!hands.isSelect) return hands;
            hands.state = Card.State.trash;

            return hands;
        });

        bool flag = false;

        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].isSelect)
            {

                hands.RemoveAt(i);
                i--;
                flag = true;
            }

        }

        if (!flag)
        {
            int ss = 0;
        }

        CardManager.instance.SetHand(hands);

    }

    /// <summary>
    /// 繧ｫ繝ｼ繝峨ｒ繝医Λ繝・す繝･縺ｫ遘ｻ蜍輔＆縺帙ｋ髢｢謨ｰ
    /// </summary>
    /// <param name="cardObjectHand"></param>
    private void CardMoveDiscard(CardObject cardObjectHand)
    {

        // 遘ｻ蜍慕岼讓吝慍轤ｹ繧堤｢ｺ隱・
        Vector3 goalPos = _handTrash.position;

        // 遘ｻ蜍暮㍼縺ｨ蠎ｧ讓吶ｒ蜷郁ｨ医ｒ邂怜・
        Vector3 moveVec = Vector3.Lerp(cardObjectHand.GetBeforePosition(), goalPos, cardObjectHand.GetMoveTimeRata());

        // 遘ｻ蜍・
        cardObjectHand.transform.position = moveVec;

        // 隗貞ｺｦ縺ｮ螟画峩
        cardObjectHand.transform.eulerAngles = Vector3.Lerp(_NORMALl_ANGLE, _TRASH_ANGLE,
            (cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED) > 1 ? 1 : cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED);

        if (cardObjectHand.GetMoveTimeRata() < 1) return;

        _cardObjectHands.Remove(cardObjectHand);

        if (_cardObjectHands.GetCount(card => card.GetStatus() == CardObject.status.discard) != 0) return;

        // 驕ｸ謚樒憾諷九・繧ｫ繝ｼ繝峨ｒ蜈ｨ縺ｦ繝医Λ繝・す繝･縺ｫ騾√ｋ
        IsSelectTrash();

        //繝ｩ繧ｦ繝ｳ繝峨・邨ゆｺ・ｺ門ｙ繧偵☆繧・
        RoundObserver.Instance.StartRoundEnd();

    }
    /// <summary>
    /// 譌｢縺ｫ陦ｨ縺ｫ縺ｪ縺｣縺ｦ縺・ｋ繧ｫ繝ｼ繝峨↓螟画峩繧貞刈縺医ｋ
    /// </summary>
    private void HandCardChengeTrump(CardObject cardObjectHand, int id)
    {

        // 逶ｮ讓呵ｧ貞ｺｦ繧定ｨｭ螳・
        Vector3 goal = _chengeCardID.Contains(id) ? _BACK_SIDE : _NORMALl_ANGLE;

        // 蛻晄悄隗貞ｺｦ繧定ｨｭ螳・
        Vector3 start = _chengeCardID.Contains(id) ? _NORMALl_ANGLE : _BACK_SIDE;

        cardObjectHand.transform.eulerAngles = Vector3.Lerp(start, goal,
            (cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED) > 1 ? 1 : cardObjectHand.GetMoveTimeRata() * _ANGLE_CHANGE_SPEED);

        // 迴ｾ蝨ｨ蜍輔￠繧狗憾諷九°繧堤｢ｺ隱・
        if (cardObjectHand.IsMovable()) return;

        // 繧ゅ≧荳蠎ｦ蜍輔￠繧九ｈ縺・↓螟画峩
        cardObjectHand.ResetMoveTime();

        // 螟画峩繧偵＠縺ｦ縺・ｋ繧ｫ繝ｼ繝峨′驟榊・縺ｮ菴慕分縺九ｒ遒ｺ隱・
        int targetID = _chengeCardID.FindIndex(n => n == id);

        if (!_chengeCardID.Contains(id) && cardObjectHand.GetStatus() != CardObject.status.hand)
        {

            cardObjectHand.SetStatus(CardObject.status.hand);

            CardPaint(_chengeCardTrump[0], id);

            _chengeCardTrump.RemoveAt(0);

        }

        if (targetID < 0) return;

        // 遒ｺ隱阪＠縺溽分蜿ｷ縺ｮ驟榊・繧帝勁螟・
        _chengeCardID.RemoveAt(targetID);

    }

    private float _time = 0;
    private int reta = 1;
    private Vector3 _lostAngle = Vector3.zero;
    /// <summary>
    /// 繧ｫ繝ｼ繝峨・繧｢繧ｯ繧ｷ繝ｧ繝ｳ繧定｡後≧繧ｯ繝ｩ繧ｹ
    /// </summary>
    /// <param name="cardObjectHand"></param>
    /// <param name="ID"></param>
    private void HandCardActionTrump(CardObject cardObjectHand, int ID)
    {

        // 繧｢繧ｯ繧ｷ繝ｧ繝ｳ蠕・ｩ溘・荳ｭ縺ｧ荳逡ｪ闍･縺・が繝悶ず繧ｧ繧ｯ繝医・縺ｨ縺阪□縺鷹壹☆
        if (ID != _cardObjectHands.FindIndex(hand => hand.GetStatus() == CardObject.status.action)) return;

        _time += Time.deltaTime * GameConfig.GetGameSpeed() * 10;

        cardObjectHand.transform.eulerAngles = Vector3.Lerp(_lostAngle, new Vector3(0, 0, 45 * reta), _time);

        if (_time < 1) return;

        _time = 0;
        int completedReta = reta;
        if (reta == 0) reta = -1;
        if (reta == 1) reta = 0;
        // transform.eulerAngles 縺ｯ 0縲・60 縺ｧ豁｣隕丞喧縺輔ｌ縺ｦ霑斐▲縺ｦ縺上ｋ縺溘ａ縲・
        // 縺昴・縺ｾ縺ｾ谺｡縺ｮ Lerp 縺ｮ髢句ｧ玖ｧ貞ｺｦ縺ｫ菴ｿ縺・→(萓・359.99ﾂｰ縺ｪ縺ｩ)螟ｧ縺阪￥蝗槭ｊ霎ｼ繧薙〒縺励∪縺・％縺ｨ縺後≠繧九・
        // 逶ｴ蜑阪・繝輔ぉ繝ｼ繧ｺ縺ｮ逶ｮ讓呵ｧ貞ｺｦ繧偵◎縺ｮ縺ｾ縺ｾ菴ｿ縺・％縺ｨ縺ｧ縲√％縺ｮ蝗槭ｊ霎ｼ縺ｿ繧帝亟縺舌・
        _lostAngle = new Vector3(0, 0, 45 * completedReta);

        if (reta != -1) return;
        _cardObjectHands[ID].PlayAction();

        reta = 1;
        _time = 0;

        if (_cardObjectHands[ID].GetActionsCount() > 0) return;

        //莉ｮ邨・∩縲繧ｹ繧ｳ繧｢縺ｮ蜉邂・蠕後〒螟画峩縺吶ｋ
        // 繧｢繧ｯ繧ｷ繝ｧ繝ｳ繧定ｿｽ蜉縺励◆莠九↓縺ゅ◆繧雁､画峩縺励◆蛟､繧呈綾縺励※縺・ｋ
        _cardObjectHands[ID].SetStatus(_cardObjectHands[ID].GetLostStatus());
        _cardObjectHands[ID].SetGrab(false);
        _cardObjectHands[ID].StopMove();

        //繧｢繧ｯ繧ｷ繝ｧ繝ｳ蠕・ｩ溘′蟄伜惠縺励※縺・ｋ
        if (_cardObjectHands.GetCount(hand => hand.GetStatus() == CardObject.status.action) > 0) return;

        JokerUtility.JokerPlayStart();

    }

    /// <summary>
    /// 莉ｮ邨・∩
    /// </summary>
    /// <param name="ID"></param>
    public void TrunpScore(int ID)
    {
        int score = 0;

        score = (int)CardManager.instance.GetHand()[ID].number;

        if (score <= 1 || 11 < score) score = 11;

        ScoreManager.instance.BasicPlus(score);
        GameConfig.AccelerateGameSpeed();
        // TODO:譁・ｭ励ｒ蜃ｺ縺吶繝槭ず縺ｧ蠕悟・
        // score
        //_cardObjectHands[ID].gameObject
        // 蝓ｺ譛ｬ繧ｹ繧ｳ繧｢
        ScoreManager.instance.SetScoreViewID(ID);
        ScoreManager.instance.SetScoreViewTrans(_cardObjectHands[ID].gameObject.transform.position);

        ScoreManager.instance.SetScoreViewText(score);

    }

    /// <summary>
    /// 繧ｫ繝ｼ繝峨・諠・ｱ繧貞・縺ｫ繧ｫ繝ｼ繝峨・繝槭ユ繝ｪ繧｢繝ｫ繧偵そ繝・ヨ縺吶ｋ髢｢謨ｰ
    /// </summary>
    public void CardPaint(Card.Trump cardData, GameObject gameObject)
    {

        MeshRenderer meshRenderer = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        // 繝医Λ繝ｳ繝励・繧ｨ繝輔ぉ繧ｯ繝医・繝・Μ繧｢繝ｫ繧偵そ繝・ヨ
        if (Card.deckBuff.None != cardData.deckBuff) materials[(int)cardMaterialType.effect] = BuffUtility.GetTrumpMaterial((int)cardData.deckBuff);
        else materials[(int)cardMaterialType.effect] = BuffUtility.GetDommyMaterial();
        if (Card.cardBuff.None != cardData.cardBuff) materials[(int)cardMaterialType.effect] = BuffUtility.GetCardMaterial((int)cardData.cardBuff);
        if (Card.sealBuff.None != cardData.sealBuff) materials[(int)cardMaterialType.sael] = BuffUtility.GetSealMaterial((int)cardData.sealBuff);
        else materials[(int)cardMaterialType.sael] = BuffUtility.GetDommyMaterial();

        // 繝医Λ繝ｳ繝励・繧ｽ繝ｼ繝・→繝翫Φ繝舌・繧貞性繧薙□繝槭ユ繝ｪ繧｢繝ｫ繧偵そ繝・ヨ
        materials[(int)cardMaterialType.main] = _materialManager.GetMaterial((int)cardData.suit, (int)cardData.number);

        if (cardData.deckBuff == Card.deckBuff.Glass)
        {
            //繧ｰ繝ｩ繧ｺ縺ｮ繝槭ユ繝ｪ繧｢繝ｫ縺ｮ縺ｨ縺阪□縺代・繝ｼ繧ｹ縺ｮ繝槭ユ繝ｪ繧｢繝ｫ縺ｮ繝ｬ繝ｳ繝繝ｪ繝ｳ繧ｰ繝｢繝ｼ繝峨ｒFade縺ｫ螟画峩縺吶ｋ
            materials[(int)cardMaterialType.main].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            materials[(int)cardMaterialType.main].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            materials[(int)cardMaterialType.main].SetInt("_ZWrite", 1);
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHATEST_ON");
            materials[(int)cardMaterialType.main].EnableKeyword("_ALPHABLEND_ON");
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[(int)cardMaterialType.main].renderQueue = 3000;
        }
        else
        {
            materials[(int)cardMaterialType.main].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            materials[(int)cardMaterialType.main].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            materials[(int)cardMaterialType.main].SetInt("_ZWrite", 1);
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHATEST_ON");
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHABLEND_ON");
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[(int)cardMaterialType.main].renderQueue = -1;
        }

        meshRenderer.materials = materials;
    }
    private void CardPaint(Card.Trump cardData, int id)
    {
        Debug.Log(cardData.suit.ToString() + id + ":" + _cardObjectHands[id].name);

        MeshRenderer meshRenderer = _cardObjectHands[id].transform.GetChild(0).GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        // 繝医Λ繝ｳ繝励・繧ｨ繝輔ぉ繧ｯ繝医・繝・Μ繧｢繝ｫ繧偵そ繝・ヨ
        if (Card.deckBuff.None != cardData.deckBuff) materials[(int)cardMaterialType.effect] = BuffUtility.GetTrumpMaterial((int)cardData.deckBuff);
        else materials[(int)cardMaterialType.effect] = BuffUtility.GetDommyMaterial();
        if (Card.cardBuff.None != cardData.cardBuff) materials[(int)cardMaterialType.effect] = BuffUtility.GetCardMaterial((int)cardData.cardBuff);
        if (Card.sealBuff.None != cardData.sealBuff) materials[(int)cardMaterialType.sael] = BuffUtility.GetSealMaterial((int)cardData.sealBuff);
        else materials[(int)cardMaterialType.sael] = BuffUtility.GetDommyMaterial();

        // 繝医Λ繝ｳ繝励・繧ｽ繝ｼ繝・→繝翫Φ繝舌・繧貞性繧薙□繝槭ユ繝ｪ繧｢繝ｫ繧偵そ繝・ヨ
        materials[(int)cardMaterialType.main] = _materialManager.GetMaterial((int)cardData.suit, (int)cardData.number);

        if (cardData.deckBuff == Card.deckBuff.Glass)
        {
            //繧ｰ繝ｩ繧ｺ縺ｮ繝槭ユ繝ｪ繧｢繝ｫ縺ｮ縺ｨ縺阪□縺代・繝ｼ繧ｹ縺ｮ繝槭ユ繝ｪ繧｢繝ｫ縺ｮ繝ｬ繝ｳ繝繝ｪ繝ｳ繧ｰ繝｢繝ｼ繝峨ｒFade縺ｫ螟画峩縺吶ｋ
            materials[(int)cardMaterialType.main].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            materials[(int)cardMaterialType.main].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            materials[(int)cardMaterialType.main].SetInt("_ZWrite", 1);
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHATEST_ON");
            materials[(int)cardMaterialType.main].EnableKeyword("_ALPHABLEND_ON");
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[(int)cardMaterialType.main].renderQueue = 3000;
        }
        else
        {
            materials[(int)cardMaterialType.main].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            materials[(int)cardMaterialType.main].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            materials[(int)cardMaterialType.main].SetInt("_ZWrite", 1);
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHATEST_ON");
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHABLEND_ON");
            materials[(int)cardMaterialType.main].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[(int)cardMaterialType.main].renderQueue = -1;
        }

        meshRenderer.materials = materials;
    }

    /// <summary>
    /// 菴ｿ逕ｨ蜿ｯ閭ｽ縺ｪ繧ｫ繝ｼ繝峨ｒ霑斐☆髢｢謨ｰ
    /// </summary>
    /// <returns></returns>
    private CardObject GetUseCardObject()
    {
        for (int i = _cardObjects.Count - 1; i >= 0; i--)
        {
            // 繧ｫ繝ｼ繝峨′deck縺ｫ縺ｪ縺九▲縺溘ｉ繧ゅ≧荳蠎ｦ
            if (_cardObjects[i].GetStatus() != CardObject.status.deck) continue;

            return _cardObjects[i];
        }
        // 菴輔ｂ霑斐○繧狗黄縺後↑縺・
        return null;

    }

    /// <summary>
    /// 52譫夂函謌舌☆繧矩未謨ｰ
    /// </summary>
    private void CreateCard()
    {
        _cardPool = new GameObject("CardPool");
        for (int i = 0; i < 52; i++)
        {
            _cardObjects.Add(Instantiate(_cardBase, _cardDeck.position, Quaternion.identity).AddComponent<CardObject>());
            _cardObjects[i].SetStatus(CardObject.status.deck);
            _cardObjects[i].transform.eulerAngles = _BACK_SIDE;
            _cardObjects[i].transform.parent = _cardPool.transform;
            _cardObjects[i].gameObject.name = "Card" + i;
        }
    }

    /// <summary>
    /// 繝励Ξ繧､迥ｶ諷九・繧ｫ繝ｼ繝峨・謨ｰ繧偵き繧ｦ繝ｳ繝医☆繧矩未謨ｰ
    /// </summary>
    /// <returns></returns>
    private int GetPlayCardCount()
    {
        int count = 0;
        for (int i = 0; i < _cardObjectHands.Count; i++)
        {
            if (_cardObjectHands[i].GetStatus() != CardObject.status.play) continue;
            count++;

        }
        return count;
    }

}

