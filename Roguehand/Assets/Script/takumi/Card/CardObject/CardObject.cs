using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    /// <summary>
    /// 荳蠎ｦ縺ｮ遘ｻ蜍輔↓謗帙°繧区凾髢薙・螳壽焚
    /// </summary>
    private const float MOVE_TIME = 0.2f;

    /// <summary>
    /// 縺薙・繧ｫ繝ｼ繝峨・迥ｶ諷・
    /// </summary>
    public enum status
    {
        none = -1,
        deck,
        change,
        hand,
        play,
        playWait,
        discard,
        action
    }

    /// <summary>
    /// 迴ｾ蝨ｨ縺ｮ迥ｶ諷・
    /// </summary>
    [SerializeField] private status _status = status.none;

    /// <summary>
    /// 縺ｲ縺ｨ縺､蜑阪・迥ｶ諷・
    /// </summary>
    [SerializeField] private status _lostStatus = status.none;

    [SerializeField]private float _moveTime = 0;

    /// <summary>
    /// 遘ｻ蜍輔ｒ髢句ｧ九☆繧句燕縺ｮ蠎ｧ讓・
    /// </summary>
    private Vector3 _beforePosition = Vector3.zero;
    /// <summary>
    /// 遘ｻ蜍輔ｒ髢句ｧ九☆繧句燕縺ｮ隗貞ｺｦ
    /// </summary>
    private Vector3 _beforeAngle = Vector3.zero;

    /// <summary>
    /// 縺薙・繧ｪ繝悶ず繧ｧ繧ｯ繝医・繝ｪ繧ｮ繝・ラ繝懊ョ繧｣
    /// </summary>
    private Rigidbody _rigidbody;

    /// <summary>
    /// 迴ｾ蝨ｨ縺､縺九∪繧後※縺・ｋ縺九←縺・°
    /// </summary>
    [SerializeField] private bool _isGrab = false;

    /// <summary>
    /// 迴ｾ蝨ｨ縺､縺九・縺薙→縺悟庄閭ｽ縺九←縺・°
    /// </summary>
    [SerializeField] private bool _grab = true;

    [SerializeField] private List<System.Action> actions = new List<System.Action>();

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Finish") return;

        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;

        tag = collision.transform.tag;
    }
    public void Awake()
    {
        initialize();
    }

    public void initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 驥榊鴨繧呈桃菴懷庄閭ｽ迥ｶ諷九↓螟画峩
    /// </summary>
    public void GravityStart()
    {
        tag = "Untagged";
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }

    /// <summary>
    /// 繧ｫ繝ｼ繝峨・繝ｪ繧ｻ繝・ヨ縺ｫ菴ｿ縺・未謨ｰ
    /// </summary>
    public void ResetCard()
    {
        SetStatus(CardObject.status.deck);
        ResetMoveTime();
        GravityStart();
        _isGrab = false;
        // 繝励・繝ｫ縺ｧ菴ｿ縺・屓縺励◆髫帙∝燕蝗槫・縺ｮ譛ｪ豸亥喧繧｢繧ｯ繧ｷ繝ｧ繝ｳ縺梧ｮ九ｉ縺ｪ縺・ｈ縺・↓縺吶ｋ
        actions.Clear();
    }

    public void GetCheckBuff(Card.Trump trump,System.Action<int> action,int id)
    {
        if (BuffUtility.CheckPlayBuffDeck(trump.deckBuff))
            actions.Add(()=>
            {
                TrumpBuff.target = gameObject;
                TrumpBuff.targetID = id;
                BuffUtility.GetActionPlayBuffDeck(trump.deckBuff)();
            });

        if (BuffUtility.CheckPlayBuffCard(trump.cardBuff))
            actions.Add(()=>
            {
                CardBuff.target = gameObject;
                CardBuff.targetID = id;

                BuffUtility.GetActionPlayBuffCard(trump.cardBuff)();
            });
        if (BuffUtility.CheckPlayBuffSeal(trump.sealBuff))
            actions.Add(()=>
            {
                SealBuff.target = gameObject;
                SealBuff.targetID = id;

                BuffUtility.GetActionPlayBuffSeal(trump.sealBuff)();
            });

        actions.Add(()=>action(id));
    }

    public int GetActionsCount() {  return actions.Count; }

    public void AddAction(System.Action action) {  actions.Add(action); }

    public void PlayAction()
    {
        actions[0]();

        actions.RemoveAt(0);
    }

    public void SetStatus(status status) { _lostStatus = _status; _status = status; }

    public status GetStatus() { return _status; }

    public status GetLostStatus() { return _lostStatus; }

    /// <summary>
    /// 遘ｻ蜍募庄閭ｽ譎る俣繧偵Μ繧ｻ繝・ヨ
    /// 遘ｻ蜍輔ｒ蜿ｯ閭ｽ縺ｫ螟画峩
    /// </summary>
    public void ResetMoveTime()
    {
        _beforePosition = transform.position;
        _beforeAngle = transform.eulerAngles;
        _moveTime = MOVE_TIME;
    }

    /// <summary>
    /// 譎る俣邨碁℃縺ｮ髢｢謨ｰ
    /// </summary>
    public void CountDown()
    {
        //縺､縺九∪繧後※縺・ｋ髢薙き繧ｦ繝ｳ繝医＠縺ｪ縺・
        if (_isGrab) return;
        _moveTime -= Time.deltaTime * GameConfig.GetGameSpeed();
        if (IsMovable()) return;
        _grab = true;
    }

    /// <summary>
    /// 遘ｻ蜍募庄閭ｽ縺九←縺・°縺ｮ蛻､螳・
    /// </summary>
    /// <returns></returns>
    public bool IsMovable() { return _moveTime > 0; }

    public float GetMoveTime() { return _moveTime; }
    public float GetMoveTimeRata() { return 1f - (_moveTime / MOVE_TIME); }

    public void StopMove() { _moveTime = 0f; }

    public Vector3 GetBeforePosition() { return _beforePosition; }
    public Vector3 GetBeforeAngle() { return _beforeAngle; }

    public void SetGrab(bool flag) { _isGrab = flag; }

    /// <summary>
    /// 縺､縺九・縺薙→縺悟庄閭ｽ縺九←縺・°繧定ｿ斐☆髢｢謨ｰ
    /// </summary>
    /// <returns></returns>
    public bool GetGrabFlag() { return _grab; }

    /// <summary>
    /// 縺､縺九・縺薙→繧貞・譚･縺ｪ縺丞､画峩
    /// 繧ｫ繝ｼ繝峨′逶ｮ逧・慍縺ｫ逹縺・◆繧芽ｧ｣髯､
    /// </summary>
    public void NotGrab() { _grab = false; }

    public bool IsGrab() { return _isGrab; }

    public System.Action AddScore(Card.number number)
    {
        return () =>
        {
            float score = (int)number;
            if (score <= 1 || 11 < score) score = 11;
        };
    }
}

