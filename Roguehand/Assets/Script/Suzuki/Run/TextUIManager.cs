using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUIManager : MonoBehaviour
{
    public static TextUIManager instance;

    [SerializeField] private TextMeshProUGUI _roundNameText;
    [SerializeField] private TextMeshProUGUI _lowestScoreText;
    [SerializeField] private TextMeshProUGUI _rewardCountText;
    [SerializeField] private TextMeshProUGUI _roundScoreText;
    [SerializeField] private TextMeshProUGUI _roleText;
    [SerializeField] private TextMeshProUGUI _basicScoreText;
    [SerializeField] private TextMeshProUGUI _magnificationText;
    [SerializeField] private TextMeshProUGUI _handText;
    [SerializeField] private TextMeshProUGUI _discardText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _anteText;
    [SerializeField] private TextMeshProUGUI _roundText;
    [SerializeField] private TextMeshProUGUI _resultLowestScoreText;
    [SerializeField] private TextMeshProUGUI _resultHandText;
    [SerializeField] private TextMeshProUGUI _resultMoneyText;
    [SerializeField] private TextMeshProUGUI _resultClearMoneyText;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        Initialize();
        // 今は適当
        SetRewardCountText(UIUtility.instance.RewardConversion(5));
    }


    public struct UITexts
    {
        public string roundname;        // ラウンドの名前
        public string lowestscore;      // 最低スコア
        public string rewardCount;      // 報酬金
        public string roundscore;       // ラウンド内で得た合計スコア
        public string role;             // 役の名前とレベル
        public string basicscore;       // 基礎点
        public string magnification;    // 倍率
        public string hand;             // ハンドの回数
        public string discard;          // ディスカードの回数
        public string money;            // 所持金
        public string ante;             // 難易度
        public string round;            // クリアしたラウンド回数
    }

    public void Initialize()
    {
        UITexts texts = new UITexts();
        texts.roundname = "";
        texts.lowestscore = "";
        texts.rewardCount = "";
        texts.roundscore = "0";
        texts.role = "";
        texts.basicscore = "0";
        texts.magnification = "0";
        texts.hand = "5";
        texts.discard = "5";
        texts.money = "$0";
        texts.ante = "1";
        texts.round = "0";

        #region テキストの初期化
        SetRoundNameText(texts.roundname);
        SetLowestScoreText(texts.lowestscore);
        SetRewardCountText(texts.rewardCount);
        SetRoundScoreText(texts.roundscore);
        SetRoleText(texts.role);
        SetBasicScoreText(texts.basicscore);
        SetMagnificationText(texts.magnification);
        SetHandText(texts.hand);
        SetDiscardText(texts.discard);
        SetMoneyText(texts.money);
        SetAnteText(texts.ante);
        SetRoundText(texts.round);
        #endregion
    }

    #region ゲッター

    public TextMeshProUGUI GetRoundNameText() { return _roundNameText; }
    public TextMeshProUGUI GetLowestScoreText() { return _lowestScoreText; }
    public TextMeshProUGUI GetRewardText() { return _rewardCountText; }
    public TextMeshProUGUI GetRoundScoreText() { return _roundScoreText; }
    public TextMeshProUGUI GetRoleText() { return _roleText; }
    public TextMeshProUGUI GetBasicScoreText() { return _basicScoreText; }
    public TextMeshProUGUI GetMagnificationText() { return _magnificationText; }
    public TextMeshProUGUI GetHandText() { return _handText; }
    public TextMeshProUGUI GetDiscardText() { return _discardText; }
    public TextMeshProUGUI GetMoneyText() { return _moneyText; }
    public TextMeshProUGUI GetAnteText() { return _anteText; }
    public TextMeshProUGUI GetRoundText() { return _roundText; }
    public TextMeshProUGUI GetResultLowestScoreText() { return _resultLowestScoreText; }
    public TextMeshProUGUI GetResultHandText() { return _resultHandText; }
    public TextMeshProUGUI GetResultMoneyText() { return _resultMoneyText; }
    public TextMeshProUGUI GetResultClearMoneyText() { return _resultClearMoneyText; }

    #endregion

    #region セッター

    public void SetRoundNameText(string value) { _roundNameText.text = value; }
    public void SetLowestScoreText(string value) { _lowestScoreText.text = value; }
    public void SetRewardCountText(string value) { _rewardCountText.text = value; }
    public void SetRoundScoreText(string value) { _roundScoreText.text = value; }
    public void SetRoleText(string value) { _roleText.text = value; }
    public void SetBasicScoreText(string value) { _basicScoreText.text = value; }
    public void SetMagnificationText(string value) { _magnificationText.text = value; }
    public void SetHandText(string value) { _handText.text = value; }
    public void SetDiscardText(string value) { _discardText.text = value; }
    public void SetMoneyText(string value) { _moneyText.text = value; }
    public void SetAnteText(string value) { _anteText.text = value; }
    public void SetRoundText(string value) { _roundText.text = value; }
    public void SetResultLowestScoreText(string value) { _resultLowestScoreText.text = value; }
    public void SetResultHandText(string value) { _resultHandText.text = value; }
    public void SetResultMoneyText(string value) { _resultMoneyText.text = value; }
    public void SetResultClearMoneyText(string value) { _resultClearMoneyText.text = value; }

    #endregion


}
